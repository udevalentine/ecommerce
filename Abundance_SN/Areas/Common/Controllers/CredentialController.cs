using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using Abundance_SN.Business;
using Abundance_SN.Model.Model;
using Abundance_SN.Models;

namespace Abundance_SN.Areas.Common.Controllers
{
    public class CredentialController : Controller
    {
        // GET: Common/Credential
        public ActionResult Invoice(string orderNumber)
        {
            ProductViewModel viewModel = new ProductViewModel();
            try
            {
                PersonLogic personLogic = new PersonLogic();
                SalesLogLogic salesLogLogic = new SalesLogLogic();
                viewModel.SalesLogs = salesLogLogic.GetModelsBy(s => s.Cart_Hash == orderNumber);
                viewModel.Person = personLogic.GetModelBy(p => p.USER.Name == User.Identity.Name);

            }
            catch (Exception)
            {
                
                throw;
            }
            return View(viewModel);
        }

        public ActionResult Recepit(string orderNumber)
        {
            ProductViewModel viewModel = new ProductViewModel();
            try
            {
                if (orderNumber != null)
                {
                    
                      PaymentLogic paymentLogic = new PaymentLogic();
                    PayStackLogic paystackLogic = new PayStackLogic();
                    SalesLogLogic salesLogLogic = new SalesLogLogic();
                     viewModel.Payment = paymentLogic.GetModelBy(s => s.Invoice_Number == orderNumber);
                         viewModel.PayStack = paystackLogic.GetModelBy(p => p.Payment_Id == viewModel.Payment.Id);
                         viewModel.SalesLogs = salesLogLogic.GetModelsBy(s => s.Cart_Hash == orderNumber);
                    

                    

                }
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            return View(viewModel);
        }
        public static bool SendSms(string senderName, string msgBody, string number)
        {
            try
            {
                InfoBipSMS smsClient = new InfoBipSMS();
                string messageBody = msgBody;
                string mobileNumber = "+234" + number.Substring(1);
                InfoSMS smsMessage = new InfoSMS();
                smsMessage.from = senderName;
                smsMessage.to = mobileNumber;
                smsMessage.text = messageBody;
                InfoBipResponse response = smsClient.SendSMS(smsMessage);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}