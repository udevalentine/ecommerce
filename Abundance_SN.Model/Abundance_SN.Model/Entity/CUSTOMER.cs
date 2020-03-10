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
    
    public partial class CUSTOMER
    {
        public CUSTOMER()
        {
            this.CUSTOMER_ORDER = new HashSet<CUSTOMER_ORDER>();
            this.DELIVERY_ADDRESS = new HashSet<DELIVERY_ADDRESS>();
        }
    
        public long Customer_Id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Middle_Name { get; set; }
        public string Phone_Number { get; set; }
        public string Land_Mark { get; set; }
        public string City { get; set; }
        public string State_Id { get; set; }
        public string Email_Address { get; set; }
        public Nullable<long> User_Id { get; set; }
    
        public virtual ICollection<CUSTOMER_ORDER> CUSTOMER_ORDER { get; set; }
        public virtual ICollection<DELIVERY_ADDRESS> DELIVERY_ADDRESS { get; set; }
    }
}