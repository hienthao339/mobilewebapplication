//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class feedback
    {
        public int id_feedback { get; set; }
        public string content { get; set; }
        public Nullable<int> rate { get; set; }
        public Nullable<System.DateTime> created_at { get; set; }
        public Nullable<int> id_product { get; set; }
        public Nullable<int> id_user { get; set; }
    
        public virtual product product { get; set; }
        public virtual user user { get; set; }
    }
}
