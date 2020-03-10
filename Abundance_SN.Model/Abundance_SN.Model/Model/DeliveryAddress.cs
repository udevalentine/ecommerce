using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abundance_SN.Model.Model
{
    public class DeliveryAddress
    {
        public long Id { get; set; }
        public string CartHash { get; set; }
        public string StateId { get; set; }
        public string Address { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateDelivered { get; set; }
        public Person Person { get; set; }
        public DeliveryService DeliveryService { get; set; }
        public DeliveryStatus DeliveryStatus { get; set; }
        public int DeliveryOption { get; set; }
        public State State { get; set; }

    }
}
