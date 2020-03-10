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
    
    public partial class USER
    {
        public USER()
        {
            this.PERSON = new HashSet<PERSON>();
            this.PRODUCT_ORDER_LOG = new HashSet<PRODUCT_ORDER_LOG>();
            this.PRODUCT_SUPPLY_LOG = new HashSet<PRODUCT_SUPPLY_LOG>();
            this.SALES_LOG = new HashSet<SALES_LOG>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Role_Id { get; set; }
    
        public virtual ICollection<PERSON> PERSON { get; set; }
        public virtual ICollection<PRODUCT_ORDER_LOG> PRODUCT_ORDER_LOG { get; set; }
        public virtual ICollection<PRODUCT_SUPPLY_LOG> PRODUCT_SUPPLY_LOG { get; set; }
        public virtual ROLE ROLE { get; set; }
        public virtual ICollection<SALES_LOG> SALES_LOG { get; set; }
    }
}