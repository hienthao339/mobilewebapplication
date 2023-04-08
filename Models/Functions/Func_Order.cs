using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models.Functions
{
    public class Func_Order
    {
        private MobileShoppingEntities db;
        public Func_Order()
        {
            db = new MobileShoppingEntities();
        }
        public IQueryable<order> orders
        {
            get { return db.orders; }
        }
        public int Insert(order model)
        {
            db.orders.Add(model);
            db.SaveChanges();
            return model.id_order;
        }
        public int Update(order model)
        {
            order order = db.orders.Find(model.id_order);
            if (order == null)
            {
                return -1;
            }
            order.id_order = model.id_order;
            order.pending = model.pending;
            order.payment_type = model.payment_type;
            order.total_price = model.total_price;
            order.shipping_fee = model.shipping_fee;
            order.promocode.code = model.promocode.code;
            order.created_at = model.created_at;
            order.finished_at = model.finished_at;
           
            order.canceled = model.canceled;
            order.started_at = model.started_at;
            
            order.paid = model.paid;
            return model.id_order;
        }
        public int Delete(int id)
        {
            order model = db.orders.Find(id);
            if (model == null)
                return -1;
            db.orders.Remove(model);
            db.SaveChanges();
            return id;
        }
    }
}