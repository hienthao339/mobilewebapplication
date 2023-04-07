using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace WebApplication1.Models.ViewModels
{
    public class UserVM
    {
        public int id_user { get; set; }
        public Nullable<bool> is_admin { get; set; }
        public string names { get; set; }
        public string email { get; set; }
        public string passwords { get; set; }
        [DataType(DataType.Upload)]
        [Display(Name = "Upload File")]
        [Required(ErrorMessage = "Please choose file to upload.")]
        public string avatar { get; set; }
        public string phone { get; set; }
        public Nullable<int> id_rank { get; set; }
    }
}