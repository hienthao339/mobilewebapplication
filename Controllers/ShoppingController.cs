using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Vml;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Ajax.Utilities;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json.Linq;
using PagedList;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.DynamicData;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WebApplication1.Controllers.Admin;
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
            Session["CheckPro"] = 0;
            foreach (var item in cart)
            {
                var pro = db.products.Where(x => x.id_product == item.id_product).FirstOrDefault();
                if (pro.quantity < 1)
                {
                    Session["CheckPro"] = 1;
                    this.AddNotification(pro.names + " out of stock !", NotificationType.WARNING);

                }
                if (pro.quantity < item.quantity)
                {
                    Session["CheckPro"] = 1;
                    this.AddNotification(pro.names + " not enough quantity !", NotificationType.WARNING);

                }
            }

            if (cart != null)
            {
                int Quantity = (int)cart.Sum(x => x.quantity);
                Session["Quantity"] = Quantity;
                decimal Temp_Total = 0;
                foreach (var item in cart)
                {
                    if (item.product.id_promo == null)
                    {
                        Temp_Total += (decimal)(item.product.price * item.quantity);
                    }
                    else
                    {
                        Temp_Total += (decimal)(item.product.discount * item.quantity);
                    }
                }
                Session["Temp_Total"] = Temp_Total;
                int Shipping = (int)Session["Quantity"] * 2;
                Session["Shipping"] = Shipping + 10;
            }
            return View(cart);
        }
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
        public ActionResult GetInfo()
        {
            return View();
        }
        public ActionResult CheckOutInfo(FormCollection form)
        {
            string email = form["email"].ToString();
            string phone = form["phone"].ToString();
            string addresss = form["address"].ToString() + ",";
            string district = form["district"].ToString() + ",";
            string ward = form["ward"].ToString() + ",";
            string city = form["city"].ToString();

            var find_cus = db.customers.Where(x => x.email == email && x.phone == phone && x.addresss == addresss && x.district == district && x.ward == ward && x.city == city).FirstOrDefault();
            if (find_cus == null)
            {
                customer cus = new customer();
                cus.email = form["email"].ToString();
                cus.phone = form["phone"].ToString();
                cus.addresss = form["address"].ToString() + ",";
                cus.district = form["district"].ToString() + ",";
                cus.ward = form["ward"].ToString() + ",";
                cus.city = form["city"].ToString();

                db.customers.Add(cus);

                Session["Customer"] = cus;
                return RedirectToAction("Promocode", "Shopping");
            }
            else
            {
                Session["Customer"] = find_cus;
                return RedirectToAction("Promocode", "Shopping");
            }
        }
        public ActionResult GetPromo(FormCollection form)
        {
            decimal Totals = (decimal)Session["Temp_Total"];
            Session["Total"] = Totals;
            string code = form["code"];
            customer customer = Session["Customer"] as customer;
            user user = Session["email"] as user;
            var promo = db.promocodes.Where(x => x.code == code).FirstOrDefault();
            if (promo != null)
            {
                if (promo.started_at <= DateTime.Now && promo.finished_at >= DateTime.Now)
                {
                    var orders = db.orders.Where(x => x.id_promo == promo.id_promo && x.id_user == user.id_user).FirstOrDefault();
                    if (orders == null)
                    {
                        decimal Total = (decimal)(((decimal)Session["Temp_Total"]) * (100 - promo.discount_price) / 100);
                        decimal Discount = (decimal)(((decimal)Session["Temp_Total"]) * promo.discount_price / 100);
                        Session["Promo"] = promo;
                        Session["Discount"] = Discount;
                        Session["Total"] = Total;
                        return RedirectToAction("CheckOut", "Shopping");
                    }
                    else
                    {
                        this.AddNotification("You have used this promo code !", NotificationType.WARNING);
                        return RedirectToAction("Promocode", "Shopping");
                    }
                }
                else
                {
                    this.AddNotification("Promo code is incorrect !", NotificationType.WARNING);
                    return RedirectToAction("Promocode", "Shopping");
                }
            }
            else if (promo == null && code == "")
            {
                decimal Total = (decimal)Session["Temp_Total"];
                Session["Total"] = Total;
                Session["Discount"] = 0.00;
                return RedirectToAction("CheckOut", "Shopping");
            }
            else
            {
                this.AddNotification("Promo code is incorrect !", NotificationType.WARNING);
                return RedirectToAction("Promocode", "Shopping");
            }

        }
        public ActionResult Promocode()
        {
            return View();
        }

        public ActionResult CheckOut()
        {
            //lấy thông tin user từ Session khi khách hàng đã đắng nhập
            user user = Session["email"] as user;

            //lấy thông tin từ cart của khách hàng
            var cart = db.carts.Where(x => x.id_user == user.id_user).ToList();

            //tạo mới 1 order
            order orders = new order();

            //tạo mới customer
            customer customer = Session["Customer"] as customer;
            var cus = db.customers.Find(customer.id_customer);
            if (cus == null)
                db.customers.Add(customer);

            var ranks = db.ranks.ToList();
            foreach (var item2 in ranks.OrderBy(x => x.spend))
            {
                if (user.totalspend > (double)item2.spend)
                {
                    user.id_rank = item2.id_rank;
                }
            }

            db.SaveChanges();



            if (Session["Promo"] != null)
            {
                orders.id_promo = ((promocode)Session["Promo"]).id_promo;
            }

            //tạo mới thông tin cho order
            var user_spend = db.users.Where(x => x.id_user == user.id_user).FirstOrDefault();

            var total = (decimal)Session["Total"];
            var shipping = (int)Session["Shipping"];
            if (user_spend.id_rank != null)
            {
                var ranks_discount = db.ranks.Where(x => x.id_rank == user_spend.id_rank).FirstOrDefault();
                orders.total_price = total * (100 - ranks_discount.discount) / 100 + shipping;
                Session["Total_Price"] = orders.total_price;

                var discount_price = total * ranks_discount.discount / 100;
                Session["UserDiscount"] = discount_price;
            }
            else
            {
                orders.total_price = total + shipping;
                Session["Total_Price"] = orders.total_price;
            }



          


            user_spend.totalspend += (double)total;
            orders.id_customer = customer.id_customer;
            orders.shipping_fee = (int)Session["Shipping"];

            orders.id_user = user.id_user;
            orders.created_at = DateTime.Now;
            orders.payment_type = true;
            orders.request_cancel = false;
            orders.pending = false;
            orders.canceled = false;
            db.orders.Add(orders);

            //lấy thông tin từ list cart để cho vào order_item
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

            //xoá thông tin trong list cart sau khi đặt hàng thành công
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

