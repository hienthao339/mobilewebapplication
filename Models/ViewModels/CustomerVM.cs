using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models.ViewModels
{
    public class CustomerVM
    {
        public int id { get; set; }
        public string email { get; set; }
        public string names { get; set; }
        public string phone { get; set; }
        public string addresss { get; set; }
        public string customer_type { get; set; }
    }
}