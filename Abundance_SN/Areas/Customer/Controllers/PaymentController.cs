using System;
using System.Activities.Debugger;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Abundance_SN.Business;
using Abundance_SN.Controllers;
using Abundance_SN.Model.Model;
using Abundance_SN.Models;



namespace Abundance_SN.Areas.Customer.Controllers
{
    public class PaymentController : BaseController
    {
        // GET: Customer/Payment
        public ActionResult Index()
        {
            ProductViewModel productViewModel = new ProductViewModel();
            try
            {
                InventoryLogic inventoryLogic = new InventoryLogic();
                ProductLogic productLogic = new ProductLogic();
                productViewModel.Products = new List<Product>();
                List<long> distinctpoductId = inventoryLogic.GetModelsBy(s => s.PRODUCT.Visits > 10 && s.Quantity > 0).Select(s => s.Product.Id).Distinct().ToList();
                for (int i = 0; i < distinctpoductId.Count; i++)
                {
                    Product product = new Product();
                    long productId = distinctpoductId[i];
                    product = productLogic.GetModelBy(p => p.Id == productId);
                    productViewModel.Products.Add(product);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return View(productViewModel);
        }

        public JsonResult MakePayment(string myRecordArray, string surname, string firstName, string middleName, string mobile, string landMark, string city, string state, string email, string shippingAddress, string paymentMode, string uuid, string deliveryMode, decimal deliveryCharge)
        {
            ArrayJsonView arrayJsonModel = new ArrayJsonView();
            List<SalesLogs> salesLogsList = new List<SalesLogs>();
            List<Product> products = new List<Product>();
            List<long> productIdList = new List<long>();
            List<long> productVariantOption = new List<long>();
            var getproductId = 0;
            long getproductVariantOptionId = 0;
            List<string> orderNumberList = new List<string>();
            var orderId = "";
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                List<CheckOutJson> arrayJsonView = serializer.Deserialize<List<CheckOutJson>>(myRecordArray);
                ProductLogic productLogic = new ProductLogic();
                SalesLogLogic salesLogLogic = new SalesLogLogic();
                InventoryLogic inventoryLogic = new InventoryLogic();
                UserLogic userLogic = new UserLogic();
                PersonLogic personLogic = new PersonLogic();
                PaymentLogic paymentLogic = new PaymentLogic();
                DeliveryAddressLogic deliveryServiceLogic = new DeliveryAddressLogic();
                ProductVariantOptionsLogic productVariantOptionLogic = new ProductVariantOptionsLogic();
                Person person = new Person();
                User user = new User();
                if (!string.IsNullOrEmpty(User.Identity.Name))
                {
                    //user = userLogic.GetModelBy(u => u.Name == User.Identity.Name);
                    user = userLogic.GetModelBy(u => u.Email== User.Identity.Name);
                }

                if (user.Id == 0 && string.IsNullOrEmpty(user.Name) && !string.IsNullOrEmpty(uuid))
                {
                    //var checkAnonymousUser = personLogic.GetModelBy(p => p.Uuid == uuid);
                    var checkAnonymousUser = personLogic.GetModelBy(x => x.Email == email);
                    if (checkAnonymousUser == null)
                    {
                        checkAnonymousUser = personLogic.GetModelsBy(p => p.Email == email).LastOrDefault();
                        if (checkAnonymousUser != null)
                        {
                            user = checkAnonymousUser.User;
                            person = checkAnonymousUser;
                        }
                        else
                        {
                            using (var transaction = new TransactionScope())
                            {
                                user = new User()
                                {
                                    Name = surname,
                                    Email = email,
                                    Password = "1234567",
                                    Role = new Role()
                                    {
                                        Id = 2
                                    }
                                };
                                var createdUser = userLogic.Create(user);
                                if (createdUser != null)
                                {
                                    user = createdUser;
                                    person.FirstName = firstName;
                                    person.Surname = surname;
                                    person.OtherName = middleName;
                                    person.City = city;
                                    person.LandMark = landMark;
                                    person.PhoneNumber = mobile;
                                    person.Address = landMark + " " + city;
                                    person.Email = email;
                                    person.StateId = state;
                                    person.User = user;
                                    person.Uuid = uuid;
                                    var createdPerson = personLogic.Create(person);
                                    if (createdPerson != null)
                                    {
                                        person = createdPerson;
                                    }
                                }
                                transaction.Complete();
                            }
                            
                        }

                    }
                    else
                    {
                        user = checkAnonymousUser.User;
                        person = checkAnonymousUser;
                    }

                }
                person = personLogic.GetModelBy(p => p.User_Id == user.Id);
                string uniqueCode = UniqueCode();
                string orderNumber = "ES" + 09 + uniqueCode + 00;

                if (!string.IsNullOrEmpty(landMark) && !string.IsNullOrEmpty(city) && !string.IsNullOrEmpty(state) || !string.IsNullOrEmpty(shippingAddress))
                {
                    //if (paymentMode == "1")
                    if (deliveryMode == "1")
                    {
                        
                           DeliveryAddress deliveryAddress = new DeliveryAddress();
                        if (string.IsNullOrEmpty(shippingAddress))
                        {
                            deliveryAddress.Address = landMark + " " + city;
                        }
                        else
                        {
                            deliveryAddress.Address = shippingAddress + " " + city;
                        }
                        deliveryAddress.StateId = state;
                        deliveryAddress.DateCreated = DateTime.Now.Date;
                        deliveryAddress.Person = person;
                        deliveryAddress.CartHash = orderNumber;
                        deliveryAddress.DeliveryStatus = new DeliveryStatus()
                        {
                            Id = 1
                        };
                        deliveryAddress.DeliveryService = new DeliveryService()
                        {
                            Id = Convert.ToInt32(deliveryMode)
                        };
                        deliveryAddress.DeliveryOption = 0;
                        deliveryServiceLogic.Create(deliveryAddress);
                    }
                    

                }
                if (deliveryMode == "3")
                {
                    DeliveryAddress deliveryAddress = new DeliveryAddress();

                    deliveryAddress.Address = "To Pick in Store";
                    deliveryAddress.StateId = "EN";
                    deliveryAddress.DateCreated = DateTime.Now.Date;
                    deliveryAddress.Person = person;
                    deliveryAddress.CartHash = orderNumber;
                    deliveryAddress.DeliveryStatus = new DeliveryStatus()
                    {
                        Id = 1
                    };
                    deliveryAddress.DeliveryService = new DeliveryService()
                    {
                        Id = Convert.ToInt32(deliveryMode)
                    };
                    deliveryAddress.DeliveryOption = 1;
                    deliveryServiceLogic.Create(deliveryAddress);
                }

                for (int i = 0; i < arrayJsonView.Count; i++)
                {
                    int productId = Convert.ToInt32(arrayJsonView[i].id);
                    Product product = productLogic.GetModelBy(p => p.Id == productId);
                    product = productLogic.GetModelBy(p => p.Id == productId);
                    product.Quantity = Convert.ToInt32(arrayJsonView[i].quantity);

                    long productOptionId = Convert.ToInt64(arrayJsonView[i].productOptionId);
                    if (productOptionId != 0)
                    {
                        Inventory inventory = inventoryLogic.GetModelBy(inv => inv.Product_Id == product.Id && inv.Product_Variant_Option_Id == productOptionId);
                        if (inventory != null)
                        {
                            inventoryLogic.Modify(product, productOptionId);
                        }
                    }
                    getproductId = productId;
                    getproductVariantOptionId = productOptionId;
                    products.Add(product);
                    productIdList.Add(productId);
                    productVariantOption.Add(productOptionId);

                    //compare the inventory quantity with the reoderlevel, for notification
                    var reorderinventory = inventoryLogic.GetModelBy(x => x.Product_Variant_Option_Id == getproductVariantOptionId && x.Product_Id == getproductId);
                    var reorderproduct = productLogic.GetModelBy(x => x.Id == getproductId);
                    var variantOption = productVariantOptionLogic.GetModelBy(x => x.Product_Option_Id == getproductVariantOptionId);
                    if (reorderinventory.Quantity == reorderproduct.ReorderLevel || reorderinventory.Quantity < reorderproduct.ReorderLevel)
                    {
                        string notificationmessage = reorderproduct.Name + " " + " " + variantOption.ProductOptionName;
                        ReorderNotificationLogic reorderNotificationLogic = new ReorderNotificationLogic();
                        ReorderNotification reorderNotification = new ReorderNotification();
                        reorderNotification.Message = notificationmessage + " " + " " + "need to be Re-ordered";
                        reorderNotification.Status = true;
                        reorderNotification.NotificationType = 1;
                        reorderNotificationLogic.Create(reorderNotification);
                    }
                    if (user != null)
                    {
                        ProductVariantOptions productVariantOptions = productVariantOptionLogic.GetModelBy(po => po.Product_Option_Id == productOptionId);
                        SalesLogs salesLogs = new SalesLogs();
                        var price = productVariantOptions.Price > 0 ? productVariantOptions.Price : product.DiscountAmount;
                        if(product.Discount>0)
                        {
                            var discountedValue= (price * product.Discount / 100);
                            price = price - discountedValue;
                        }
                        
                        if (product.Quantity > 1)
                        {
                                salesLogs.Amount = (decimal)(price * product.Quantity);

                        }
                        else
                        {
                            
                            salesLogs.Amount = (decimal)price;

                        }
                        if(deliveryCharge>0)
                        {
                            salesLogs.Amount += deliveryCharge;
                        }
                        salesLogs.CartHash = orderNumber;
                        //salesLogs.UnitPrice = product.Price;
                        salesLogs.UnitPrice = (decimal)price;
                        salesLogs.Product = product;
                        salesLogs.Quantity = product.Quantity;
                        salesLogs.User = user;
                        orderId = salesLogs.CartHash;


                        if (productOptionId != 0)
                        {
                            salesLogs.ProductVariantOptions = new ProductVariantOptions()
                            {
                                Id = Convert.ToInt64(arrayJsonView[i].productOptionId),
                            };
                        }
                        else
                        {
                            ProductVariantOptionsLogic variantOptionLogic = new ProductVariantOptionsLogic();
                            salesLogs.ProductVariantOptions = variantOptionLogic.GetModelBy(s => s.PRODUCT_VARIANT.Product_Id == product.Id);
                        }

                        salesLogsList.Add(salesLogs);
                        orderNumberList.Add(orderId);


                    }

                }



                Payment payment = new Payment();
                payment.Person = personLogic.GetModelBy(p => p.User_Id == user.Id);
                payment.PaymentMode = new PaymentMode()
                {
                    Id = Convert.ToInt32(paymentMode)
                };
                payment.InvoiceNumber = orderNumber;
                payment.DatePaid = DateTime.Now.Date;
                var createdPayment = paymentLogic.Create(payment);
                salesLogLogic.Create(salesLogsList);
                arrayJsonModel.CustomerOrderNumber = orderNumber;
                arrayJsonModel.paymentMode = paymentMode;
                if (arrayJsonModel.CustomerOrderNumber != null)
                {
                    PaystackRepsonse paystackRepsonse = new PaystackRepsonse();

                    string PaystackSecrect = ConfigurationManager.AppSettings["PaystackSecrect"].ToString();
                    string PaystackSubAccount = ConfigurationManager.AppSettings["PaystackSubAccount"].ToString();

                    if (!String.IsNullOrEmpty(PaystackSecrect))
                    {
                        PayStackLogic paystackLogic = new PayStackLogic();
                        Paystack payStack = new Paystack();
                        using (var transaction = new TransactionScope())
                        {
                            decimal orderAmount = 0;
                            for (int i = 0; i < salesLogsList.Count; i++)
                            {
                                orderAmount += salesLogsList[i].Amount;
                            }
                            if (!String.IsNullOrEmpty(PaystackSecrect))
                            {
                                decimal amount = 0;
                                amount = orderAmount;
                                decimal commission = 0;
                                commission = 100;

                                paystackRepsonse = paystackLogic.MakePayment(payment, PaystackSecrect, PaystackSubAccount, amount, commission);
                                if (paystackRepsonse != null && paystackRepsonse.status && !String.IsNullOrEmpty(paystackRepsonse.data.authorization_url))
                                {
                                    paystackRepsonse.Paystack = new Paystack();
                                    if (payment != null)
                                    {
                                        payment.Id = createdPayment.Id;
                                        paystackRepsonse.Paystack.Payment = payment;
                                    }
                                    arrayJsonModel.PayStackUrl = paystackRepsonse.data.authorization_url;
                                    paystackRepsonse.Paystack.authorization_url = paystackRepsonse.data.authorization_url;
                                    paystackRepsonse.Paystack.access_code = paystackRepsonse.data.access_code;
                                    payStack = paystackLogic.Create(paystackRepsonse.Paystack);

                                }
                            }
                            transaction.Complete();
                        }

                            if (orderNumber != null)
                            {
                            //salesLogsList
                            //ProductViewModel viewModel = new ProductViewModel();
                            //viewModel.SalesLogs = new List<SalesLogs>();
                            List<SalesLogs> saleLogList = new List<SalesLogs>();
                                if(productIdList.Count>0 && productVariantOption.Count>0)
                                {
                                    for(int p=0;p< productIdList.Count;p++)
                                    {
                                        var firstItem = productIdList[p];
                                        var secondItem = productVariantOption[p];
                                    saleLogList=salesLogLogic.GetAll().Where(x => x.Product.Id == firstItem && x.CartHash == orderNumber && x.ProductVariantOptions.Id== secondItem).ToList();
                                        for (int i = 0; i < saleLogList.Count; i++)
                                        {
                                            string notificationmessage = saleLogList[i].Quantity + " " + " " + saleLogList[i].Product.Name + " " + " " + saleLogList[i].ProductVariantOptions.ProductOptionName;
                                            ReorderNotificationLogic reorderNotificationLogic = new ReorderNotificationLogic();
                                            ReorderNotification reorderNotification = new ReorderNotification();
                                            reorderNotification.Message = notificationmessage + " " + " " + "was(were) Ordered";
                                            reorderNotification.Status = true;
                                            reorderNotification.NotificationType = 2;
                                        reorderNotification.OrderNumber = orderNumber;
                                            reorderNotificationLogic.Create(reorderNotification);
                                        }
                                    }
                                }
                                

                            }
                       
                    }
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Json(arrayJsonModel, JsonRequestBehavior.AllowGet);
        }

        public static string PaddNumber(long id, int maxCount)
        {
            try
            {
                string idInString = id.ToString();
                string paddNumbers = "";
                if (idInString.Count() < maxCount)
                {
                    int zeroCount = maxCount - id.ToString().Count();
                    StringBuilder builder = new StringBuilder();
                    for (int counter = 0; counter < zeroCount; counter++)
                    {
                        builder.Append("0");
                    }

                    builder.Append(id);
                    paddNumbers = builder.ToString();
                    return paddNumbers;
                }
                else
                {
                    paddNumbers = id.ToString();
                }

                return paddNumbers;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string UniqueCode()
        {
            try
            {
                string str = "0123456789";

                string code = "";

                code = Shuffle(str).Substring(0, 10).ToUpper();

                return code;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public string Shuffle(string str)
        {
            char[] array = str.ToCharArray();
            Random rng = new Random();
            int n = array.Length;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                var value = array[k];
                array[k] = array[n];
                array[n] = value;
            }
            return new string(array);
        }


        public JsonResult PayStackPayment(string customerId)
        {
            PaystackRepsonse paystackRepsonse = new PaystackRepsonse();
            try
            {

                if (string.IsNullOrEmpty(customerId))
                {
                    throw new ArgumentNullException("customerId", "Could Not Find Customer");
                }

                string PaystackSecrect = ConfigurationManager.AppSettings["PaystackSecrect"].ToString();
                string PaystackSubAccount = ConfigurationManager.AppSettings["PaystackSubAccount"].ToString();

                if (!String.IsNullOrEmpty(PaystackSecrect))
                {
                    PayStackLogic paystackLogic = new PayStackLogic();
                    SalesLogLogic salesLogLogic = new SalesLogLogic();
                    PaymentLogic paymentLogic = new PaymentLogic();
                    Paystack payStack = new Paystack();
                    using (var transaction = new TransactionScope())
                    {
                        decimal orderAmount = 0;


                        List<SalesLogs> salesOrderList = salesLogLogic.GetModelsBy(co => co.Cart_Hash == customerId);
                        for (int i = 0; i < salesOrderList.Count; i++)
                        {
                            orderAmount += salesOrderList[i].Amount;
                        }
                        Payment payment = paymentLogic.GetModelBy(p => p.Invoice_Number == customerId);
                        if (!String.IsNullOrEmpty(PaystackSecrect))
                        {
                            decimal amount = 0;
                            amount = orderAmount;
                            decimal commission = 0;
                            commission = 100;

                            paystackRepsonse = paystackLogic.MakePayment(payment, PaystackSecrect, PaystackSubAccount, amount, commission);
                            if (paystackRepsonse != null && paystackRepsonse.status && !String.IsNullOrEmpty(paystackRepsonse.data.authorization_url))
                            {
                                paystackRepsonse.Paystack = new Paystack();

                                if (payment != null)
                                {
                                    paystackRepsonse.Paystack.Payment = payment;
                                }
                                paystackRepsonse.Paystack.authorization_url = paystackRepsonse.data.authorization_url;
                                paystackRepsonse.Paystack.access_code = paystackRepsonse.data.access_code;
                                payStack = paystackLogic.Create(paystackRepsonse.Paystack);
                              
                            }

                        }
                        transaction.Complete();
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            if (paystackRepsonse != null && paystackRepsonse.Paystack.authorization_url != null)
            {
                var payStackUrl = paystackRepsonse.Paystack.authorization_url;
                return Json(payStackUrl, JsonRequestBehavior.AllowGet);
            }

            return Json("Error", JsonRequestBehavior.AllowGet);
        }

        public ActionResult OrderPreview()
        {
            ProductViewModel productViewModel = new ProductViewModel();
            try
            {
                InventoryLogic inventoryLogic = new InventoryLogic();
                ProductLogic productLogic = new ProductLogic();
                productViewModel.Products = new List<Product>();
                List<long> distinctpoductId = inventoryLogic.GetModelsBy(s => s.PRODUCT.Visits > 10 && s.Quantity > 0).Select(s => s.Product.Id).Distinct().ToList();
                for (int i = 0; i < distinctpoductId.Count; i++)
                {
                    Product product = new Product();
                    long productId = distinctpoductId[i];
                    product = productLogic.GetModelBy(p => p.Id == productId);
                    productViewModel.Products.Add(product);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return View(productViewModel);
        }

        public ActionResult Cart()
        {
            ProductViewModel viewModel = new ProductViewModel();
            try
            {
                InventoryLogic inventoryLogic = new InventoryLogic();
                ProductLogic productLogic = new ProductLogic();
                viewModel.Inventorieses = inventoryLogic.GetModelsBy(s => s.Quantity > 0 && s.PRODUCT.Active);
                var distinctProducts = viewModel.Inventorieses.Select(s => s.Product.Id).Distinct().ToList();
                viewModel.Products = new List<Product>();
                for (int i = 0; i < distinctProducts.Count; i++)
                {
                    Product product = new Product();
                    long productId = distinctProducts[i];
                    product = productLogic.GetModelBy(s => s.Id == productId);
                    viewModel.Products.Add(product);
                }
                var distinctBrand = viewModel.Inventorieses.Select(b => b.Product.Brand.Id).Distinct().ToList();
                viewModel.GroupCount = distinctBrand;
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