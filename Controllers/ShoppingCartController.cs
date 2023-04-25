using DocumentFormat.OpenXml.Vml;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WebApplication1.Extensions;
using WebApplication1.Models;

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
            return RedirectToAction("Details_Pro", "Home", new { id = id });
        }
        // trang gio hang
        // flashback
        public ActionResult ShowToCart()
        {
            if (Session["Cart"] == null)
            {
                Carts carts = Session["Cart"] as Carts;
                return View();
            }
            Carts cart = Session["Cart"] as Carts;
            foreach (var item in cart.Items)
            {
                var pro = db.products.Where(x => x.id_product == item.Shopping_product.id_product).FirstOrDefault();
                if (pro.quantity < 1)
                {
                    Session["CheckPro"] = 1;
                    this.AddNotification(pro.names + " out of stock !", NotificationType.WARNING);

                }
                if (pro.quantity < item.Shopping_quantity)
                {
                    Session["CheckPro"] = 1;
                    this.AddNotification(pro.names + " not enough quantity !", NotificationType.WARNING);

                }
            }
            if (cart != null)
            {
                int Quantity = (int)cart.Items.Sum(x => x.Shopping_quantity);
                Session["Quantity"] = Quantity;
                decimal Temp_Total = 0;
                foreach (var item in cart.Items)
                {
                    if (item.Shopping_product.id_promo == null)
                    {
                        Temp_Total += (decimal)(item.Shopping_product.price * item.Shopping_quantity);
                    }
                    else
                    {
                        Temp_Total += (decimal)(item.Shopping_product.discount * item.Shopping_quantity);
                    }
                }
                Session["Total"] = Temp_Total;
                int Shipping = (int)Session["Quantity"] * 2;
                Session["Shipping"] = Shipping + 10;

            }
            foreach (var item in cart.Items)
            {
                var pro = db.products.Where(x => x.id_product == item.Shopping_product.id_product).FirstOrDefault();
                if (pro.quantity < 1)
                {
                    Session["CheckPro"] = 1;
                    this.AddNotification(pro.names + " out of stock !", NotificationType.WARNING);
                    return RedirectToAction("ShowToCart", "Shopping");
                }
                if (pro.quantity < item.Shopping_quantity)
                {
                    item.Shopping_quantity = (int)pro.quantity;
                    db.SaveChanges();
                    this.AddNotification(pro.names + " quantity has been edited", NotificationType.WARNING);
                    return RedirectToAction("ShowToCart", "Shopping");
                }
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


            Carts cart = Session["Cart"] as Carts;

            order orders = new order();

            customer customer = Session["Customer"] as customer;
            var cus = db.customers.Find(customer.id_customer);
            if (cus == null)
                db.customers.Add(customer);

            var total = (decimal)Session["Total"];
            var shipping = (int)Session["Shipping"];

            orders.id_customer = customer.id_customer;
            orders.created_at = DateTime.Now;
            orders.payment_type = true;
            orders.shipping_fee = (int)Session["Shipping"];
            orders.total_price = total + shipping;
            Session["Total_Price"] = total + shipping;
            orders.id_promo = null;
            orders.pending = false;
            orders.canceled = false;
            orders.request_cancel = false;
            db.orders.Add(orders);
            db.SaveChanges();


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
            return RedirectToAction("ShoppingSuccess", "Shopping", new { id = orders.id_order });


        }
    }
}