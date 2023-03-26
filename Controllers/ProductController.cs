using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        MobileShoppingEntities db = new MobileShoppingEntities();
        public ActionResult OrderProduct()
        {
            int id = Convert.ToInt32(RouteData.Values["id"].ToString());
            if (Session["email"] == null)
            {
                Session["TrangTruoc"] = Request.RawUrl;
                return RedirectToAction("Gust", "User");
            }
            var listPro = db.products.Where(m => m.id_product == id).First();
            return View(listPro);
        }
        //public ActionResult OrderProduct(int quantity)
        //{
        //    int id_pro = Convert.ToInt32(RouteData.Values["id"].ToString());
        //    var products = db.products.Where(m => m.id_product == id_pro).First();
        //    var checkQuantity = db.products.Where(m => m.id_product == id_pro && m.quantity > quantity).First();
        //    if (checkQuantity != null)
        //    {
        //        var list_outofstock = db.orders.Where(m=>)
        //    }
        //}
    }

}