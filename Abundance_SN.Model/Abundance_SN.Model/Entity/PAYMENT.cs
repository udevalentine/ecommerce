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
    
    public partial class PAYMENT
    {
        public long Payment_Id { get; set; }
        public long Person_Id { get; set; }
        public int Payment_Mode_Id { get; set; }
        public Nullable<long> Payment_Serial_Number { get; set; }
        public string Invoice_Number { get; set; }
        public System.DateTime Date_Paid { get; set; }
        public Nullable<bool> Paid { get; set; }
    
        public virtual PAYMENT_MODE PAYMENT_MODE { get; set; }
        public virtual PAYMENT_PAYSTACK PAYMENT_PAYSTACK { get; set; }
        public virtual PERSON PERSON { get; set; }
    }
}
