using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor.Text;
using System.Web.UI.WebControls;
using WebApplication1.Extensions;
using WebApplication1.Models;
using WebApplication1.Models.Functions;
using WebApplication1.Models.ViewModels;

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
            if (cart != null)
            {
                var pro = cart.Sum(x => x.quantity);
                Session["Quantity_pro"] = pro;
                var total_cost = cart.Sum(x => x.product.price * x.quantity);
                Session["TotalCost"] = total_cost;
                var shippingfee = 10 + cart.Sum(x => x.quantity * 2);
                Session["Shipping"] = shippingfee;
                var total_order = shippingfee + total_cost;
                Session["Total_order"] = total_order;
            }
            return View(cart);
        }
        public ActionResult Details_Pro(int id)
        {
            var pro = db.products.Where(x => x.id_product == id).First();
            return View(pro);
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
            return RedirectToAction("Details_Pro", "Shopping", new { id = id });
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
        //public ActionResult Update_Cart_Quantity(FormCollection form)
        //{
        //    user user = Session["email"] as user;
        //    int id_pro = int.Parse(form["ID_PRO_CART"]);
        //    int quantity = int.Parse(form["NEW_QUANTITY"]);
        //    var carts = db.carts.Where(x => x.id_user == user.id_user && x.id_product == id_pro).FirstOrDefault();

        //    var cart = new cart()
        //    {
        //        id_cart = carts.id_cart,
        //        quantity = quantity,
        //        id_user = user.id_user,
        //        id_product = id_pro,
        //    };
        //    var fcart = new Func_Cart();
        //    fcart.Update(cart);
        //    return RedirectToAction("ShowToCart", "Shopping");
        //}
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

        //public ActionResult CheckOutInfo(FormCollection form) 
        //{

        //}
        public ActionResult btn_Increase(int id)
        {
            user user = Session["email"] as user;
            var cart = db.carts.Where(x => x.id_product == id && x.id_user == user.id_user).FirstOrDefault();
            var pro = db.products.Where(x => x.id_product == id).FirstOrDefault();
            if (pro.quantity >= 1 && cart.quantity <= pro.quantity)
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
            try
            {
                user user = Session["email"] as user;
                customer customer = new customer();

                customer.phone = form["phone"];
                customer.addresss = form["address"] + ",";
                customer.ward = form["ward"] + ",";
                customer.district = form["district"] + ",";
                customer.city = form["city"];
                customer.email = form["email"];

                db.customers.Add(customer);
                db.SaveChanges();

                var cart = db.carts.Where(x => x.id_user == user.id_user).ToList();
                order orders = new order();
                orders.id_customer = customer.id_customer;
                orders.id_user = user.id_user;
                orders.created_at = DateTime.Now;
                orders.payment_type = true;
                orders.finished_at = null;
                orders.shipping_fee = Convert.ToInt32(Session["Shipping"]);
                orders.total_price = Convert.ToDecimal(Session["Total_order"]);
                orders.id_promo = null;
                orders.pending = true;
                orders.delivering = false;
                orders.successed = false;
                orders.canceled = false;
                orders.paid = false;
                db.orders.Add(orders);
                db.SaveChanges();

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
                db.SaveChanges();
                foreach (var item in cart)
                {
                    cart model = db.carts.Where(x => x.id_cart == item.id_cart).FirstOrDefault();
                    db.carts.Remove(model);
                    db.SaveChanges();
                }
                return RedirectToAction("ShoppingSuccess", "Shopping", new { id = orders.id_order });
            }
            catch
            {
                return Content("Error Check Out. Please infomation of Customer...");
            }
        }
    }
}