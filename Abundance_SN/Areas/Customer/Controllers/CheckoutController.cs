using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Abundance_SN.Business;
using Abundance_SN.Model.Model;
using Abundance_SN.Models;
using Abundance_SN.Areas.Admin.Models;
using Abundance_SN.Areas.Admin.Controllers;

namespace Abundance_SN.Areas.Customer.Controllers
{
    public class CheckoutController : Controller
    {
        // GET: Customer/Checkout
        public ActionResult Checkout(string id)
        {
            ProductViewModel productViewModel = new ProductViewModel();
            try
            {
                InventoryLogic inventoryLogic = new InventoryLogic();
                ProductLogic productLogic = new ProductLogic();
                if (Request.IsAuthenticated && !string.IsNullOrEmpty(id))
                {
                    productViewModel.Products = new List<Product>();
                    ViewBag.StateId = productViewModel.PopulateCourierStateSelectListItem;
                    //ViewBag.CourierId = productViewModel.populateCourierServiceSelectListItem;
                    List<long> distinctpoductId = inventoryLogic.GetModelsBy(s => s.PRODUCT.Visits > 10 && s.Quantity > 0).Select(s => s.Product.Id).Distinct().ToList();
                    for (int i = 0; i < distinctpoductId.Count; i++)
                    {
                        long productId = distinctpoductId[i];
                        Product product = productLogic.GetModelBy(p => p.Id == productId);
                        productViewModel.Products.Add(product);
                    }
                    //passing logged in user information to the view
                    PersonLogic personlogic = new PersonLogic();
                    productViewModel.Person = personlogic.GetModelBy(x => x.Email == User.Identity.Name);
                    
                }
                else if (!Request.IsAuthenticated && !string.IsNullOrEmpty(id) )
                {
                      productViewModel.Products = new List<Product>();
                    ViewBag.StateId = productViewModel.PopulateCourierStateSelectListItem;
                    //ViewBag.CourierId = productViewModel.populateCourierServiceSelectListItem;
                    List<long> distinctpoductId = inventoryLogic.GetModelsBy(s => s.PRODUCT.Visits > 10 && s.Quantity > 0).Select(s =>s.Product.Id).Distinct().ToList();
                    for (int i = 0; i < distinctpoductId.Count; i++)
                    {
                        long productId = distinctpoductId[i];
                        Product product = productLogic.GetModelBy(p => p.Id == productId);
                        productViewModel.Products.Add(product);
                    }
                }
                else
                {
                    string returnUrl = "Customer/Checkout/Checkout";
                    return RedirectToAction("login", "Account", new { area = "Security", returnUrl});
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return View(productViewModel);
        }

        public JsonResult AssignCheckOutItems(string myRecordArray)
        {
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                List<ArrayJsonView> arrayJsonView = serializer.Deserialize<List<ArrayJsonView>>(myRecordArray);

                List<ArrayJsonView> arrayJsonModel = new List<ArrayJsonView>();

                ProductLogic productLogic = new ProductLogic();

                for (int i = 0; i < arrayJsonView.Count; i++)
                {
                    ProductViewModel model = new ProductViewModel();
                    model.Quantity = Convert.ToInt32(arrayJsonView[i].quantity);
                    int productId = Convert.ToInt32(arrayJsonView[i].item_Id);
                    model.Product = productLogic.GetModelBy(p => p.Id == productId);

                    string[] imagePath = model.Product.ImageUrl.Split('~');
                    arrayJsonView[i].ProductImageUrl = imagePath[1];
                    arrayJsonView[i].item_name = model.Product.Name;
                    arrayJsonView[i].quantity = model.Quantity;
                    arrayJsonView[i].amount = model.Product.Price.ToString();
                    if (model.Quantity > 1)
                    {
                        arrayJsonView[i].Price = (model.Product.Price * model.Quantity).ToString();
                    }
                    else
                    {
                        arrayJsonView[i].Price = model.Product.Price.ToString();
                    }

                    productLogic.UpdateVisit(model.Product);
                    arrayJsonModel.Add(arrayJsonView[i]);
                }

                return Json(arrayJsonModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ActionResult ViewOrders()
        {
            ProductViewModel viewModel = new ProductViewModel();
            try
            {
                UserLogic userLogic = new UserLogic();
                SalesLogLogic salesLogic = new SalesLogLogic();
                PaymentLogic paymentLogic = new PaymentLogic();
                PayStackLogic payStackLogic = new PayStackLogic();
                 viewModel.Payments = new List<Payment>();
                viewModel.DeliveryAddresses = new List<DeliveryAddress>();
                viewModel.PayStackPayments = new List<Paystack>();
                PersonLogic personLogic = new PersonLogic(); 
                DeliveryAddressLogic deliveryAddressLogic = new DeliveryAddressLogic();
                //User user = userLogic.GetModelBy(u => u.Name == User.Identity.Name);
                User user = userLogic.GetModelBy(u => u.Email == User.Identity.Name);
                if (user != null)
                {
                    viewModel.SalesLogs = salesLogic.GetModelsBy(s => s.User_Id == user.Id);
                    viewModel.Person = personLogic.GetModelBy(p => p.User_Id == user.Id);
                    List<string> orderNumber = new List<string>();
                    for (int i = 0; i < viewModel.SalesLogs.Count; i++)
                    {
                        orderNumber.Add(viewModel.SalesLogs[i].CartHash);
                    }
                    var distinctOrder = orderNumber.Distinct().ToList();
                    viewModel.GroupCountString = distinctOrder;
                    for (int i = 0; i < viewModel.GroupCountString.Count; i++)
                    {
                        var order = viewModel.GroupCountString[i];
                        Payment payment = paymentLogic.GetModelBy(p => p.Invoice_Number == order);;
                        DeliveryAddress deliveryAddress = deliveryAddressLogic.GetModelBy(da => da.Cart_Hash == order);
                       
                        viewModel.Payments.Add(payment);
                        if (deliveryAddress != null)
                        {
                            viewModel.DeliveryAddresses.Add(deliveryAddress); 
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            return View(viewModel);
        }

        public ActionResult ViewOrdersByNumber(string orderNumber)
        {
            ProductViewModel viewModel = new ProductViewModel();
            
            try
            {
                SalesLogLogic salesLogLogic = new SalesLogLogic();
                PaymentLogic paymentLogic = new PaymentLogic();
                PayStackLogic payStackLogic = new PayStackLogic();
                viewModel.SalesLogs = salesLogLogic.GetModelsBy(s => s.Cart_Hash == orderNumber);
                viewModel.Payment = paymentLogic.GetModelBy(p => p.Invoice_Number == orderNumber);
                if (viewModel.Payment != null)
                {
                    viewModel.PayStack = payStackLogic.GetModelBy(p => p.Payment_Id == viewModel.Payment.Id);
                }
               
            }
            catch (Exception)
            {
                    
                throw;
            }
            return View(viewModel);
        }

        public JsonResult CheckInventory(string productId, string productOptionId)
        {
            try
            {
                int inventoryQuantity = 0;
                if (string.IsNullOrEmpty(productId) || string.IsNullOrEmpty(productOptionId))
                {
                    //throw new ArgumentNullException("Id");
                }
                int prodId = Convert.ToInt32(productId);
                int prodOptId = Convert.ToInt32(productOptionId);
                InventoryLogic inventoryLogic = new InventoryLogic();
                var productInventory = inventoryLogic.GetModelBy(x => x.Product_Variant_Option_Id == prodOptId && x.Product_Id == prodId);
                if (productInventory != null)
                {
                    inventoryQuantity = productInventory.Quantity;
                }

                return Json(inventoryQuantity, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }
        public JsonResult CourierArea(string stateId)
        {
            List<CourierAreaCharges> areaList = new List<CourierAreaCharges>();
            JsonResultModel result = new JsonResultModel();
            string message = "";
            try
            {
                if(String.IsNullOrEmpty(stateId))
                {
                    message = "No State Selected";
                }

                
                CourierAreaCharges courierAreaCharges = new CourierAreaCharges();
                CourierAreaChargesLogic courierAreaChargesLogic = new CourierAreaChargesLogic();
                var area  = courierAreaChargesLogic.GetModelsBy(x => x.State_Id == stateId);
                if(area !=null && area.Count>0)
                {
                    for (int i = 0; i < area.Count; i++)
                    {
                        courierAreaCharges.Area = area[i].Area;
                        courierAreaCharges.Id = area[i].Id;
                        areaList.Add(courierAreaCharges);
                    }
                    result.IsError = false;

                }
                //courierAreaCharges.CourierService = new CourierService();
                //CourierServiceLogic courierServiceLogic = new CourierServiceLogic();
                //var courierService=courierServiceLogic.GetAll();
                //if(courierService!=null && courierService.Count>0)
                //{
                //    for(int i=0;i<courierService.Count;i++)
                //    {
                //        courierAreaCharges.CourierService.Courier_Id = courierService[i].Courier_Id;
                //        courierAreaCharges.CourierService.Courier_Name = courierService[i].Courier_Name;
                //        areaList.Add(courierAreaCharges);
                //    }
                //    result.IsError = false;
                //}
                

            }
            catch(Exception ex)
            {
                result.IsError = true;
                result.Message = ex.Message;
            }
            return Json(areaList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CourierCharge(string stateId, string areaId)
        {
            List<CourierDetail> courierDetails = new List<CourierDetail>();
            CourierServiceLogic courierServiceLogic = new CourierServiceLogic();
            var preferredCourier=courierServiceLogic.GetAll().FirstOrDefault();
            JsonResultModel result = new JsonResultModel();
            string message = "";
            try
            {
                if (String.IsNullOrEmpty(stateId)|| String.IsNullOrEmpty(areaId) || preferredCourier.Courier_Id<=0)
                {
                    message = "Make Proper Selection For Delivery";
                }
                int id = Convert.ToInt32(areaId);
                //int courierId = Convert.ToInt32(serviceId);
                CourierDetail courierDetail = new CourierDetail();
                courierDetail.CourierCharges = new CourierCharges();
                CourierDetailLogic courierDetailLogic = new CourierDetailLogic();
                var detail = courierDetailLogic.GetModelsBy(x => x.State_Id == stateId && x.Courier_Id== preferredCourier.Courier_Id && x.Area_Id==id);
                if (detail != null && detail.Count > 0)
                {
                    for (int i = 0; i < detail.Count; i++)
                    {
                        courierDetail.Id = detail[i].Id;
                        courierDetail.charges = Convert.ToDecimal(detail[i].CourierCharges.Charges);
                        courierDetails.Add(courierDetail);
                    }
                    result.IsError = false;

                }

            }
            catch (Exception ex)
            {
                result.IsError = true;
                result.Message = ex.Message;
            }
            return Json(courierDetails, JsonRequestBehavior.AllowGet);
        }
    }
}