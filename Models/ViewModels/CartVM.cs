using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models.Functions;

namespace WebApplication1.Models.ViewModels
{
    public partial class cartItem
    { 
        public Nullable<int> id_user { get; set; }
        public Nullable<int> quantity { get; set; }

        public virtual product product { get; set; }
        public virtual user user { get; set; }
    }
    public class CartVM
    {
        List<cartItem> items = new List<cartItem>();
        public IEnumerable<cartItem> Items { get { return items; } }

        MobileShoppingEntities db = new MobileShoppingEntities();

        public void Add_User(product pro,int id_user, int quantity = 1)
        {
            var item = items.FirstOrDefault(x => x.product.id_product == pro.id_product );
            if (item == null)
            {
                // neu chua co thi them moi san pham voi so luong = 1
                items.Add(new cartItem
                {
                    id_user = id_user,
                    product = pro,
                    quantity = quantity,
                });
            }
            else
            {
                // neu da co san pham thi se tang so luong them = 1
                item.quantity += quantity;
            }
        }
        public void Add_guest(product pro, int quantity = 1)
        {
            var item = items.FirstOrDefault(x => x.product.id_product == pro.id_product);
            if (item == null)
            {
                // neu chua co thi them moi san pham voi so luong = 1
                items.Add(new cartItem
                {
                    product = pro,
                    quantity = quantity,
                });
            }
            else
            {
                // neu da co san pham thi se tang so luong them = 1
                item.quantity += quantity;
            }
        }
        // update lai so luong trong gio hang
        public void Update_Quantity_User(int id, int id_user , int quantity)
        {
            var item = items.Find(x => x.product.id_product == id && x.id_user == id_user);
            if (item != null)
            {
                item.quantity = quantity;
            }
        }
        public void Update_Quantity_guest(int id, int quantity)
        {
            var item = items.Find(x => x.product.id_product == id );
            if (item != null)
            {
                item.quantity = quantity;
            }
        }
        public double Total_Price()
        {
            var total = items.Sum(x => x.product.price * x.quantity);

            return (double)total;
        }
        public void Remove_CartItem(int id)
        {
            items.RemoveAll(s => s.product.id_product == id);
        }
        public int? Total_Quantity()
        {
            return items.Sum(x => x.quantity);
        }
        public void ClearCart()
        {
            items.Clear();
        }
    }
}