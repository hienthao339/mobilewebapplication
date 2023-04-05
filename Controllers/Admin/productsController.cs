using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers.Admin
{
    public class productsController : Controller
    {
        private MobileShoppingEntities db = new MobileShoppingEntities();

        // GET: products
        public ActionResult Index()
        {
            var products = db.products.Include(p => p.promocode);
            return View(products.ToList());
        }

        // GET: products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: products/Create
        public ActionResult Create()
        {
            ViewBag.id_promo = new SelectList(db.promocodes, "id_promo", "code");
            return View();
        }

        // POST: products/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "id_product,names,images,price,display,weights,water_resistance,operating_system,processor,battery,ram,quantity,rate,id_promo,color,brand")] product product)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.products.Add(product);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.id_promo = new SelectList(db.promocodes, "id_promo", "code", product.id_promo);
        //    return View(product);
        //}

        // GET: products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_promo = new SelectList(db.promocodes, "id_promo", "code", product.id_promo);
            return View(product);
        }

        // POST: products/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "id_product,names,images,price,display,weights,water_resistance,operating_system,processor,battery,ram,quantity,rate,id_promo,color,brand")] product product)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(product).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.id_promo = new SelectList(db.promocodes, "id_promo", "code", product.id_promo);
        //    return View(product);
        //}

        // GET: products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            product product = db.products.Find(id);
            db.products.Remove(product);
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

        //Tải hình ảnh
        #region UploadImage
        [HttpPost]
        public ActionResult Create(product product, HttpPostedFileBase uploadimage)
        {
            db.products.Add(product);
            db.SaveChanges();

            if (uploadimage != null && uploadimage.ContentLength > 0)
            {
                int id = int.Parse(db.products.ToList().Last().id_product.ToString());

                string _FileName = "";
                int index = uploadimage.FileName.IndexOf('.');
                _FileName = id.ToString() + "." + uploadimage.FileName.Substring(index + 1);
                string _path = Path.Combine(Server.MapPath("~/wwwroot/Images/Products/"), _FileName);
                uploadimage.SaveAs(_path);

                product pd = db.products.FirstOrDefault(x => x.id_product == id);
                pd.images = _FileName;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(product product, HttpPostedFileBase uploadimage)
        {
            //t cũng k biết m để làm gì, t để zo cho đủ tụ
            ViewBag.id_promo = new SelectList(db.promocodes, "id_promo", "code", product.id_promo);

            product upd = db.products.FirstOrDefault(x => x.id_product == product.id_product);
            upd.names = product.names;
            upd.price = product.price;
            upd.display = product.display;
            upd.weights = product.weights;
            upd.water_resistance = product.water_resistance;
            upd.operating_system = product.operating_system;
            upd.processor = product.processor;
            upd.battery = product.battery;
            upd.ram = product.ram;
            upd.quantity = product.quantity;
            upd.rate = product.rate;
            upd.rate = product.rate;
            upd.color = product.color;
            upd.brand = product.brand;
            upd.id_promo = product.id_promo;

            if (uploadimage != null && uploadimage.ContentLength > 0)
            {
                int id = product.id_product;

                string _FileName = "";
                int index = uploadimage.FileName.IndexOf('.');
                _FileName = id.ToString() + "." + uploadimage.FileName.Substring(index + 1);
                string _path = Path.Combine(Server.MapPath("~/wwwroot/Images/Products/"), _FileName);
                uploadimage.SaveAs(_path);
                upd.images = _FileName;
            }

            db.SaveChanges();

            return RedirectToAction("Index");
        }
        #endregion
    }
}
