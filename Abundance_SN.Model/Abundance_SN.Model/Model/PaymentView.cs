using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abundance_SN.Model.Model
{
    public class PaymentView
    {
        public string CustomerName { get; set; }
        public long PaymentId { get; set; }
        public string InvoiceNumber { get; set; }
        public long PersonId { get; set; }
        public int PaymentModeId { get; set; }
        public decimal? Amount { get; set; }
        public string Tansactiondate { get; set; }
       public string PaymentModeName { get; set; }
    }
}
