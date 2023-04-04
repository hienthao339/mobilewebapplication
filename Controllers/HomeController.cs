using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.Functions;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private MobileShoppingEntities db = new MobileShoppingEntities();
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("Index", "Admin");
            }
            if (Session["Account"] == null)
            {
                if (Request.Cookies["NameAccount"] != null)
                {
                    HttpCookie Email = Request.Cookies["NameAccount"];
                    HttpCookie Password = Request.Cookies["Password"];
                    var listAcc = db.users.Where(m => m.email == Email.Value && m.passwords == Password.Value).ToList();
                    if (listAcc.Count != 0)
                    {
                        user Account = listAcc.First();
                        Session["email"] = Account;
                    }
                }
            }
            List<product_seri> product_Seris = db.product_seri.ToList();
            return View(product_Seris);
        }
        public ActionResult SearchPage(string searching)
        {
            return View(db.product_seri.Where(x => x.name_seri.Contains(searching) || x.brand.Contains(searching) || searching == null).ToList());
        }
        public ActionResult SearchOrders(string searchOrders)
        {
            var cus = db.customers.Where(x=>x.phone == searchOrders || x.email == searchOrders).FirstOrDefault();
            return RedirectToAction("YourOrders", "Home", new { id = cus.id_customer });
        }
        public ActionResult SearchOrderPage()
        {
            return View();
        }
        public ActionResult YourOrders(int id)
        {
            var cus = db.customers.Where(x=>x.id_customer == id).FirstOrDefault();
            Session["Customer"] = cus;
            var order_items = db.order_item.Where(x=>x.order.id_customer == id).ToList();
            return View(order_items);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Details_Pro_Seri(int id)
        {
            var pro_seri = db.product_seri.Where(x=>x.id_product_seri == id).FirstOrDefault();
            var pro = db.products.Where(x => x.names == pro_seri.name_seri).ToList();
            return View(pro);
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Signin()
        {
            return View();
        }

        public ActionResult Signup()
        {
            return View();
        }

        public ActionResult Cart()
        {
            return View();
        }

        public ActionResult Product()
        {
            return View();
        }
        public ActionResult ProductList()
        {
            List<product> products = db.products.ToList();
            return View(products);
        }
        public ActionResult Checkout()
        {
            return View();
        }
        public ActionResult Search()
        {
            return View();
        }
    }
}