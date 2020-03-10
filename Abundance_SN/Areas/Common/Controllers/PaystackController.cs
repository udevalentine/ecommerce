using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Abundance_SN.Business;
using Abundance_SN.Model.Model;

namespace Abundance_SN.Areas.Common.Controllers
{
    public class PaystackController : Controller
    {
        // GET: Common/Paystack
        public ActionResult Index(Paystack paystack)
        {
            string PaystackSecrect = ConfigurationManager.AppSettings["PaystackSecrect"].ToString();
            PayStackLogic paystackLogic = new PayStackLogic();
             SalesLogLogic saleslogLogic = new SalesLogLogic();
            PersonLogic personLogic = new PersonLogic();
            PaymentLogic paymentLogic = new PaymentLogic();
            Payment payment = paymentLogic.GetModelBy(p => p.Invoice_Number == paystack.reference);
            paystackLogic.VerifyPayment(payment, PaystackSecrect);
           
            List<SalesLogs> salesLogs = saleslogLogic.GetModelsBy(s => s.Cart_Hash == paystack.reference);
            if (salesLogs.Count > 0)
            {
                long userId = salesLogs.LastOrDefault().User.Id;
                Person person = personLogic.GetModelBy(p => p.User_Id == userId);
                IEmailServiceManger emailServiceManger = new EmailServiceManger();
              var response =  emailServiceManger.SendEmail(person.Email, paystack.reference,salesLogs);
                //string receiptUrl = "http://localhost:7000/Common/Credential/Recepit?orderNumber=" + paystack.reference;
                //var receiptLink = emailServiceManger.SendEmail(person.Email, receiptUrl, salesLogs);
                //send sms
                StoreSetupLogic storeSetupLogic = new StoreSetupLogic();
                var storeInfo = storeSetupLogic.GetAll().ToList();
                if (storeInfo != null)
                {
                    var shopName = storeInfo.Select(x => x.ShopName).FirstOrDefault().ToUpper();
                    var businessPhone = storeInfo.Select(x => x.BusinessPhone).FirstOrDefault();
                    string receiptUrl = "http://166.62.87.104/Common/Credential/Recepit?orderNumber=" + paystack.reference;
                    string orderAlertMessage = "A new order was made on your store. Click" + " " + receiptUrl + " " + "for details";
                    SendSms(shopName, orderAlertMessage, businessPhone);
                }

            }
            
            return RedirectToAction("Recepit", "Credential", new { Area = "Common", orderNumber = paystack.reference });
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