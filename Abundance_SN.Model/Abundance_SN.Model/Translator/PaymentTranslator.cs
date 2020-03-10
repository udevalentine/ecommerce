using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abundance_SN.Model.Entity;
using Abundance_SN.Model.Model;

namespace Abundance_SN.Model.Translator
{
    public class PaymentTranslator : TranslatorBase<Payment,PAYMENT>
    {
        private PersonTranslator personTranslator;
        private PaymentModeTranslator paymentModeTranslator;

        public PaymentTranslator()
        {
            personTranslator = new PersonTranslator();
            paymentModeTranslator = new PaymentModeTranslator();
        }
        public override Payment TranslateToModel(PAYMENT entity)
        {
            try
            {
                Payment model = null;
                if (entity != null)
                {
                    model = new Payment();
                    model.Id = entity.Payment_Id;
                    model.InvoiceNumber = entity.Invoice_Number;
                    model.DatePaid = entity.Date_Paid;
                    model.PaymentSerialNumber = entity.Payment_Serial_Number;
                    model.Person = personTranslator.Translate(entity.PERSON);
                    model.PaymentMode = paymentModeTranslator.Translate(entity.PAYMENT_MODE);
                    model.Paid = entity.Paid;
                }
                return model;
            }
            catch (Exception ex)
            {
               
                throw ex; 
            }
        }

        public override PAYMENT TranslateToEntity(Payment model)
        {
            try
            {
                PAYMENT entity = null;
                if (model != null)
                {
                    entity = new PAYMENT();
                    entity.Payment_Id = model.Id;
                    entity.Payment_Mode_Id = model.PaymentMode.Id;
                    entity.Person_Id = model.Person.Id;
                    entity.Invoice_Number = model.InvoiceNumber;
                    entity.Date_Paid = model.DatePaid;
                    entity.Payment_Serial_Number = model.PaymentSerialNumber;
                    entity.Paid = model.Paid;
                }
                return entity;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
    }
}

