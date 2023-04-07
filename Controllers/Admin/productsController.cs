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
using WebApplication1.Models.Functions;

namespace WebApplication1.Controllers.Admin
{
    public class productsController : Controller
    {
        private MobileShoppingEntities db = new MobileShoppingEntities();

        // GET: products
        public ActionResult Index()
        {
            var products = db.products.ToList();
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
            return View();
        }

        // POST: products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(product product, HttpPostedFileBase images)
        {
            if (ModelState.IsValid)
            {
                product pro = new product();
                pro.id_product = product.id_product;
                pro.price = product.price;
                pro.quantity = product.quantity;
                pro.rate = product.rate;
                pro.water_resistance = product.water_resistance;
                pro.operating_system = product.operating_system;
                pro.weights = product.weights;
                pro.battery = product.battery;
                pro.brand = product.brand;
                pro.color = product.color;
                pro.ram = product.ram;
                pro.display = product.display;
                pro.id_promo = product.id_promo;
                pro.processor = product.processor;
                pro.names = product.names;
                if (images != null && images.ContentLength > 0)
                {
                    int id = product.id_product;
                    string name = product.names;
                    string color = product.color;
                    string ram = product.ram;

                    string File_name = "";
                    int index = images.FileName.IndexOf('.');
                    File_name = "pro" + "_" + id.ToString() + "." + images.FileName.Substring(index + 1);
                    string path = Path.Combine(Server.MapPath("~/wwwroot/Images/Products"), File_name);
                    images.SaveAs(path);

                    pro.images = File_name;
                }
                var fpro = new Func_Product();
                fpro.Insert(pro);
                return RedirectToAction("Index");
            }
            return View(product);
        }

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
            return View(product);
        }

        // POST: products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(product product, HttpPostedFileBase images)
        {
            if (ModelState.IsValid)
            {
                product pro = new product();
                pro.id_product = product.id_product;
                pro.price = product.price;
                pro.quantity = product.quantity;
                pro.rate = product.rate;
                pro.water_resistance = product.water_resistance;
                pro.operating_system = product.operating_system;
                pro.weights = product.weights;
                pro.battery = product.battery;
                pro.brand = product.brand;
                pro.color = product.color;
                pro.ram = product.ram;
                pro.display = product.display;
                pro.id_promo = product.id_promo;
                pro.processor = product.processor;
                pro.names = product.names;
                if (images != null && images.ContentLength > 0)
                {
                    int id = product.id_product;

                    string File_name = "";
                    int index = images.FileName.IndexOf('.');
                    File_name = "pro" +"_"+ id.ToString()+"." + images.FileName.Substring(index + 1);
                    string path = Path.Combine(Server.MapPath("~/wwwroot/Images/Products"), File_name);
                    images.SaveAs(path);

                    pro.images = File_name;
                }
                var fpro = new Func_Product();
                fpro.Update(pro);
                return RedirectToAction("Index");
            }
            return View(product);
        }

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
    }
}
