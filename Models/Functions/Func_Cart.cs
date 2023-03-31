using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models.Functions
{
    public class Func_Cart
    {
        List<cart> items = new List<cart>();
        public IEnumerable<cart> Items { get { return items; } }
        private MobileShoppingEntities db;
        public Func_Cart()
        {
            db = new MobileShoppingEntities();
        }
        public IQueryable<cart> carts
        {
            get { return db.carts; }
        }
        public int Insert(cart model)
        {
            db.carts.Add(model);
            db.SaveChanges();
            return model.id_cart;
        }
        public int Update(cart model)
        {
            cart carts = db.carts.Find(model.id_cart);
            if (carts == null)
            {
                return -1;
            }
            carts.id_product = model.id_product;
            carts.id_user = model.id_user;
            carts.quantity = model.quantity;
            db.SaveChanges();
            return model.id_cart;
        }
        public int Delete(int id)
        {
            cart model = db.carts.Find(id);
            if (model == null)
            {
                return -1;
            }
            db.carts.Remove(model);
            db.SaveChanges();
            return model.id_cart;
        }
        public void ClearCart()
        {
            items.Clear();
        }
        public int? Total_Quantity()
        {
            return items.Sum(x => x.quantity);
        }
        public double Total_Price()
        {
            var total = items.Sum(x => x.product.price * x.quantity);
            return (double)total;
        }
        internal void Update()
        {
            throw new NotImplementedException();
        }

        internal void Insert()
        {
            throw new NotImplementedException();
        }
    }
}