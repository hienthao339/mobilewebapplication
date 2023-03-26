using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CustomerHomeController : Controller
    {
        // GET: CustomerHome
        MobileShoppingEntities db = new MobileShoppingEntities();
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("Index", "Admin");
            }
            if (Session["Account"] == null)
            {
                HttpCookie Email = Request.Cookies["Email"];
                HttpCookie Password = Request.Cookies["Password"];
                var listACC = db.users.Where(m => m.email == Email.Value && m.passwords == Password.Value).ToList();
                if (listACC.Count != 0)
                {
                    user Account = listACC.First();
                    Session["email"] = Account;
                }
            }
            List<product> products = db.products.ToList();
            return View(products);
        }
    }
}