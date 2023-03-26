using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models.Functions
{
    public class Func_User
    {
        private MobileShoppingEntities db;
        public Func_User()
        {
            db = new MobileShoppingEntities();
        }
        public IQueryable<user> users
        {
            get { return db.users; }
        }
        public int Insert(user model)
        {
            db.users.Add(model);
            db.SaveChanges();
            return model.id_user;
        }
        public int Update(user model)
        {
            user users = db.users.Find(model.id_user);
            if (users == null)
            {
                return -1;
            }
            users.id_user = model.id_user;
            users.email = model.email;
            users.passwords = model.passwords;
            users.is_admin = model.is_admin;
            users.avatar = model.avatar;
            db.SaveChanges();
            return model.id_user;
        }
        public int Delete(int id)
        {
            user model = db.users.Find(id);
            if (model == null)
            {
                return -1;
            }
            db.users.Remove(model);
            db.SaveChanges();
            return model.id_user;
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