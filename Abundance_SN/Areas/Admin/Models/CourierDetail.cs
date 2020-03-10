using Abundance_SN.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Abundance_SN.Areas.Admin.Models
{
    public class CourierDetail
    {
        public CourierService CourierService { get; set; }
        public CourierAreaCharges CourierAreaCharges { get; set; }
        public State State { get; set; }
        public CourierCharges CourierCharges { get; set; }
    }
}