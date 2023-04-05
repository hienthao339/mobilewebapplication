using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        MobileShoppingEntities db = new MobileShoppingEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Pending()
        {
            var orders = db.orders.Where(x=>x.pending == false).ToList();
            return View(orders);
        }
    }
}