using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
        public int Update(order order)
        {
            order ord = db.orders.Find(order.id_order);
            if (order == null)
            {
                return -1;
            }
            ord.id_order = order.id_order;
            ord.total_price = order.total_price;
         
         
            ord.started_at = order.started_at;
            ord.created_at = order.created_at;
            ord.canceled = order.canceled;
         
            ord.payment_type = order.payment_type;
          
            ord.shipping_fee = order.shipping_fee;
            ord.id_customer = order.id_customer;
            ord.id_user = order.id_user;
            ord.pending = order.pending;
            ord.id_promo = order.id_promo;
            return order.id_order;
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