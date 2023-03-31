using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models.ViewModels
{
    public class UserVM
    {
        public int id_user { get; set; }
        public string email { get; set; }
        public string passwords { get; set; }
        public string avatar { get; set; }
        public string phone { get; set; }
        public string addresss { get; set; }
        public string city { get; set; }
        public string district { get; set; }
        public string ward { get; set; }
        public Nullable<int> id_rank { get; set; }
        public string user_type { get; set; }
    }
}