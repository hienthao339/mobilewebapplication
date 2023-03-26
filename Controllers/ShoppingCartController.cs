using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO.Ports;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.Functions;

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
        // phuong thuc add item vao gio hang

        public ActionResult AddToCart(int id)
        {

            var pro = db.products.SingleOrDefault(x => x.id_product == id);
            if (pro != null)
            {
                GetCart().Add(pro);
            }
            return RedirectToAction("ShowToCart", "ShoppingCart");
        }
        //public ActionResult AddToCart(int id)
        //{
        //    if (Session["email"] == null)
        //    {
        //        var pro = db.products.SingleOrDefault(x => x.id_product == id);
        //        if (pro != null)
        //        {
        //            GetCart().Add(pro);
        //        }
        //    }
        //    else if (Session["email"] != null)
        //    {
        //        user user_info = (user)Session["email"];
        //        var user_cart = new cart()
        //        {
        //            id_user_cart = user_info.id_user,
        //            id_product = id,
        //            quantity = 1,
        //        };
        //        var FCart = new Func_Cart();
        //        FCart.Insert(user_cart);
        //        db.SaveChanges();
        //    }
        //    return RedirectToAction("ShowToCart", "ShoppingCart");
        //}
        // trang gio hang
        // flashback
        public ActionResult ShowToCart()
        {
            if (Session["Cart"] == null)
            {
                return RedirectToAction("ShowToCart", "ShoppingCart");
            }
            Carts cart = Session["Cart"] as Carts;
            return View(cart);
        }
        //public ActionResult ShowToCart()
        //{
        //    if (Session["email"] == null)
        //    {
        //        if (Session["Cart"] == null)
        //        {
        //            return RedirectToAction("ShowToCart", "ShoppingCart");
        //        }
        //        Carts cart = Session["Cart"] as Carts;
        //        return View(cart);
        //    }
        //    else if (Session["email"] != null)
        //    {
        //        var user_info = (user)Session["email"];
        //        var user_info_cart = db.carts.Where(x => x.id_user_cart == user_info.id_user).FirstOrDefault();
        //        var user_cart = new cart
        //        {
        //            id_user_cart = user_info.id_user,
        //            id_product = user_info_cart.id_product,
        //            quantity = user_info_cart.quantity
        //        };
        //        return View(user_cart);
        //    }
        //    return View();
        //}
        public ActionResult Update_Quantity_Cart(FormCollection form)
        {
            Carts carts = Session["Cart"] as Carts;
            int id_pro = int.Parse(form["ID_PRO"]);
            int quantity = int.Parse(form["QUANTITY"]);
            carts.Update_Quantity_Shopping(id_pro, quantity);
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
                total_item = cart.Total_Quantity();
            }
            ViewBag.InfoCart = total_item;
            return PartialView("BagCart");
        }
        //public int Random_Code()
        //{
        //    Random rnd = new Random();
        //    int ID_CUSTOMER = rnd.Next(10);
        //    var list_cus = db.customers.Where(x => x.id_customer == ID_CUSTOMER).ToList();
        //    while (list_cus != null)
        //    {
        //        ID_CUSTOMER = rnd.Next(10);
        //        list_cus = db.customers.Where(x => x.id_customer == ID_CUSTOMER).ToList();
        //    }
        //    return ID_CUSTOMER;
        //}
        public ActionResult ShoppingSuccess()
        {
            return View();
        }
        public ActionResult CheckOut(FormCollection form)
        {
            try
            {
                Carts cart = Session["Cart"] as Carts;
                order orders = new order();
                orders.created_at = DateTime.Now;
                orders.id_customer = int.Parse(form["ID_CUSTOMER"]);
                orders.addresss = form["ADDRESS"];
                db.orders.Add(orders);
                foreach (var item in cart.Items)
                {
                    order_item order_items = new order_item();
                    order_items.id_order = orders.id_order;
                    order_items.id_product = item.Shopping_product.id_product;
                    order_items.uni_price = item.Shopping_product.price;
                    order_items.quantity = item.Shopping_product.quantity;
                    db.order_item.Add(order_items);
                }
                db.SaveChanges();
                cart.ClearCart();
                return RedirectToAction("ShoppingSuccess", "ShoppingCart");
            }
            catch
            {
                return Content("Error CheckOut. Please infomation of Customer...");
            }
        }
    }
}