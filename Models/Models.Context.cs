﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MobileShoppingEntities : DbContext
    {
        public MobileShoppingEntities()
            : base("name=MobileShoppingEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<cart> carts { get; set; }
        public virtual DbSet<chat> chats { get; set; }
        public virtual DbSet<customer> customers { get; set; }
        public virtual DbSet<feedback> feedbacks { get; set; }
        public virtual DbSet<order_item> order_item { get; set; }
        public virtual DbSet<order> orders { get; set; }
        public virtual DbSet<product> products { get; set; }
        public virtual DbSet<promocode> promocodes { get; set; }
        public virtual DbSet<rank> ranks { get; set; }
        public virtual DbSet<user> users { get; set; }
    }
}
