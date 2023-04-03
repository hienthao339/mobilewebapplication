using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers.Admin
{
    public class ordersController : Controller
    {
        private MobileShoppingEntities db = new MobileShoppingEntities();

        // GET: orders
        public ActionResult Index()
        {
            var orders = db.orders.Include(o => o.promocode).Include(o => o.user).Include(o => o.customer);
            return View(orders.ToList());
        }

        // GET: orders/Details/5
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

        // GET: orders/Create
        public ActionResult Create()
        {
            ViewBag.id_promo = new SelectList(db.promocodes, "id_promo", "code");
            ViewBag.id_user = new SelectList(db.users, "id_user", "names");
            ViewBag.id_customer = new SelectList(db.customers, "id_customer", "email");
            return View();
        }

        // POST: orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_order,id_user,payment_type,created_at,started_at,finished_at,shipping_fee,total_price,id_promo,pending,delivering,successed,canceled,paid,id_customer")] order order)
        {
            if (ModelState.IsValid)
            {
                db.orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_promo = new SelectList(db.promocodes, "id_promo", "code", order.id_promo);
            ViewBag.id_user = new SelectList(db.users, "id_user", "names", order.id_user);
            ViewBag.id_customer = new SelectList(db.customers, "id_customer", "email", order.id_customer);
            return View(order);
        }

        // GET: orders/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.id_promo = new SelectList(db.promocodes, "id_promo", "code", order.id_promo);
            ViewBag.id_user = new SelectList(db.users, "id_user", "names", order.id_user);
            ViewBag.id_customer = new SelectList(db.customers, "id_customer", "email", order.id_customer);
            return View(order);
        }

        // POST: orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_order,id_user,payment_type,created_at,started_at,finished_at,shipping_fee,total_price,id_promo,pending,delivering,successed,canceled,paid,id_customer")] order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_promo = new SelectList(db.promocodes, "id_promo", "code", order.id_promo);
            ViewBag.id_user = new SelectList(db.users, "id_user", "names", order.id_user);
            ViewBag.id_customer = new SelectList(db.customers, "id_customer", "email", order.id_customer);
            return View(order);
        }

        // GET: orders/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            order order = db.orders.Find(id);
            db.orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
