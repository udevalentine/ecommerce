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
    
    public partial class COURIER_CHARGES
    {
        public COURIER_CHARGES()
        {
            this.COURIER_DETAIL = new HashSet<COURIER_DETAIL>();
        }
    
        public int Id { get; set; }
        public decimal Charges { get; set; }
    
        public virtual ICollection<COURIER_DETAIL> COURIER_DETAIL { get; set; }
    }
}