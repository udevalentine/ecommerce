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
    
    public partial class COURIER_SERVICE
    {
        public COURIER_SERVICE()
        {
            this.COURIER_DETAIL = new HashSet<COURIER_DETAIL>();
        }
    
        public int Courier_Id { get; set; }
        public string Courier_Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone_Number { get; set; }
    
        public virtual ICollection<COURIER_DETAIL> COURIER_DETAIL { get; set; }
    }
}
