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
    
    public partial class CUSTOMER_VISIT
    {
        public long Customer_Visit_Id { get; set; }
        public long Product_Id { get; set; }
        public string Uuid { get; set; }
        public Nullable<System.DateTime> Date_Of_Visit { get; set; }
    
        public virtual PRODUCT PRODUCT { get; set; }
    }
}
