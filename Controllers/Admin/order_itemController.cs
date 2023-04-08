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
    public class order_itemController : Controller
    {
        private MobileShoppingEntities db = new MobileShoppingEntities();

        // GET: order_item
        public ActionResult Index()
        {
            var order_item = db.order_item.ToList();
            return View(order_item.ToList());
        }

        // GET: order_item/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            order_item order_item = db.order_item.Find(id);
            if (order_item == null)
            {
                return HttpNotFound();
            }
            return View(order_item);
        }

        // GET: order_item/Create
        public ActionResult Create()
        {
            ViewBag.id_order_item = new SelectList(db.feedbacks, "id_feedback_order_item", "content");
            ViewBag.id_order = new SelectList(db.orders, "id_order", "id_order");
            ViewBag.id_product = new SelectList(db.products, "id_product", "names");
            return View();
        }

        // POST: order_item/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_order_item,id_order,id_product,quantity")] order_item order_item)
        {
            if (ModelState.IsValid)
            {
                db.order_item.Add(order_item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_order_item = new SelectList(db.feedbacks, "id_feedback_order_item", "content", order_item.id_order_item);
            ViewBag.id_order = new SelectList(db.orders, "id_order", "id_order", order_item.id_order);
            ViewBag.id_product = new SelectList(db.products, "id_product", "names", order_item.id_product);
            return View(order_item);
        }

        // GET: order_item/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            order_item order_item = db.order_item.Find(id);
            if (order_item == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_order_item = new SelectList(db.feedbacks, "id_feedback_order_item", "content", order_item.id_order_item);
            ViewBag.id_order = new SelectList(db.orders, "id_order", "id_order", order_item.id_order);
            ViewBag.id_product = new SelectList(db.products, "id_product", "names", order_item.id_product);
            return View(order_item);
        }

        // POST: order_item/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_order_item,id_order,id_product,quantity")] order_item order_item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order_item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_order_item = new SelectList(db.feedbacks, "id_feedback_order_item", "content", order_item.id_order_item);
            ViewBag.id_order = new SelectList(db.orders, "id_order", "id_order", order_item.id_order);
            ViewBag.id_product = new SelectList(db.products, "id_product", "names", order_item.id_product);
            return View(order_item);
        }

        // GET: order_item/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            order_item order_item = db.order_item.Find(id);
            if (order_item == null)
            {
                return HttpNotFound();
            }
            return View(order_item);
        }

        // POST: order_item/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            order_item order_item = db.order_item.Find(id);
            db.order_item.Remove(order_item);
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
