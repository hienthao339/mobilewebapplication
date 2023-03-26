﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    
    [Serializable]
    public class CartItem
    {
        public customer Shopping_customer { get; set; }
        public product Shopping_product { get; set; }
        public int Shopping_quantity { get; set; }
    }

    public class Carts
    {
        List<CartItem> items = new List<CartItem>();
        public IEnumerable<CartItem> Items { get { return items; } }
        public void Add( product pro, int quantity = 1)
        {
            var item = items.FirstOrDefault(x => x.Shopping_product.id_product == pro.id_product );
            if (item == null)
            {
                // neu chua co thi them moi san pham voi so luong = 1
                items.Add(new CartItem
                {
                    Shopping_product = pro,
                    Shopping_quantity = quantity,
                });
            }
            else
            {
                // neu da co san pham thi se tang so luong them = 1
                item.Shopping_quantity += quantity;
            }
        }
        // update lai so luong trong gio hang
        public void Update_Quantity_Shopping(int id, int quantity)
        {
            var item = items.Find(x => x.Shopping_product.id_product == id);
            if (item != null)
            {
                item.Shopping_quantity = quantity;
            }
        }
        public double Total_Price()
        {
            var total = items.Sum(x => x.Shopping_product.price * x.Shopping_quantity);
            return (double)total;
        }
        public void Remove_CartItem(int id)
        {
            items.RemoveAll(s => s.Shopping_product.id_product == id);
        }
        public int Total_Quantity()
        {
            return items.Sum(x => x.Shopping_quantity);
        }
        public void ClearCart()
        {
            items.Clear();
        }
    }
}