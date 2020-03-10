using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using Abundance_SN.Model.Model;
using RestSharp;
using RestSharp.Authenticators;
using System.Net.Mime;

namespace Abundance_SN.Business
{
    public class EmailServiceManger : IEmailServiceManger
    {
        public IRestResponse SendEmailMessage(string emailAddress, string orderNumber, List<SalesLogs> salesLogses)
        {
            try
            {
                PersonLogic personlogic = new PersonLogic();
                decimal amountPaid = salesLogses.Sum(s => s.Amount);
                long userId = salesLogses.LastOrDefault().User.Id;
                Person person = personlogic.GetModelBy(u => u.User_Id == userId);
                string fullName = person.Surname + person.FirstName;
                List<string> distinctsalesTable = new List<string>();
                string salesDetailTable = null;

                string htmlheader = "http://www.w3.org/1999/xhtml";

                for (int i = 0; i < salesLogses.Count; i++)
                {
                    salesDetailTable += "<tr style=" +
                     "font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;" +
                     "><td style=" +
                     "font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; border-top-width: 1px; border-top-color: #eee; border-top-style: solid; margin: 0; padding: 5px 0;" +
                     "valign=" + "top" + "> " + salesLogses[i].Product.Name + " x" + salesLogses[i].Quantity + "</td>" + "<td class=" + "alignright" + "style=" +
                     "font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box;" +
                     "font-size: 14px; vertical-align: top; text-align: right; border-top-width: 1px; border-top-color: #eee; border-top-style: solid; margin: 0; padding: 5px 0;" +
                     "align=" + "right" + "valign=" + "top" + ">" + salesLogses[i].Product.Price + "</td></tr>";
                    distinctsalesTable.Add(salesDetailTable);
                }
                RestClient client = new RestClient();
                client.BaseUrl = new Uri("https://api.mailgun.net/v3");
                client.Authenticator =
                    new HttpBasicAuthenticator("api",
                                                "key-89518398f4a3879ad76d270718025c05");
                RestRequest request = new RestRequest();
                request.AddParameter("domain", "sandbox96515b4a9e6844cb8bf25eadec702747.mailgun.org", ParameterType.UrlSegment);
                request.Resource = "{domain}/messages";
                request.AddParameter("from", "emmatex2000@gmail.com");
                request.AddParameter("to", emailAddress);
                request.AddParameter("subject", "Purchase Reciept ");
                request.AddParameter("text", "Dear customer your order has been Created Successfully.Your order number is" + orderNumber);
                request.AddParameter("html",
                    "<html" + "xmlns=" + " \"http://www.w3.org/1999/xhtml\" " + "style= font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif box-sizing: border-box; font-size: 14px; margin: 0;" + ">" +
                    "<head><meta name=" + "viewport" + "content=" + "width=device-width" + "/> <meta http-equiv=" +
                    "Content-Type" + "content=" + "text/html; charset=UTF-8" + "/><title>Lloydant Ecommerce</title><style type=" +
                    "text/css" + ">" +
                    "img {max-width: 100%;}body {-webkit-font-smoothing: antialiased; -webkit-text-size-adjust: none; width: 100% !important; height: 100%; line-height: 1.6em;}body {background-color: #f6f6f6;}@media only screen and (max-width: 640px) {body { padding: 0 !important;}" +
                    "h1 { font-weight: 800 !important; margin: 20px 0 5px !important;h2 {font-weight: 800 !important; margin: 20px 0 5px !important;}} h3{font-weight: 800 !important; margin: 20px 0 5px !important;} h4 {font-weight: 800 !important; margin: 20px 0 5px !important;}" +
                    " h1 { font-size: 22px !important;} h2 {font-size: 18px !important;} h3 {font-size: 16px !important;}.container {padding: 0 !important; width: 100% !important;} .content {padding: 0 !important;}.content-wrap {padding: 10px !important;}.invoice {width: 100% !important;}" +
                    "}</style></head><body itemscope itemtype=" + "http://schema.org/EmailMessage" + "style=" +
                    "font-family: 'Helvetica Neue',Helvetica,Arial," +
                    "sans-serif; box-sizing: border-box; font-size: 14px; -webkit-font-smoothing: antialiased; -webkit-text-size-adjust:" +
                    "none; width: 100% !important; height: 100%; line-height: 1.6em; background-color: #f6f6f6; margin: 0;" +
                    "bgcolor=" + "#f6f6f6" + "><table class=" + "body-wrap" + "style=" +
                    "font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box;" +
                    "font-size: 14px; width: 100%; background-color: #f6f6f6; margin: 0;" + "bgcolor=" + "#f6f6f6" +
                    "><tr style=" +
                    "font-family: Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;" +
                    "><td style=" + "font-family:" +
                    "'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0;" +
                    "valign=" + "top" + "></td><td class=" + "container" + "width=" + "600" + "style=" +
                    "font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; " +
                    "box-sizing: border-box; font-size: 14px; vertical-align: top; display: block !important; max-width: 600px !important; clear: both !important; margin: 0 auto;" +
                    "valign=" + "top" +
                    "><div class=" + "content" + "style=" +
                    "font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; max-width: 600px; display: block; margin: 0 auto; padding: 20px;" +
                    "><table class=" + "main" + "width=" + "100%" + "cellpadding=" + "0" + "cellspacing=" + "0" +
                    "style=" +
                    "font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; border-radius: 3px; background-color: #fff; margin: 0; border: 1px solid #e9e9e9;" +
                    "bgcolor=" + "#fff" + "><tr style=" + "font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; " +
                    "box-sizing: border-box; font-size: 14px; margin: 0;" + "><td class=" + "alert alert-warning" + "style=" +
                    "font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 26px; vertical-align: top; color: #fff; font-weight: 500; text-align: center; border-radius: " +
                    "3px 3px 0 0; background-color: #0da9ef; margin: 0; padding: 20px;  height:100px; padding: 55px 0;" +
                    "align=" + "center" + "bgcolor=" + "#FF9F00" + "valign=" + "top" + ">LLOYDANT ECOMMERCE</td></tr><tr style=" +
                    "font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; " +
                    "font-size: 14px; margin: 0;" + "><td class=" + "alert alert-warning" + "style=" +
                    "font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 40px; vertical-align: top; color: #fff; font-weight: 500; text-align: center; border-radius: 3px 3px 0 0;" +
                    " background-color: rgb(11, 216, 128); height:150px; margin: 0; padding: 55px 0;" + "" + "align=" + "center" +
                    "bgcolor=" + "#FF9F00" + "valign=" + "top" + "<b>Paid: ₦" + amountPaid + "</b></td></tr> <tr style=" +
                    "font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; " +
                    "font-size: 14px; margin: 0;" + "><td class=" + "content-wrap" + "style=" +
                    "font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0; padding: 20px;" +
                    "valign=" + "top" + "><table class=" + "main" + "width=" + "100%" + "cellpadding=" + "0"
                    + "cellspacing=" + "0" + "style=" +
                    "font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; border-radius: 3px; background-color: #fff; margin: 0; " +
                    " bgcolor=" + "#fff" + "><tr style=" +
                    "font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;" +
                    ">" + "<td class="
                    + "content-wrap aligncenter" + "style=" + "font-family:" +
                    ",Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; text-align: center; margin: 0; padding: 20px;" +
                    "align=" + "center" + "valign=" + "top" + "><table width=" + "100%" + "cellpadding=" + "0" + "cellspacing=" +
                    "0" + "style=" +
                    "font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;" +
                    ">" + "<tr style=" +
                    "font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;" +
                    "><td class=" + "content-block aligncenter" + "style=" +
                    "font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; text-align: center; margin: 0; padding: 0 0 20px;" +
                    "align=" + "center" + "valign=" + "top" + "><table class=" + "invoice" + "style=" +
                    "font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; " +
                    "font-size: 14px; text-align: left; width: 80%; margin: 40px auto;" + "><tr style=" +
                    "font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;" +
                    "><td style=" +
                    "font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0; padding: 5px 0;" +
                    "valign=" + "top" + ">" + fullName + "<br style=" +
                    "font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;" +
                    "/>Order#" + orderNumber + "<br style=" +
                    "font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;" +
                    "/>" + DateTime.UtcNow.ToShortDateString() + "</td>" +
                    "</tr><tr style=" +
                    "font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;" +
                    "><td style=" +
                    "font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0; padding: 5px 0;" +
                    "valign=" + "top" + ">" +
                    "<table class=" + "invoice-items" + " cellpadding=" + "0" + "cellspacing=" + "0" + "style=" +
                    "font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; width: 100%; margin: 0;" +
                    "><tr style=" +
                    "font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;" +
                    ">" + "<tr style=" +
                     "font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;" +
                     "><td style=" +
                     "font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; border-top-width: 1px; border-top-color: #eee; border-top-style: solid; margin: 0; padding: 5px 0;" +
                     "valign=" + "top" + "> " + salesLogses[0].Product.Name + " x" + salesLogses[0].Quantity + "</td>" + "<td class=" + "alignright" + "style=" +
                     "font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box;" +
                     "font-size: 14px; vertical-align: top; text-align: right; border-top-width: 1px; border-top-color: #eee; border-top-style: solid; margin: 0; padding: 5px 0;" +
                     "align=" + "right" + "valign=" + "top" + ">" + salesLogses[0].Product.Price + "</td></tr>" + "<tr class=" + "total" + "style=" +
                    "font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;" +
                    "><td class=" + "alignright" + "width=" + "80%" + "style=" +
                    "font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; " +
                    "text-align: right; border-top-width: 2px; border-top-color: #333; border-top-style: solid; border-bottom-color: #333; border-bottom-width: 2px; border-bottom-style: solid; font-weight: 700; margin: 0; padding: 5px 0;" +
                    "align=" + "right" + "valign=" + "top" + ">Total</td> <td class=" + "alignright" + "style=" +
                    "font-family:'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box;" +
                    " font-size: 14px; vertical-align: top; text-align: right; border-top-width: 2px; border-top-color: #333; border-top-style: solid; border-bottom-color: #333; border-bottom-width: 2px; border-bottom-style: solid; font-weight: 700; margin: 0; padding: 5px 0;" +
                    "align=" + "right" + "valign=" + "top" + ">" + amountPaid + "</td></tr></table></td></tr></table></td></tr>" +
                    "<tr style=" +
                    "font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;" +
                    "><td class=" + "content-block aligncenter" + "style=" +
                    "font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; text-align: center; margin: 0; padding: 0 0 20px;" +
                    "align=" + "center" + "valign=" + "top" + ">" +
                    "<a href=" + "http://97.74.6.243/easyShop/customer/checkout/viewOrderByNumber/" + orderNumber + "style=" +
                    "font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; color: #348eda; text-decoration: underline; margin: 0;" +
                    ">View in browser</a></td></tr></table></td> </tr></table><table width=" + "100%" +
                    "cellpadding=" + "0" + "cellspacing=" + "0" + "style=" +
                    "font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;" +
                    "><tr style=" +
                    "font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;" +
                    "><td class=" + "content-block" + "style=" +
                    "font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; " +
                    "font-size: 14px; vertical-align: top; margin: 0; padding: 0 0 20px;" + "valign" + "top" + "><h2 class=" +
                    "aligncenter" + "style=" +
                    "font-family: 'Helvetica Neue',Helvetica,Arial,'Lucida Grande',sans-serif; box-sizing: border-box; font-size: 24px; color: #000; line-height: 1.2em; font-weight: 400; text-align: center; margin: 40px 0 0;" +
                    "align=" + "center" + ">Thanks for using LLoydant Ecommerce</h2>" +
                    "</td></tr></table></td></tr></table><div class=" + "footer" + "style=" +
                    "font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; width: 100%; clear: both; color: #999; margin: 0; padding: 20px;" +
                    "><table width=" + "100%" + " style=" +
                    "font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;" +
                    "><tr style=" +
                    "font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;" +
                    "><td class=" + "aligncenter content-block" + "style=" +
                    "font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 12px; vertical-align: top; color: #999; text-align: center; margin: 0; padding: 0 0 20px;" +
                    "align=" + "center" + "valign=" + "top" + "><a href=" + "http://www.mailgun.com" + "style=" +
                    "font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 12px; color: #999; text-decoration: underline; margin: 0;" +
                    ">Unsubscribe</a> from these alerts.</td></tr></table></div></div></td><td style=" +
                    "font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0;" +
                    "valign=" + "top" + "></td></tr></table></body>" + "</html>");

                request.Method = Method.POST;
                return client.Execute(request);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public IRestResponse SendEmail(string emailAddress, string orderNumber, List<SalesLogs> salesLogses)
        {
              RestClient client = new RestClient();
              RestRequest request = new RestRequest();
            try
            {
                string purchase = null;
                for (int i = 0; i < salesLogses.Count; i++)
                {
                   purchase  += "<tr><td style=\"font-family: 'arial'; font-size: 14px; vertical-align: middle; margin: 0; padding: 9px 0;\">" + salesLogses[i].Product.Name + "" +
                            "<td style=\"font-family: 'arial'; font-size: 14px; vertical-align: middle; margin: 0; padding: 9px 0;\"  align=\"right\">"+ salesLogses[i].Product.DiscountAmount +"</td></tr>";
                }
                PersonLogic personlogic = new PersonLogic();
                string amountPaid = salesLogses.Sum(s => s.Amount).ToString(CultureInfo.InvariantCulture);
                long userId = salesLogses.LastOrDefault().User.Id;
                Person person = personlogic.GetModelBy(u => u.User_Id == userId);
                string fullName = person.Surname + " " + person.FirstName;
                string date = DateTime.Now.ToLongDateString();

                client.BaseUrl = new Uri("https://api.mailgun.net/v3");
                client.Authenticator = new HttpBasicAuthenticator("api", "key-89518398f4a3879ad76d270718025c05");
                request.AddParameter("domain", "sandbox96515b4a9e6844cb8bf25eadec702747.mailgun.org", ParameterType.UrlSegment);
                request.Resource = "{domain}/messages";
                request.AddParameter("from", "emmatex2000@gmail.com");
                request.AddParameter("to", emailAddress);
                request.AddParameter("subject", "Payment Confimation");
                request.AddParameter("text", "Dear customer your order has been Created Successfully");
                request.AddParameter("html", "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTDxhtml1-transitional.dtd\"><html xmlns=\"http://www.w3.org/1999/xhtml\">" +
                                             "<head><meta name=\"viewport\" content=\"width=device-width\"/><meta http-equiv=\"Content-Type\"content=\"text/html; charset=UTF-8\"/><title>Email Template</title></head>" +
                                             "<body style=\"margin:0px; background: #f8f8f8;\"><div width = \"100%\"style=\"background: #f8f8f8; padding: 0px 0px; font-family:arial; line-height:28px; height:100%; width: 100%; color: #514d6a;\">" +
                                             "<div style=\"max-width: 700px; padding:50px 0;  margin: 0px auto; font-size: 14px\"><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" style=\"width:100%; margin-bottom: 20px\"><tbody><tr style=\"font-family:" +
                                             "'Helvetica Neue',Helvetica,Arial,sans-serif;box-sizing: border-box; font-size: 14px; margin: 0;\"><td class=\" alert alert-warning style=\"font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 26px; " +
                                             "vertical-align: top; color: #fff; font-weight: 500; text-align: center; border-radius:3px 3px 0 0; background-color: #0da9ef; margin: 0; padding: 20px;  height:100px; padding: 55px 0; align:center;bgcolor=\"#FF9F00\" valign:top\">Lloydant E-commerce</td></tr></tbody></table>" +
                                             "<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" style=\"width:100%;\"><tbody><tr><td style=\"background:#f44336; padding:20px; color:#fff; text-align:center;\"> Thank you for Shopping with us</td></tr> </tbody></table><div style=\"padding: 40px; background: #fff;\">" +
                                             "<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" style=\"width: 100%;\"><tbody><tr><td><b>" + fullName + "</b> <p style=\"margin-top:0px;\">Invoice #" + orderNumber + "</p></td><td align=\"right\" width=\"100\"> " + date + "</td></tr><tr> <td colspan=\"2\" style=\"padding:20px 0;" +
                                             " border-top:1px solid #f6f6f6;\"><div><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\"><tbody>"+ purchase +" <tr class=\"total\"><td style=\"font-family: 'arial'; font-size: 14px; vertical-align: middle; border-top-width: 1px; border-top-color: #f6f6f6; " +
                                             "border-top-style: solid; margin: 0; padding: 9px 0; font-weight:bold;\" width=\"80%\">Total</td> <td style=\"font-family: 'arial'; font-size: 14px; vertical-align: middle; border-top-width: 1px; border-top-color: #f6f6f6;" +
                                             "border-top-style: solid; margin: 0; padding: 9px 0; font-weight:bold;\" align=\"right\">₦" + amountPaid + "</td></tr></tbody></table></div></td></tr><tr> <td colspan=\"2\"><center> <a href=\"http://97.74.6.243/easyShop/customer/checkout/viewOrderByNumber/\"" + orderNumber +
                                             "style=\"display: inline-block; padding: 11px 30px; margin: 20px 0px 30px; font-size: 15px; color: #fff; background: #1e88e5; border-radius: 60px; text-decoration:none;\">View Order Details</a></center><b>-Thanks(Suppport)</b></td></tr></tbody></table></div><div style=\"text-align: center; font-size: 12px; color: #b2b2b5; " +
                                             "margin-top: 20px\"><p> Powered by LLoydant Business Services <br></p></div></div></div></body></html>");
                
                       
           
               
             
              
          
                     
                   
       
                request.Method = Method.POST;
               
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return client.Execute(request);
        }
    }
}
