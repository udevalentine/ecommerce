using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Abundance_SN.Model.Model;
using Abundance_SN.Models;

namespace Abundance_SN.Areas.Admin.Models
{
    public class CustomerViewModel
    {
        public CustomerViewModel()
        {
            DeliveryStatuSelectListItem = Utility.PopulateDeliveryStatuSelectListItems();
        }
        public List<SalesLogs> SalesLogses = new List<SalesLogs>();
        public List<string> GroupCountString { get; set; }
        public List<PaymentView> Payments { get; set; }
        public List<Payment> PaymentList { get; set; }
        public DeliveryAddress DeliveryAddress { get; set; }
        public List<SelectListItem> DeliveryStatuSelectListItem { get; set; }
        public DeliveryStatus DeliveryStatus { get; set; }
        public List<DeliveryAddress> DeliveryAddresses { get; set; } 
        public List<ReorderNotification> ReorderNotifications { get; set; }
    }
    public class OrderArrayJsonView
    {
        public int Quantity { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public string Amount { get; set; }
        public ProductVariantOptions ProductVariantOptions {get;set;}
        public Person Person { get; set; }
        public DeliveryAddress DeliveryAddress { get; set; }
        public State State { get; set; }
    }

 
}