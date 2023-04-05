using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models.ViewModels
{
    public class CheckOutVM
    {
        public promocode promocode { get; set; }
        public int id_order { get; set; }
        public Nullable<int> id_user { get; set; }
        public Nullable<bool> payment_type { get; set; }
        public Nullable<System.DateTime> created_at { get; set; }
        public Nullable<System.DateTime> started_at { get; set; }
        public Nullable<System.DateTime> finished_at { get; set; }
        public Nullable<int> shipping_fee { get; set; }
        public Nullable<decimal> total_price { get; set; }
        public Nullable<int> id_promo { get; set; }
        public Nullable<bool> pending { get; set; }
        public Nullable<bool> delivering { get; set; }
        public Nullable<bool> successed { get; set; }
        public Nullable<bool> canceled { get; set; }
        public Nullable<bool> paid { get; set; }
        public string addresss { get; set; }
        public string city { get; set; }
        public string district { get; set; }
        public string ward { get; set; }
    }
}