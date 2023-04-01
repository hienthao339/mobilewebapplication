using System;
using System.Collections.Generic;
using System.Data;
using System.IO.Ports;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.Mvc;
using System.Web.UI;
using WebApplication1.Models;
using WebApplication1.Models.Functions;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        MobileShoppingEntities db = new MobileShoppingEntities();
        //Add_Items
        public ActionResult Add_Items(int id, int add_quantity = 1)
        {

            user cus = Session["email"] as user;
            var carts = db.carts.Where(x => x.id_user == cus.id_user).ToList();
            var pro = db.products.Where(x => x.id_product == id).First();
            if (pro != null || pro.quantity >= add_quantity)
            {
                var cart = db.carts.Where(x => x.id_product == id && x.id_user == cus.id_user).FirstOrDefault();
                if (cart != null)
                {
                    cart cart_user = new cart()
                    {
                        id_product = id,
                        id_user = cus.id_user,
                        quantity = add_quantity + 1,
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
                        quantity = add_quantity,
                    };
                    var fcart = new Func_Cart();
                    fcart.Insert(cart_user);
                }
            }
            return RedirectToAction("Cart_User", "Cart");
        }
        //Cart_User
        public ActionResult Cart_User()
        {
            user cus = Session["email"] as user;
            if (cus != null)
            {
                var cart_user = db.carts.Where(x => x.id_user == cus.id_user).ToList();
                return View(cart_user);
            }
           return RedirectToAction("Cart_User","Cart");
        }
        //Update_Quantity
        public ActionResult UpdateQuanity(FormCollection form)
        {
            int id = int.Parse(form["ID_PRO_CART"]);
            int new_quantity = int.Parse(form["NEW_QUANTITY"]);
            int id_user = int.Parse(form["ID_USER"]);
            cart item = db.carts.Where(x => x.id_product == id && x.id_user == id_user).FirstOrDefault();
            if (item != null && item.quantity >= new_quantity)
            {
                cart carts = db.carts.Find(item.id_cart);
                carts.id_product = item.id_product;
                carts.id_user = item.id_user;
                carts.quantity = item.quantity;
                db.SaveChanges();
            }
            return RedirectToAction("Cart_User", "Cart");
        }
        //Cart/Remove_Item
        public ActionResult Remove_Item(int id)
        {
            user cus = Session["email"] as user;
            cart item = db.carts.Where(x => x.id_product == id && x.id_user == cus.id_user).FirstOrDefault();
            db.carts.Remove(item);
            db.SaveChanges();
            return RedirectToAction("Cart_User", "Cart");
        }
        //Quantity_Cart
        public PartialViewResult Quantity_Cart()
        {
            int quantity_cart = 0;
            List<cart> carts = new List<cart>().Where(x => x.id_user == (int)Session["ID_CUS"]).ToList();
            if (carts != null)
            {
                quantity_cart = (int)carts.Sum(x => x.id_product);
            }
            ViewBag.quantity_cart = quantity_cart;
            return PartialView("Quantity_Cart");
        }
        //Order_Success
        public ActionResult Order_Success()
        {
            return View();
        }
        //Order_CheckOut
        public ActionResult Order_CheckOut(FormCollection form)
        {
            try
            {
                var code = form["PROMO_CODE"];
                var promo = db.promocodes.Where(x => x.code == code).FirstOrDefault();
                List<cart> carts = new List<cart>().Where(x => x.id_user == (int)Session["ID_CUS"]).ToList();
                order orders = new order()
                {
                    id_user = (int)Session["ID_CUS"],
                    payment_type = true,
                    finished_at = null,
                    shipping_fee = 10,
                    total_price = 10,
                    id_promo = promo.id_promo,
                    pending = true,
                    delivering = false,
                    successed = false,
                    canceled = false,
                    paid = false,
                    created_at = DateTime.Now,
                   
                };
                var forder = new Func_Order();
                forder.Insert(orders);
                carts.Clear();
                return RedirectToAction("Order_Success", "Cart");
            }
            catch
            {
                return Content("Error Check Out. Please infomation of Customer...");
            }
        }
    }
}