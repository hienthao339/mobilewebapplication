using PagedList;
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
using WebApplication1.Models.ViewModels;

namespace WebApplication1.Controllers.Admin
{
    public class productsController : Controller
    {
        private MobileShoppingEntities db = new MobileShoppingEntities();

        // GET: products
        public ActionResult Index(int? page)
        {
            // 1. Tham số int? dùng để thể hiện null và kiểu int( số nguyên)
            // page có thể có giá trị là null ( rỗng) và kiểu int.

            // 2. Nếu page = null thì đặt lại là 1.
            if (page == null) page = 1;

            // 3. Tạo truy vấn sql, lưu ý phải sắp xếp theo trường nào đó, ví dụ OrderBy
            // theo Masp mới có thể phân trang.
            var sp = db.products.OrderBy(x => x.id_product);

            // 4. Tạo kích thước trang (pageSize) hay là số sản phẩm hiển thị trên 1 trang
            int pageSize = 10;

            // 4.1 Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            int pageNumber = (page ?? 1);

            // 5. Trả về các sản phẩm được phân trang theo kích thước và số trang.
            return View(sp.ToPagedList(pageNumber, pageSize));
        }

        //public ActionResult Index()
        //{
        //    var products = db.products.ToList();
        //    return View(products.ToList());
        //}

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
                var pro = db.products.Where(x => x.id_product == product.id_product).FirstOrDefault();
                if (images != null)
                {
                    int id = product.id_product;

                    string File_name = "";
                    int index = images.FileName.IndexOf('.');
                    File_name = id.ToString() + "." + images.FileName.Substring(index + 1);
                    string path = Path.Combine(Server.MapPath("~/wwwroot/Images/Products"), File_name);
                    images.SaveAs(path);

                    product.images = File_name;
                }
                else if (images == null)
                {
                    product.images = null;
                }
               
                var fpro = new Func_Product();
                fpro.Insert(product);


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
                var pro = db.products.Where(x => x.id_product == product.id_product).FirstOrDefault();
                if (images != null )
                {
                    int id = product.id_product;

                    string File_name = "";
                    int index = images.FileName.IndexOf('.');
                    File_name = id.ToString() + "." + images.FileName.Substring(index + 1);
                    string path = Path.Combine(Server.MapPath("~/wwwroot/Images/Products"), File_name);
                    images.SaveAs(path);

                    product.images = File_name;
                }
                if (product.images == null)
                {
                    product.images = pro.images;
                }
                if (product.promocode.code != null)
                {
                    var promo = db.promocodes.Where(x => x.code == product.promocode.code).FirstOrDefault();
                    product.id_promo= promo.id_promo;
                    product.discount = pro.price.Value * (100 - promo.discount_price) / 100;
                }

                var fpro = new Func_Product();
                fpro.Update(product);


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
