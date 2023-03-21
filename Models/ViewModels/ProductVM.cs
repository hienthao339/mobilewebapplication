using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication1.Models.ViewModels
{
    public class ProductVM
    {
        public int id_product { get; set; }
        public string names { get; set; }
        public string images { get; set; }
        public Nullable<decimal> price { get; set; }
        public string display { get; set; }
        public string weights { get; set; }
        public string water_resistance { get; set; }
        public string operating_system { get; set; }
        public string processor { get; set; }
        public string battery { get; set; }
        public string ram { get; set; }
        public string rom { get; set; }
        public string brand { get; set; }
        public string color { get; set; }
        public Nullable<int> quantity { get; set; }
        public Nullable<decimal> rate { get; set; }
        public Nullable<decimal> discount_price { get; set; }
    }
}