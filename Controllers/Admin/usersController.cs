using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using WebApplication1.Extensions;
using WebApplication1.Models;

namespace WebApplication1.Controllers.Admin
{
    public class usersController : Controller
    {
        private MobileShoppingEntities db = new MobileShoppingEntities();

        // GET: users
        public ActionResult Index()
        {
            var user_ranks = db.users.ToList();
            var ranks = db.ranks.ToList();
            foreach (var item1 in user_ranks)
            {
                foreach (var item2 in ranks.OrderBy(x => x.spend))
                {
                    if (item1.totalspend > (double)item2.spend)
                    {
                        item1.id_rank = item2.id_rank;
                    }
                }
            }
            db.SaveChanges();
            var users = db.users.Include(u => u.rank);
            return View(users.ToList());
        }

        // GET: users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: users/Create
        public ActionResult Create()
        {
            ViewBag.id_rank = new SelectList(db.ranks, "id_rank", "id_rank");
            return View();
        }

        // POST: users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_user,is_admin,names,email,passwords,avatar,phone,id_rank")] user user)
        {
            if (ModelState.IsValid)
            {
                db.users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_rank = new SelectList(db.ranks, "id_rank", "id_rank", user.id_rank);
            return View(user);
        }

        // GET: users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_rank = new SelectList(db.ranks, "id_rank", "id_rank", user.id_rank);
            return View(user);
        }

        // POST: users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_user,is_admin,names,email,passwords,avatar,phone,id_rank")] user user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_rank = new SelectList(db.ranks, "id_rank", "id_rank", user.id_rank);
            return View(user);
        }

        // GET: users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            user user = db.users.Find(id);

            var ord = db.orders.Where(x => x.id_user == user.id_user).ToList();

            foreach (var item in ord)
            {
                if (item.pending == false)
                {
                    this.AddNotification("This user can not delete !", NotificationType.ERROR);
                    return RedirectToAction("Delete", "users",new {id = id});
                }
            }
            
            foreach(var item in ord)
            {
                var ord_item = db.order_item.Where(x => x.id_order == item.id_order).ToList();
                foreach(var item2 in ord_item)
                {
                    db.order_item.Remove(item2);
                }
                db.orders.Remove(item);
            }

            db.users.Remove(user);
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
