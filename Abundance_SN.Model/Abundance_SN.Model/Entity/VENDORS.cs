//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Abundance_SN.Model.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class VENDORS
    {
        public VENDORS()
        {
            this.PRODUCT_ORDER_LOGS = new HashSet<PRODUCT_ORDER_LOGS>();
            this.PRODUCT_SUPPLY_LOGS = new HashSet<PRODUCT_SUPPLY_LOGS>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone_Number { get; set; }
        public bool Active { get; set; }
        public Nullable<System.DateTime> Createed_At { get; set; }
        public Nullable<System.DateTime> Updated_At { get; set; }
    
        public virtual ICollection<PRODUCT_ORDER_LOGS> PRODUCT_ORDER_LOGS { get; set; }
        public virtual ICollection<PRODUCT_SUPPLY_LOGS> PRODUCT_SUPPLY_LOGS { get; set; }
    }
}
