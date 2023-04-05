using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO.Ports;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WebApplication1.Extensions;
using WebApplication1.Models;
using WebApplication1.Models.Functions;
using WebGrease.ImageAssemble;

namespace WebApplication1.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCart
        MobileShoppingEntities db = new MobileShoppingEntities();
        public Carts GetCart()
        {
            Carts cart = Session["Cart"] as Carts;
            if (cart == null || Session["Cart"] == null)
            {
                cart = new Carts();
                Session["Cart"] = cart;
            }
            return cart;
        }
        public ActionResult AddToCart(int id)
        {
            this.AddNotification("Item has been added to your card !", NotificationType.SUCCESS);
            var pro = db.products.SingleOrDefault(x => x.id_product == id);
            Carts carts = Session["Cart"] as Carts;
            if (pro != null)
            {
                GetCart().Add(pro);
            }
            return RedirectToAction("Details_Pro", "Shopping", new { id = id });
        }
        // trang gio hang
        // flashback
        public ActionResult ShowToCart()
        {
            if (Session["Cart"] == null)
            {
                Carts carts = Session["Cart"] as Carts;
                return View(carts);
            }
            Carts cart = Session["Cart"] as Carts; cart = Session["Cart"] as Carts;
            if (cart != null)
            {
                var pro = cart.Items.Sum(x => x.Shopping_quantity);
                Session["Quantity_pro"] = pro;
                var total_cost = cart.Items.Sum(x => x.Shopping_product.price * x.Shopping_quantity);
                Session["TotalCost"] = total_cost;
                var shippingfee = 10 + cart.Items.Sum(x => x.Shopping_quantity * 2);
                Session["Shipping"] = shippingfee;
                var total_order = shippingfee + total_cost;
                Session["Total_order"] = total_order;
            }
            return View(cart);
        }

        public ActionResult BuyNow(int id)
        {
            var pro = db.products.SingleOrDefault(x => x.id_product == id);
            Carts carts = Session["Cart"] as Carts;
            if (pro != null)
            {
                GetCart().Add(pro);
            }
            return RedirectToAction("ShowToCart", "ShoppingCart");
        }

        public ActionResult btn_Decrease(int id)
        {
            Carts cart = Session["Cart"] as Carts;
            cart.btn_Decrease(id);
            return RedirectToAction("ShowToCart", "ShoppingCart");
        }
        public ActionResult btn_Increase(int id)
        {
            Carts cart = Session["Cart"] as Carts;
            cart.btn_Increase(id);
            return RedirectToAction("ShowToCart", "ShoppingCart");
        }
        //RemoveCart
        public ActionResult RemoveCart(int id)
        {
            Carts cart = Session["Cart"] as Carts;
            cart.Remove_CartItem(id);
            return RedirectToAction("ShowToCart", "ShoppingCart");
        }
        public PartialViewResult BagCart()
        {
            int total_item = 0;
            Carts cart = Session["Cart"] as Carts;
            if (cart != null)
            {
                total_item = (int)cart.Total_Quantity();
            }
            ViewBag.InfoCart = total_item;
            return PartialView("BagCart");
        }


        public ActionResult ShoppingSuccess(int id)
        {
            var list = db.order_item.Where(x => x.id_order == id).ToList();
            return View(list);
        }
        public ActionResult CheckOut(FormCollection form)
        {
            try
            {
                Carts cart = Session["Cart"] as Carts;
                customer customer = new customer();

                customer.phone = form["phone"];
                customer.addresss = form["address"] + ",";
                customer.ward = form["ward"] + ",";
                customer.district = form["district"] + ",";
                customer.city = form["city"];
                customer.email = form["email"];

                db.customers.Add(customer);
                db.SaveChanges();

                order orders = new order();
                orders.id_customer = customer.id_customer;
                orders.created_at = DateTime.Now;
                orders.payment_type = true;
                orders.finished_at = null;
                orders.shipping_fee = Convert.ToInt32(Session["Shipping"]);
                orders.total_price = Convert.ToDecimal(Session["Total_order"]);
                orders.id_promo = null;
                orders.pending = false;
                orders.delivering = false;
                orders.successed = false;
                orders.canceled = false;
                orders.paid = false;
                db.orders.Add(orders);
                db.SaveChanges();

                customer customers = Session["Customer"] as customer;
                Session["Customer"] = customer;

                ViewBag.order_guest = orders.id_order;

                foreach (var item in cart.Items)
                {
                    product products = db.products.Find(item.Shopping_product.id_product);
                    int quantity_pro = (int)products.quantity - item.Shopping_quantity;
                    products.quantity = quantity_pro;
                    db.SaveChanges();
                    order_item order_items = new order_item();
                    order_items.id_order = orders.id_order;
                    order_items.id_product = item.Shopping_product.id_product;
                    order_items.quantity = item.Shopping_quantity;
                    db.order_item.Add(order_items);
                }
                db.SaveChanges();
                cart.ClearCart();
                return RedirectToAction("ShoppingSuccess", "Shopping", new {id = orders.id_order});
            }
            catch
            {
                return Content("Error Check Out. Please infomation of Customer...");
            }
        }
    }
}