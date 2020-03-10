using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Abundance_SN.Areas.Admin.Models;
using Abundance_SN.Business;
using Abundance_SN.Model.Model;
using Abundance_SN.Models;

namespace Abundance_SN.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class CustomerController : Controller
    {
        // GET: Admin/Customer
        public ActionResult ViewCustomerOrder()
        {
            CustomerViewModel customerViewModel = new CustomerViewModel();
            try
            {
                 
                SalesLogLogic salesLogic = new SalesLogLogic();
                customerViewModel.SalesLogses = salesLogic.GetAll();
                List<string> orderNumber = new List<string>();
                for (int i = 0; i < customerViewModel.SalesLogses.Count; i++)
                {
                    orderNumber.Add(customerViewModel.SalesLogses[i].CartHash);
                }
                var distinctOrder = orderNumber.Distinct().ToList();
                customerViewModel.GroupCountString = distinctOrder;
               
            }
            catch (Exception)
            {
                
                throw;
            }
            return View(customerViewModel);
        }
        public JsonResult ViewOrdersByNumber(string orderNumber)
        {
            try
            {
                CustomerViewModel viewModel = new CustomerViewModel();
                List<OrderArrayJsonView> orderArraysJson = new List<OrderArrayJsonView>();
               
                    SalesLogLogic salesLogLogic = new SalesLogLogic();
                    ProductVariantOptionsLogic productVariantOptionsLogic = new ProductVariantOptionsLogic();
                DeliveryAddressLogic deliveryAddressLogic = new DeliveryAddressLogic();
                StateLogic stateLogic = new StateLogic();
                PersonLogic personLogic = new PersonLogic();
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
                        //orderArrayJson.Amount = (viewModel.SalesLogses[i].Product.Price * viewModel.SalesLogses[i].Quantity).ToString();
                        orderArrayJson.Amount = (viewModel.SalesLogses[i].Amount.ToString());
                    }
                    else
                    {
                        //orderArrayJson.Amount = viewModel.SalesLogses[i].Product.Price.ToString();
                        orderArrayJson.Amount = viewModel.SalesLogses[i].Amount.ToString();
                    }
                    long productVariantOptionId = viewModel.SalesLogses[i].ProductVariantOptions.Id;
                    var userId = viewModel.SalesLogses[i].User.Id;
                    var cartHarsh = viewModel.SalesLogses[i].CartHash;
                    orderArrayJson.ProductVariantOptions = productVariantOptionsLogic.GetModelBy(pv => pv.Product_Option_Id == productVariantOptionId);
                    //getting customer's detail
                    orderArrayJson.Person = personLogic.GetModelBy(x => x.User_Id == userId);
                    
                    orderArrayJson.DeliveryAddress = deliveryAddressLogic.GetModelBy(x => x.Cart_Hash == cartHarsh);
                    if(orderArrayJson.DeliveryAddress!=null)
                    {
                        var stateId = orderArrayJson.DeliveryAddress.StateId;
                        orderArrayJson.State = stateLogic.GetModelBy(x => x.State_Id == stateId);
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

        public ActionResult ViewPaidOrder()
        {
            CustomerViewModel viewModel = new CustomerViewModel();
            DeliveryAddressLogic deliveryAddressLogic = new DeliveryAddressLogic();
            try
            {
                PaymentLogic paymentLogic = new PaymentLogic();
                SalesLogLogic salesLogLogic = new SalesLogLogic();
                viewModel.Payments = paymentLogic.GetPaidOrder().OrderByDescending(x=>x.Tansactiondate).ToList();

                List<string> orderNumber = new List<string>();
                for (int i = 0; i < viewModel.Payments.Count; i++)
                {
                    orderNumber.Add(viewModel.Payments[i].InvoiceNumber);
                }
                var distinctOrder = orderNumber.Distinct().ToList();
                viewModel.SalesLogses = salesLogLogic.GetAll();
                viewModel.GroupCountString = distinctOrder;
                viewModel.DeliveryAddresses = deliveryAddressLogic.GetAll();

                ViewBag.DeliveryStatus = Utility.PopulateDeliveryStatuSelectListItems();

            }
            catch (Exception)
            {
                
                throw;
            }
            return View(viewModel);
        }

        public ActionResult ViewCashOnDeliveryOrder()
        {
            CustomerViewModel viewModel = new CustomerViewModel();
            try
            {
                PaymentLogic paymentLogic = new PaymentLogic();
                viewModel.PaymentList = paymentLogic.GetModelsBy(p => p.Payment_Mode_Id == 1 && p.Paid == false || p.Paid == null);
            }
            catch (Exception)
            {
                
                throw;
            }
            return View(viewModel);
        }

    }
}