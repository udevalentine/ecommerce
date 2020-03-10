using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abundance_SN.Model.Model
{
    public class CourierAreaCharges
    {
        public int Id { get; set; }
        public string Area { get; set; }
        //public CourierRegion CourierRegion {get;set;}
        public State State { get; set; }
        public Nullable<decimal> Charger { get; set; }
        public int? CourierId { get; set; }
        public CourierDetail CourierDetails { get; set; }
        public CourierService CourierService { get; set; }
    }
}
