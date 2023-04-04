using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web;
using System.Web.Management;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.Models.Functions
{
    public class Func_Product
    {
        private MobileShoppingEntities db;
        public Func_Product()
        {
            db = new MobileShoppingEntities();
        }
        public IQueryable<product> products
        {
            get { return db.products; }
        }
        public int Detail(int id)
        {
            product product = db.products.Find(id);
            return product.id_product;
        }
        public int Insert(product product)
        {
            db.products.Add(product);
            db.SaveChanges();
            return product.id_product;
        }
        public int Update(product product)
        {
            product products = db.products.Find(product.id_product);
            if (products == null)
            {
                return -1;
            }
            products.id_product = product.id_product;
            products.names = product.names;
            products.price = product.price;
            products.ram = product.ram;
            products.quantity = product.quantity;
            products.images = product.images;
            products.color = product.color;
            products.brand = product.brand;
            products.battery = product.battery;
            products.display = product.display;
            products.water_resistance = product.water_resistance;
            products.weights = product.weights;
            products.processor = product.processor;
            products.operating_system = product.operating_system;
            products.rate = product.rate;
            products.promocode.code = product.promocode.code;
            db.SaveChanges();
            return products.id_product;
        }
        public int Delete(int id)
        {
            product products = db.products.Find(id);
            if (products == null)
            {
                return -1;
            }
            db.products.Remove(products);
            db.SaveChanges();
            return id;
        }
        public List<ProductVM> Products_View()
        {
            List<ProductVM> listproductview;
            var query = from pro in db.products
                        select new ProductVM
                        {
                            id_product = pro.id_product,
                            names = pro.names,
                            price = pro.price,
                            ram = pro.ram,
                            quantity = pro.quantity,
                            images = pro.images,
                            color = pro.color,
                            brand = pro.brand,
                            battery = pro.battery,
                            display = pro.display,
                            water_resistance = pro.water_resistance,
                            weights = pro.weights,
                            processor = pro.processor,
                            id_promo = pro.id_promo,
                            rate = pro.rate,
                            operating_system = pro.operating_system,
                        };
            listproductview = query.ToList();
            return listproductview;
        }
        public List<ProductVM> Search(string loaiTimKiem, string mucTimKiem, int giaTriTimKiem, string tenTiemKiem)
        {
            List<ProductVM> listproductview;
            var query = from pro in db.products
                        select new ProductVM
                        {
                            id_product = pro.id_product,
                            names = pro.names,
                            price = pro.price,
                            ram = pro.ram,
                            quantity = pro.quantity,
                            images = pro.images,
                            color = pro.color,
                            brand = pro.brand,
                            battery = pro.battery,
                            display = pro.display,
                            water_resistance = pro.water_resistance,
                            weights = pro.weights,
                            processor = pro.processor,
                            id_promo = pro.promocode.id_promo,
                            rate = pro.rate,
                            operating_system = pro.operating_system,
                        };
            if (loaiTimKiem == "brand")
            {
                if (mucTimKiem == "==") listproductview = query.Where(m => m.brand == tenTiemKiem).ToList();
                else listproductview = query.Where(m => m.brand == tenTiemKiem).ToList();
            }
            else
            {
                if (mucTimKiem == "==") listproductview = query.Where(m => m.names == tenTiemKiem).ToList();
                else listproductview = query.Where(m => m.names == tenTiemKiem).ToList();
            }
            return listproductview;
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