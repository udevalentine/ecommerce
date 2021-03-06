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
    
    public partial class PRODUCT_SUPPLY_LOG
    {
        public int Id { get; set; }
        public long Product_Id { get; set; }
        public int Vendor_Id { get; set; }
        public int Quantity { get; set; }
        public int User_Id { get; set; }
        public System.DateTime Supply_Date { get; set; }
        public decimal Unit_Price { get; set; }
        public Nullable<int> Product_Order_Log_Id { get; set; }
    
        public virtual PRODUCT PRODUCT { get; set; }
        public virtual PRODUCT_ORDER_LOG PRODUCT_ORDER_LOG { get; set; }
        public virtual USER USER { get; set; }
        public virtual VENDOR VENDOR { get; set; }
    }
}
