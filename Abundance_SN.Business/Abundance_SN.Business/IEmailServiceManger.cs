using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abundance_SN.Model.Model;
using RestSharp;

namespace Abundance_SN.Business
{
    public interface IEmailServiceManger
    {
        IRestResponse SendEmailMessage(string emailAddress,string orderNumber,List<SalesLogs> salesLogses );
        IRestResponse SendEmail(string emailAddress, string orderNumber, List<SalesLogs> salesLogses);

    }
}
