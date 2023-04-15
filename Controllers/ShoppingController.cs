using Microsoft.Ajax.Utilities;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.DynamicData;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WebApplication1.Extensions;
using WebApplication1.Models;
using WebApplication1.Models.Functions;

namespace WebApplication1.Controllers
{
    public class ShoppingController : Controller
    {
        // GET: Shopping
        MobileShoppingEntities db = new MobileShoppingEntities();

        public ActionResult ShowToCart()
        {
            user user = Session["email"] as user;
            List<cart> cart = db.carts.Where(x => x.id_user == user.id_user).ToList();
            int a = 0;
            int b = 0;
            foreach (var item in cart)
            {
                var pro = db.products.Where(x => x.id_product == item.id_product).FirstOrDefault();
                if (pro.quantity == 0)
                {
                    a = a + 1;
                    this.AddNotification("Product " + pro.names + " out of stock", NotificationType.WARNING);
                }
                if (pro.quantity < item.quantity)
                {
                    b = b + 1;
                    this.AddNotification("Product " + pro.names + " not enough quantity", NotificationType.WARNING);
                }
            }
            Session["CheckQuantity1"] = a;
            Session["CheckQuantity2"] = b;

            if (cart != null)
            {
                var pro = cart.Sum(x => x.quantity);
                Session["Quantity_pro"] = pro;
                var total_cost = cart.Sum(x => x.product.price * x.quantity);
                Session["TotalCost"] = total_cost;
                var shippingfee = cart.Sum(x => x.quantity * 2);
                if (shippingfee > 0)
                {
                    Session["Shipping"] = shippingfee + 10;
                }
                else
                {
                    Session["Shipping"] = shippingfee;
                }
                var total_order = (int)Session["Shipping"] + total_cost;
                Session["Total_order"] = total_order;
            }
            return View(cart);
        }
        //public ActionResult Details_Pro(int id)
        //{
        //    var pro = db.products.Where(x => x.id_product == id).First();
        //    return View(pro);
        //}
        public ActionResult AddToCart(int id)
        {
            this.AddNotification("Item has been added to your card !", NotificationType.SUCCESS);
            user cus = Session["email"] as user;
            var carts = db.carts.Where(x => x.id_user == cus.id_user).ToList();
            var pro = db.products.Where(x => x.id_product == id).First();
            if (pro != null || pro.quantity >= 1)
            {
                var cart = db.carts.Where(x => x.id_product == id && x.id_user == cus.id_user).FirstOrDefault();
                if (cart != null)
                {
                    cart cart_user = new cart()
                    {
                        id_product = id,
                        id_user = cus.id_user,
                        quantity = cart.quantity + 1,
                    };
                    var fcart = new Func_Cart();
                    fcart.Update(cart_user);
                }
                if (cart == null)
                {

                    cart cart_user = new cart()
                    {
                        id_product = id,
                        id_user = cus.id_user,
                        quantity = 1,
                    };
                    var fcart = new Func_Cart();
                    fcart.Insert(cart_user);
                }
            }
            return RedirectToAction("Details_Pro", "Home", new { id = id });
        }

