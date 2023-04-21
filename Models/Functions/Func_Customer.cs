using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models.Functions
{
    public class Func_Customer
    {
        private MobileShoppingEntities db;
        public Func_Customer()
        {
            db = new MobileShoppingEntities();
        }
        public IQueryable<customer> customers
        {
            get { return db.customers; }
        }
        public int Insert(customer model)
        {
            db.customers.Add(model);
            db.SaveChanges();
            return model.id_customer;
        }
        public int Update(customer model)
        {
            customer customer = db.customers.Find(model.id_customer);
            if (customers == null)
            {
                return -1;
            }
            customer.id_customer = model.id_customer;
            customer.email = model.email;
            customer.phone = model.phone;
            customer.addresss = model.addresss;
            customer.ward = model.ward;
            customer.district = model.district;
            customer.city = model.city;
            db.SaveChanges();
            return model.id_customer;
        }
        public int Delete(int id)
        {
            customer model = db.customers.Find(id);
            if (model == null)
            {
                return -1;
            }
            db.customers.Remove(model);
            db.SaveChanges();
            return model.id_customer;
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