using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Abundance_SN.Model.Entity;
using Abundance_SN.Model.Model;
using Abundance_SN.Model.Translator;

namespace Abundance_SN.Business
{
    public class PaymentLogic : BusinessBaseLogic<Payment,PAYMENT>
    {
        public PaymentLogic()
        {
            translator = new PaymentTranslator();
        }

        public List<PaymentView> GetPaidOrder()
        {
                 List<PaymentView> payments = new List<PaymentView>();
            try
            {
                
                payments = (from sr in repository.GetAll<VW_SALES_PAYMENT>()
                            select new PaymentView
                            {
                                InvoiceNumber = sr.Invoice_Number,
                                PaymentModeId = sr.Payment_Mode_Id,
                                PersonId = sr.Person_Id,
                                Amount = sr.amount,
                                Tansactiondate = Convert.ToDateTime(sr.transaction_date).ToShortDateString(),
                                CustomerName =  sr.Name,
                                PaymentModeName =  sr.Payment_Mode_Name


                            }).OrderByDescending(x=>x.Tansactiondate).ToList();
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return payments;
        }

        public void Modify(Payment payment)
        {
            try
            {
                Expression<Func<PAYMENT, bool>> selector = a => a.Payment_Id == payment.Id;
                PAYMENT entity = GetEntityBy(selector);

                entity.Paid = true;
            }
            catch (Exception)
            {
                
                throw;
            }
           
        }
    }
}