        public ActionResult BuyNow(int id)
        {
            user cus = Session["email"] as user;
            var carts = db.carts.Where(x => x.id_user == cus.id_user).ToList();
            var pro = db.products.Where(x => x.id_product == id).First();
            if (pro != null || pro.quantity >= 1)
            {
                var cart = db.carts.Where(x => x.id_product == id && x.id_user == cus.id_user).FirstOrDefault();
                if (cart != null)
                {
                    cart cart_user = new cart()
                    {
                        id_product = id,
                        id_user = cus.id_user,
                        quantity = cart.quantity + 1,
                    };
                    var fcart = new Func_Cart();
                    fcart.Update(cart_user);
                }
                if (cart == null)
                {

                    cart cart_user = new cart()
                    {
                        id_product = id,
                        id_user = cus.id_user,
                        quantity = 1,
                    };
                    var fcart = new Func_Cart();
                    fcart.Insert(cart_user);
                }
            }
            return RedirectToAction("ShowToCart", "Shopping");
        }
        public ActionResult RemoveCart(int id)
        {
            user user = Session["email"] as user;
            var cart = db.carts.Where(x => x.id_user == user.id_user && x.id_product == id).FirstOrDefault();
            var fcart = new Func_Cart();
            fcart.Delete(cart.id_cart);
            return RedirectToAction("ShowToCart", "Shopping");
        }
        public PartialViewResult BagCart()
        {
            int total_item = 0;
            user user = Session["email"] as user;
            var cart = db.carts.Where(x => x.id_user == user.id_user).ToList();
            if (cart != null)
                total_item = (int)cart.Sum(x => x.quantity);
            ViewBag.InfoCart = total_item;
            return PartialView("BagCart");
        }
        public ActionResult ShoppingSuccess(int id)
        {
            var list = db.order_item.Where(x => x.id_order == id).ToList();
            return View(list);
        }
        public ActionResult btn_Increase(int id)
        {
            user user = Session["email"] as user;
            var cart = db.carts.Where(x => x.id_product == id && x.id_user == user.id_user).FirstOrDefault();
            var pro = db.products.Where(x => x.id_product == id).FirstOrDefault();
            if (pro.quantity >= 1 && cart.quantity < pro.quantity)
            {
                var quantity = new cart()
                {
                    id_cart = cart.id_cart,
                    id_user = user.id_user,
                    id_product = id,
                    quantity = cart.quantity + 1,
                };
                var fcart = new Func_Cart();
                fcart.Update(quantity);
            }
            return RedirectToAction("ShowToCart", "Shopping");
        }
        public ActionResult btn_Decrease(int id)
        {
            user user = Session["email"] as user;
            var cart = db.carts.Where(x => x.id_product == id && x.id_user == user.id_user).FirstOrDefault();
            var pro = db.products.Where(x => x.id_product == id).FirstOrDefault();
            if (cart.quantity > 1)
            {
                var quantity = new cart()
                {
                    id_cart = cart.id_cart,
                    id_user = user.id_user,
                    id_product = id,
                    quantity = cart.quantity - 1,
                };
                var fcart = new Func_Cart();
                fcart.Update(quantity);
            }
            return RedirectToAction("ShowToCart", "Shopping");
        }
        public ActionResult CheckOut(FormCollection form)
        {
            user user = Session["email"] as user;
            customer customer = new customer();

            string phone = form["phone"];
            string email = form["email"];
            string address = form["address"];
            string district = form["district"];
            string ward = form["ward"];
            string city = form["city"];

            var cart = db.carts.Where(x => x.id_user == user.id_user).ToList();
            order orders = new order();
            var findcus = db.customers.Where(x => x.phone == phone && x.email == email && x.addresss == address && x.district == district && x.ward == ward && x.city == city).FirstOrDefault();
            if (findcus == null)
            {
                customer.phone = form["phone"];
                customer.addresss = form["address"] + ",";
                customer.ward = form["ward"] + ",";
                customer.district = form["district"] + ",";
                customer.city = form["city"];
                customer.email = form["email"];

                db.customers.Add(customer);

                orders.id_customer = customer.id_customer;
            }
            else if (findcus != null)
            {
                orders.id_customer = findcus.id_customer;
            }



            orders.id_user = user.id_user;
            orders.created_at = DateTime.Now;
            orders.payment_type = true;
            orders.request_cancel = false;

            var code = form["code"];
            var promo = db.promocodes.Where(x => x.code == code).FirstOrDefault();
            var count = db.promocodes.Count(x => x.code == code);
            if (count == 1)
            {
                if (promo.started_at <= DateTime.Now && promo.finished_at >= DateTime.Now)
                {
                    var checkpromo = db.orders.Where(x => x.id_user == user.id_user).ToList();
                    var check = 0;
                    foreach (var item in checkpromo)
                    {
                        if (item.id_promo == promo.id_promo)
                        {
                            check = 1;
                        }
                    }
                    if (check == 0)
                    {
                        orders.id_promo = promo.id_promo;
                        orders.shipping_fee = Convert.ToInt32(Session["Shipping"]);
                        decimal totalcost = Convert.ToDecimal(Session["TotalCost"]);
                        int shipping = Convert.ToInt32(Session["Shipping"]);
                        Session["discount"] = totalcost * promo.discount_price / 100;
                        orders.total_price = totalcost - Convert.ToDecimal(Session["discount"]) + shipping;
                        Session["Total_order_new"] = totalcost - Convert.ToDecimal(Session["discount"]) + shipping;
                    }
                    else
                    {
                        this.AddNotification("Promocode '" + promo.code + "' has been used !", NotificationType.WARNING);
                        return RedirectToAction("ShowToCart", "Shopping");
                    }

                }
                else
                {
                    this.AddNotification("Promocode '" + promo.code + "' out of date !", NotificationType.WARNING);
                    return RedirectToAction("ShowToCart", "Shopping");
                }
            }
            else if (count == 0 && code != "")
            {
                this.AddNotification("Promocode '" + code + "' is not correct !", NotificationType.WARNING);
                return RedirectToAction("ShowToCart", "Shopping");
            }
            else if (code.IsNullOrWhiteSpace())
            {
                Session["Total_order_new"] = Convert.ToDecimal(Session["Total_order"]);
                Session["discount"] = 0;
                orders.id_promo = null;
                orders.shipping_fee = Convert.ToInt32(Session["Shipping"]);
                orders.total_price = Convert.ToDecimal(Session["Total_order"]);
            }
            orders.pending = false;
            orders.canceled = false;

            db.orders.Add(orders);


            customer customers = Session["Customer"] as customer;
            Session["Customer"] = customer;

            ViewBag.order = orders.id_order;
            foreach (var item in cart)
            {
                product products = db.products.Find(item.product.id_product);
                int quantity_pro = (int)products.quantity - (int)item.quantity;
                products.quantity = quantity_pro;
                db.SaveChanges();
                order_item order_items = new order_item();
                order_items.id_order = orders.id_order;
                order_items.id_product = item.product.id_product;
                order_items.quantity = item.quantity;
                db.order_item.Add(order_items);
                db.SaveChanges();
            }
            foreach (var item in cart)
            {
                cart model = db.carts.Where(x => x.id_cart == item.id_cart).FirstOrDefault();
                db.carts.Remove(model);
                db.SaveChanges();
            }
            db.SaveChanges();
            return RedirectToAction("ShoppingSuccess", "Shopping", new { id = orders.id_order });

        }
        public ActionResult Thanks()
        {
            return View();
        }
        public ActionResult Payment()
        {
            //request params need to request to MoMo system

            string endpoint = "https://test-payment.momo.vn/gw_payment/transactionProcessor";
            string partnerCode = "MOMOOJOI20210710";
            string accessKey = "iPXneGmrJH0G8FOP";
            string serectkey = "sFcbSGRSJjwGxwhhcEktCHWYUuTuPNDB";
            string orderInfo = "test";
            string returnUrl = "https://localhost:44377/Shopping/Thanks";
            string notifyurl = "https://localhost:44377/Shopping/Thanks"; //lưu ý: notifyurl không được sử dụng localhost, có thể sử dụng ngrok để public localhost trong quá trình test

            string amount = "1000";
            string orderid = DateTime.Now.Ticks.ToString(); //mã đơn hàng
            string requestId = DateTime.Now.Ticks.ToString();
            string extraData = "";

            //string endpoint = ConfigurationManager.AppSettings["endpoint"].ToString();
            //string partnerCode = ConfigurationManager.AppSettings["partnerCode"].ToString();
            //string accessKey = ConfigurationManager.AppSettings["accessKey"].ToString();
            //string serectkey = ConfigurationManager.AppSettings["serectkey"].ToString();
            //string orderInfo = ConfigurationManager.AppSettings["orderInfo"].ToString();
            //string returnUrl = ConfigurationManager.AppSettings["returnUrl"].ToString();
            //string notifyurl = ConfigurationManager.AppSettings["notifyurl"].ToString(); //lưu ý: notifyurl không được sử dụng localhost, có thể sử dụng ngrok để public localhost trong quá trình test

            //string amount = Session["Total_order"].ToString();
            //string orderid = Guid.NewGuid().ToString(); //mã đơn hàng
            //string requestId = Guid.NewGuid().ToString();
            //string extraData = "";

            //Before sign HMAC SHA256 signature
            string rawHash = "partnerCode=" +
                partnerCode + "&accessKey=" +
                accessKey + "&requestId=" +
                requestId + "&amount=" +
                amount + "&orderId=" +
                orderid + "&orderInfo=" +
                orderInfo + "&returnUrl=" +
                returnUrl + "&notifyUrl=" +
                notifyurl + "&extraData=" +
                extraData;

            MoMoSecurity crypto = new MoMoSecurity();
            //sign signature SHA256
            string signature = crypto.signSHA256(rawHash, serectkey);

            //build body json request
            JObject message = new JObject
            {
                { "partnerCode", partnerCode },
                { "accessKey", accessKey },
                { "requestId", requestId },
                { "amount", amount },
                { "orderId", orderid },
                { "orderInfo", orderInfo },
                { "returnUrl", returnUrl },
                { "notifyUrl", notifyurl },
                { "extraData", extraData },
                { "requestType", "captureMoMoWallet" },
                { "signature", signature }

            };

            string responseFromMomo = PaymentRequest.sendPaymentRequest(endpoint, message.ToString());

            JObject jmessage = JObject.Parse(responseFromMomo);

            return Redirect(jmessage.GetValue("payUrl").ToString());
        }

        //Khi thanh toán xong ở cổng thanh toán Momo, Momo sẽ trả về một số thông tin, trong đó có errorCode để check thông tin thanh toán
        //errorCode = 0 : thanh toán thành công (Request.QueryString["errorCode"])
        //Tham khảo bảng mã lỗi tại: https://developers.momo.vn/#/docs/aio/?id=b%e1%ba%a3ng-m%c3%a3-l%e1%bb%97i
        public ActionResult ConfirmPaymentClient(Result result)
        {
            //lấy kết quả Momo trả về và hiển thị thông báo cho người dùng (có thể lấy dữ liệu ở đây cập nhật xuống db)
            string rMessage = result.message;
            string rOrderId = result.orderId;
            string rErrorCode = result.errorCode; // = 0: thanh toán thành công
            return View();
        }

        [HttpPost]
        public void SavePayment()
        {
            //cập nhật dữ liệu vào db
            String a = "";
        }
    }
}
