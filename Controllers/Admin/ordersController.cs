using DocumentFormat.OpenXml.Office2010.PowerPoint;
using DocumentFormat.OpenXml.Vml.Office;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Extensions;
using WebApplication1.Models;

namespace WebApplication1.Controllers.Admin
{
    public class ordersController : Controller
    {
        private MobileShoppingEntities db = new MobileShoppingEntities();

        public ActionResult BagCartOrders()
        {
            int total_orders = db.orders.Count(x => x.pending == false);
            ViewBag.Total_orders = total_orders;
            return PartialView("BagCartOders");
        }
        // GET: orders
        public ActionResult Index(int? page)
        {
            if (page == null) page = 1;

            // 3. Tạo truy vấn sql, lưu ý phải sắp xếp theo trường nào đó, ví dụ OrderBy
            // theo Masp mới có thể phân trang.
            //var sp = db.products.OrderBy(x => x.id_product);
            var sp = db.orders.Include(o => o.customer).Include(o => o.promocode).Include(o => o.user).ToList();
            // 4. Tạo kích thước trang (pageSize) hay là số sản phẩm hiển thị trên 1 trang
            int pageSize = 10;

            // 4.1 Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            int pageNumber = (page ?? 1);

            // 5. Trả về các sản phẩm được phân trang theo kích thước và số trang.
            return View(sp.ToPagedList(pageNumber, pageSize));
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
        //public ActionResult Create()
        //{
        //    ViewBag.id_customer = new SelectList(db.customers, "id_customer", "email");
        //    ViewBag.id_promo = new SelectList(db.promocodes, "id_promo", "code");
        //    ViewBag.id_user = new SelectList(db.users, "id_user", "names");
        //    return View();
        //}

        //// POST: orders/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "id_order,id_customer,payment_type,created_at,started_at,finished_at,shipping_fee,total_price,id_promo,pending,onprocess,completed,canceled,paid,id_user")] order order)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.orders.Add(order);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.id_customer = new SelectList(db.customers, "id_customer", "email", order.id_customer);
        //    ViewBag.id_promo = new SelectList(db.promocodes, "id_promo", "code", order.id_promo);
        //    ViewBag.id_user = new SelectList(db.users, "id_user", "names", order.id_user);
        //    return View(order);
        //}

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
            ViewBag.id_customer = new SelectList(db.customers, "id_customer", "email", order.id_customer);
            ViewBag.id_promo = new SelectList(db.promocodes, "id_promo", "code", order.id_promo);
            ViewBag.id_user = new SelectList(db.users, "id_user", "names", order.id_user);
            return View(order);
        }

        // POST: orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(order order)
        {

            //db.Entry(order).State = EntityState.Modified;

            var ord = db.orders.Find(order.id_order);
            ord.id_order = order.id_order;
            ord.id_customer = order.id_customer;
            ord.id_user = order.id_user;
            ord.id_promo = order.id_promo;
            ord.total_price = order.total_price;
            ord.created_at = order.created_at;
            ord.finished_at = order.finished_at;

            if(order.pending == true)
            {
                ord.pending = order.pending;
                ord.started_at = DateTime.Now;
                if(order.canceled == true)
                {
                    this.AddNotification("Your order has been solved !", NotificationType.WARNING);
                    return RedirectToAction("Edit", "orders");
                }
            }
            else
            {
                ord.started_at = order.started_at;
                ord.pending = order.pending;
                ord.canceled = order.canceled;
                if (order.canceled == true)
                {
                    ord.pending = null;
                }
            }

            ord.started_at = order.started_at;
            ord.shipping_fee = order.shipping_fee;
            ord.completed = order.completed;
            ord.payment_type = order.payment_type;

            db.SaveChanges();

            ViewBag.id_customer = new SelectList(db.customers, "id_customer", "email", order.id_customer);
            ViewBag.id_promo = new SelectList(db.promocodes, "id_promo", "code", order.id_promo);
            ViewBag.id_user = new SelectList(db.users, "id_user", "names", order.id_user);
            return RedirectToAction("Index");
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
            return RedirectToAction("Index");
        }

        // POST: orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            order order = db.orders.Find(id);
            var order_item = db.order_item.Where(x => x.id_order == order.id_order).ToList();
            foreach (var item in order_item)
            {
                db.order_item.Remove(item);
                db.SaveChanges();
            }
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
