using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Office2010.Excel;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WebApplication1.Controllers.Admin;
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

            var find_cus = db.customers.Where(x => x.email == cus.email).ToList();

            List<order> ord = new List<order>();

            foreach (var item in find_cus)
            {
                var find_ord = db.orders.Where(x => x.id_customer == item.id_customer).ToList();
                foreach (var item2 in find_ord)
                {
                    ord.Add(item2);
                }
            }

            //Session["Customer"] = cus;
            //var orders = db.orders.Where(x => x.id_customer == cus.id_customer).ToList();
            //var order_items = db.order_item.Where(x=>x.order.id_customer == id).ToList();
            return View(ord);
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

            var count = db.feedbacks.Count(x => x.id_product == id);
            var feedbacks = db.feedbacks.Where(x => x.id_product == id).ToList();
         
            int star = 0;
            int one = 0;
            int two = 0;
            int three = 0;
            int four = 0;
            int five = 0;
            foreach (var item in feedbacks)
            {
                if (item.rate == 1)
                    one += 1;
                if (item.rate == 2)
                    two += 1;
                if (item.rate == 3)
                    three += 1;
                if (item.rate == 4)
                    four += 1;
                if (item.rate == 5)
                    five += 1;

                star += (int)item.rate;
            }

            Session["rate1"] = one;
            Session["rate2"] = two;
            Session["rate3"] = three;
            Session["rate4"] = four;
            Session["rate5"] = five;


            double allstar = one + two + three + four + five;
            double avg_1 = one / allstar * 100;
            double avg_2 = two / allstar * 100;
            double avg_3 = three / allstar * 100;
            double avg_4 = four / allstar * 100;
            double avg_5 = five / allstar * 100;

            Session["1"] = Math.Round(avg_1, 2) + "%";
            Session["2"] = Math.Round(avg_2, 2) + "%";
            Session["3"] = Math.Round(avg_3, 2) + "%";
            Session["4"] = Math.Round(avg_4, 2) + "%";
            Session["5"] = Math.Round(avg_5, 2) + "%";

            double avg = (double)star / (double)count;
            int avg2 = star / count;
            Session["avg2"] = avg2;
            avg = Math.Round(avg, 1);
            Session["count"] = count;
            Session["star"] = star;
            Session["avg"] = avg;


            ViewData["feedback"] = feedbacks;

            return View(pro_seri);
        }

        public ActionResult Feedbacks(string comment, int rating, int id_pro)
        {
            var content = comment;
            feedback new_fb = new feedback();
            new_fb.content = content;
            new_fb.created_at = DateTime.Now;
            int id = id_pro;
            if (Session["email"] == null)
            {
                this.AddNotification("You must be logged in to rate", NotificationType.ERROR);
                return RedirectToAction("Details_Pro", "Home", new { id = id_pro });
            }
            user user = Session["email"] as user;
            new_fb.id_user = user.id_user;
            new_fb.id_product = id;
            if(rating == 0)
            {
                this.AddNotification("You must be rating before review", NotificationType.ERROR);
                return RedirectToAction("Details_Pro", "Home", new { id = id_pro });
            }
            new_fb.rate = rating;
            db.feedbacks.Add(new_fb);
            db.SaveChanges();
            return RedirectToAction("Details_Pro", "Home", new { id = id_pro });
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