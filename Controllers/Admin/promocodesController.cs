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
    public class promocodesController : Controller
    {
        private MobileShoppingEntities db = new MobileShoppingEntities();

        // GET: promocodes
        public ActionResult Index()
        {
            return View(db.promocodes.ToList());
        }

        // GET: promocodes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            promocode promocode = db.promocodes.Find(id);
            if (promocode == null)
            {
                return HttpNotFound();
            }
            return View(promocode);
        }

        // GET: promocodes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: promocodes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_promo,code,created_at,started_at,finished_at,discount_price")] promocode promocode)
        {
            if (ModelState.IsValid)
            {
                db.promocodes.Add(promocode);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(promocode);
        }

        // GET: promocodes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            promocode promocode = db.promocodes.Find(id);
            if (promocode == null)
            {
                return HttpNotFound();
            }
            return View(promocode);
        }

        // POST: promocodes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_promo,code,created_at,started_at,finished_at,discount_price")] promocode promocode)
        {
            if (ModelState.IsValid)
            {
                db.Entry(promocode).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(promocode);
        }

        // GET: promocodes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            promocode promocode = db.promocodes.Find(id);
            if (promocode == null)
            {
                return HttpNotFound();
            }
            return View(promocode);
        }

        // POST: promocodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            promocode promocode = db.promocodes.Find(id);
            db.promocodes.Remove(promocode);
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
