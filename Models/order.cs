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
    
    public partial class order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public order()
        {
            this.order_item = new HashSet<order_item>();
        }
    
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
        public Nullable<int> id_customer { get; set; }
    
        public virtual customer customer { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<order_item> order_item { get; set; }
        public virtual promocode promocode { get; set; }
        public virtual user user { get; set; }
    }
}
