using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Abundance_SN.Areas.Admin.Models;
using Abundance_SN.Business;
using Abundance_SN.Controllers;
using Abundance_SN.Model.Model;
using Abundance_SN.Models;

namespace Abundance_SN.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class ReportController : BaseController
    {
        public ActionResult DailySalesReport()
        {
            ProductViewModel viewModel = new ProductViewModel();
            try
            {
                DateTime todaysDateTime = DateTime.Today;
                DateTime todaysDateStarTime = DateTime.Today;
                TimeSpan ts = new TimeSpan(23, 59, 0);
                todaysDateTime = todaysDateTime.Date + ts;
                ts = new TimeSpan(00, 00, 0);
                var dateFrom = todaysDateStarTime.Date + ts;
                SalesLogLogic salesLogLogic = new SalesLogLogic();
                var sales = salesLogLogic.GetModelsBy(p => (p.Date_Ordered >= dateFrom && p.Date_Ordered <= todaysDateTime));
                var dailySales = sales.Select(s => s.CartHash).Distinct().ToList();
                List<string> orderNumber = new List<string>();
                for (int i = 0; i < dailySales.Count; i++)
                {
                    orderNumber.Add(dailySales[i]);
                }
                var distinctOrder = orderNumber.Distinct().ToList();
                viewModel.GroupCountString = distinctOrder;
                viewModel.SalesLogs = salesLogLogic.GetAll();

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            return View(viewModel);
        }
        public ActionResult WeeklySaleReport()
        {
            ProductViewModel viewModel = new ProductViewModel();
            try
            {
                SalesLogLogic salesLogLogic = new SalesLogLogic();
                var sunday = DateTime.Today.AddDays(-(int) DateTime.Today.DayOfWeek);
                var sales = salesLogLogic.GetModelsBy(p => (p.Date_Ordered >= sunday && p.Date_Ordered <= DateTime.Now));
                var dailySales = sales.Select(s => s.CartHash).Distinct().ToList();
                List<string> orderNumber = new List<string>();
                for (int i = 0; i < dailySales.Count; i++)
                {
                    orderNumber.Add(dailySales[i]);
                }
                var distinctOrder = orderNumber.Distinct().ToList();
                viewModel.GroupCountString = distinctOrder;
                viewModel.SalesLogs = salesLogLogic.GetAll();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return View(viewModel);
        }

        public ActionResult UpdateDeliveryStatus()
        {
            ViewBag.DeliveryStatus = Utility.PopulateDeliveryStatuSelectListItems();
            return View();
        }

        public JsonResult GetOrderByNumber(string orderNumber)
        {
            try
            {
                CustomerViewModel viewModel = new CustomerViewModel();
                List<OrderArrayJsonView> orderArraysJson = new List<OrderArrayJsonView>();

                SalesLogLogic salesLogLogic = new SalesLogLogic();
                viewModel.SalesLogses = salesLogLogic.GetModelsBy(s => s.Cart_Hash == orderNumber);
                for (int i = 0; i < viewModel.SalesLogses.Count; i++)
                {
                    OrderArrayJsonView orderArrayJson = new OrderArrayJsonView();
                    string[] imagePath = viewModel.SalesLogses[i].Product.ImageUrl.Split('~');
                    orderArrayJson.ImageUrl = imagePath[1];
                    orderArrayJson.Name = viewModel.SalesLogses[i].Product.Name.ToUpperInvariant();
                    orderArrayJson.Quantity = viewModel.SalesLogses[i].Quantity;
                    if (viewModel.SalesLogses[i].Quantity > 1)
                    {
                        orderArrayJson.Amount = (viewModel.SalesLogses[i].Product.Price * viewModel.SalesLogses[i].Quantity).ToString();
                    }
                    else
                    {
                        orderArrayJson.Amount = viewModel.SalesLogses[i].Product.Price.ToString();
                    }
                    orderArraysJson.Add(orderArrayJson);
                }

                return Json(orderArraysJson, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ActionResult SaveDeliveryStatus(CustomerViewModel viewModel)
        {
            try
            {
                if (viewModel.DeliveryAddress.CartHash != null)
                {
                    DeliveryAddressLogic deliveryAddressLogic = new DeliveryAddressLogic();
                    DeliveryAddress deliveryAddress = deliveryAddressLogic.GetModelBy(s => s.Cart_Hash == viewModel.DeliveryAddress.CartHash);
                    if (deliveryAddress != null)
                    {
                        deliveryAddress.DeliveryStatus = viewModel.DeliveryStatus;
                        deliveryAddressLogic.Modify(deliveryAddress);
                        SetMessage("Operation Successful", Message.Category.Information);
                    }
                    else
                    {
                        DeliveryAddress deliveryAdd = new DeliveryAddress();
                        //deliveryAdd.Address=viewModel.DeliveryAddress.

                    }
                    ReorderNotificationLogic reorderNotificationLogic = new ReorderNotificationLogic();
                    ReorderNotification reorderNotification = reorderNotificationLogic.GetModelBy(x => x.Order_Number == viewModel.DeliveryAddress.CartHash);
                    if(reorderNotification!=null)
                    {
                        reorderNotification.Status = false;
                        reorderNotificationLogic.Modify(reorderNotification);
                    }
                }
                else
                {
                    SetMessage("Operation Failed", Message.Category.Error);
                }
               
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            //return RedirectToAction("UpdateDeliveryStatus", "Report", new {area = "Admin"});
            return RedirectToAction("ViewPaidOrder", "Customer", new { area = "Admin" });
        }

        public ActionResult DeliveryReport()
        {
            CustomerViewModel viewModel = new CustomerViewModel();
            try
            {
                DeliveryAddressLogic deliveryLogic = new DeliveryAddressLogic();
                viewModel.DeliveryAddresses = deliveryLogic.GetAll();
            }
            catch (Exception ex)
            {
                    
                throw ex;
            }
            return View(viewModel);
        }
        public ActionResult ViewDeliveredOrders()
        {
            CustomerViewModel viewModel = new CustomerViewModel();
            try
            {
                DeliveryAddressLogic deliveryAddressLogic = new DeliveryAddressLogic();
                viewModel.DeliveryAddresses = deliveryAddressLogic.GetModelsBy(p => p.Delivery_Status_Id == 3);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return View(viewModel);
        }
        public JsonResult GetChartData()
        {

            ProductViewModel viewModel = new ProductViewModel();
            try
            {
                DateTime todaysDateTime = DateTime.Today;
                DateTime todaysDateWeekStart = todaysDateTime.Date.AddDays(-7);
                TimeSpan ts = new TimeSpan(23, 59, 0);
                todaysDateTime = todaysDateTime.Date + ts;
                ts = new TimeSpan(00, 00, 0);
                var dateFrom = todaysDateWeekStart.Date + ts;
                SalesLogLogic salesLogLogic = new SalesLogLogic();
                List<SalesLogs> salesLogses = salesLogLogic.GetModelsBy(s => s.Date_Ordered >= dateFrom && s.Date_Ordered <= todaysDateTime);
                
                var distinctSoldProducts = salesLogses.Select(s => s.Product.Id).Distinct().Take(5).ToList();
                for (int i = 0; i < distinctSoldProducts.Count; i++)
                {
                    long currentProductId = distinctSoldProducts[i];
                    viewModel.SalesLogs = salesLogLogic.GetModelsBy(s => s.Product_Id == currentProductId);
                    viewModel.SalesLogs.FirstOrDefault().Quantity = viewModel.SalesLogs.Count;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }
    }
}