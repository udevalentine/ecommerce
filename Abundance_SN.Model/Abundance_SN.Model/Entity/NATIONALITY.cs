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
    
    public partial class NATIONALITY
    {
        public NATIONALITY()
        {
            this.STATE = new HashSet<STATE>();
        }
    
        public int Nationality_Id { get; set; }
        public string Nationality_Name { get; set; }
    
        public virtual ICollection<STATE> STATE { get; set; }
    }
}
