using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abundance_SN.Model.Model
{
    public class Payment
    {
        public long Id { get; set; }
        public long? PaymentSerialNumber { get; set; }
        public string InvoiceNumber { get; set; }
        public System.DateTime DatePaid { get; set; }
        public PaymentMode PaymentMode { get; set; }
        public Paystack PaymentPaystack { get; set; }
        public Person Person { get; set; }
        public bool? Paid { get; set; }
    }
}
