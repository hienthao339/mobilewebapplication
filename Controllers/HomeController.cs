using DocumentFormat.OpenXml.Office2010.Excel;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WebApplication1.Extensions;
using WebApplication1.Models;
using WebApplication1.Models.Functions;
using WebGrease;
using WebGrease.Css.Extensions;

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
            var products = db.products.ToList();
            return View(products);
        }
        public ActionResult SearchPage(string searching, string brand, string color)
        {
            ViewBag.Brands = (from c in db.products select c.brand).Distinct().ToList();
            ViewBag.Colors = (from c in db.products select c.color).Distinct().ToList();
            if (searching != null)
            {
                return View(db.products.Where(x => x.names.Contains(searching) || x.brand.Contains(searching) || searching == null).ToList());
            }

            if (color == null && brand != null)
            {
                return View(db.products.Where(x => x.brand.Contains(brand)).ToList());
            }
            else if (brand == null && color != null)
            {
                return View(db.products.Where(x => x.color.Contains(color)).ToList());
            }
            else if (color != null && brand != null)
            {
                return View(db.products.Where(x => x.brand.Contains(brand) && x.color.Contains(color)).ToList());
            }
            else
            {
                return View(db.products.ToList());
            }
        }
        public ActionResult Filter(string brand, string color)
        {
            if (color == null && brand != null)
            {
                return View(db.products.Where(x => x.brand.Contains(brand)).ToList());
            }
            else if (brand == null && color != null)
            {
                return View(db.products.Where(x => x.color.Contains(color)).ToList());
            }
            else if (color != null && brand != null)
            {
                return View(db.products.Where(x => x.brand.Contains(brand) && x.color.Contains(color)).ToList());
            }
            return RedirectToAction("SearchPage", "Home");
        }
        public ActionResult SearchOrders(FormCollection form)
        {
            if (form["searchOrders"] != null && form["searchOrders"] != "")
            {
                var input = form["searchOrders"].ToString();
                var cus = db.customers.Where(x => x.phone == input || x.email == input).FirstOrDefault();
                if (cus != null)
                {
                    return RedirectToAction("YourOrders", "Home", new { id = cus.id_customer });
                }
                else
                {
                    this.AddNotification("We can not find your orders!", NotificationType.ERROR);
                    return RedirectToAction("SearchOrderPage", "Home");
                }
            }
            this.AddNotification("Please enter the email or phone number of the order !! ", NotificationType.WARNING);
            return RedirectToAction("SearchOrderPage", "Home");
        }
        public ActionResult SearchOrderPage()
        {
            return View();
        }
        public ActionResult YourOrders(int id)
        {
            var cus = db.customers.Where(x => x.id_customer == id).FirstOrDefault();
            Session["Customer"] = cus;
            var orders = db.orders.Where(x => x.id_customer == cus.id_customer).ToList();
            //var order_items = db.order_item.Where(x=>x.order.id_customer == id).ToList();
            return View(orders);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Details_Pro(int id)
        {
            var pro_seri = db.products.Where(x => x.id_product == id).FirstOrDefault();
            var pro = db.products.Where(x => x.names == pro_seri.names).ToList();
            ViewBag.Ram = (from c in db.products select c.ram).Distinct().ToList();
            ViewData["pro"] = pro;

            return View(pro_seri);
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            order order = db.orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
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
        public ActionResult Request_Cancel(int id)
        {
            var ord = db.orders.Where(x => x.id_order == id).FirstOrDefault();
            ord.request_cancel = true;
            db.SaveChanges();
            return RedirectToAction("YourOrders", "Home", new { id = ord.id_customer });

        }
        public ActionResult ProductList(string brand)
        {
            ViewBag.Brands = (from c in db.products select c.brand).Distinct().ToList();
            List<product> products = db.products.Where(x => x.brand == brand).ToList();
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
        public ActionResult Chats()
        {
            return View();
        }
        public ActionResult Order()
        {
            return View();
        }
    }
}