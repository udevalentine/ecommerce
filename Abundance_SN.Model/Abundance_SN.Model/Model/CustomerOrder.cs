using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abundance_SN.Model.Model
{
    public class CustomerOrder
    {
        public long Id { get; set; }
        public string CustomerOrderNumber { get; set; }
        public string CartHash { get; set; }
        public decimal Amount { get; set; }
        public string CustomerDeliveryAddress { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public Customer Customer { get; set; }
        public DeliveryService DeliveryService { get; set; }
    }
}
