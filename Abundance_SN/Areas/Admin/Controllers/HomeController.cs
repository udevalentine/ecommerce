using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Abundance_SN.Business;
using Abundance_SN.Controllers;
using Abundance_SN.Model.Model;
using Abundance_SN.Models;
using System.Transactions;
using Abundance_SN.Areas.Admin.Models;
using System.Web.Script.Serialization;

namespace Abundance_SN.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class HomeController : BaseController
    {
        private const string ID = "Id";
        private const string NAME = "Name";
        private const string VALUE = "Value";
        private const string TEXT = "Text";
        public bool IsError { get; set; }
        // GET: Admin/Home
        public ActionResult Dashboard()
        {
            ProductViewModel productViewModel = new ProductViewModel();

            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    PersonLogic personLogic = new PersonLogic();
                    UserLogic userLogic = new UserLogic();
                    ProductLogic productLogic = new ProductLogic();
                    SalesLogLogic saleslogic = new SalesLogLogic();
                    InventoryLogic inventoryLogic = new InventoryLogic();
                    ReorderNotificationLogic reorderNotificationLogic = new ReorderNotificationLogic();
                    var newOrder = reorderNotificationLogic.GetAll().Where(x => x.NotificationType == 2 && x.Status).GroupBy(y=>y.OrderNumber).ToList();
                    ViewBag.NewOrder = newOrder.Count();
                    List<Product> products = productLogic.GetAll();
                    productViewModel.Products = products.OrderByDescending(s => s.Visits).Take(5).ToList();
                    List<SalesLogs> salesLogs = saleslogic.GetAll();
                    ViewBag.AmountSold = salesLogs.Sum(s => s.Amount).ToString("n");
                    ViewBag.TotalSales = salesLogs.Select(s => s.CartHash).Distinct().Count();
                    ViewBag.ProductsInStore = productLogic.GetAll().Count;
                    List<Inventory> inventories = inventoryLogic.GetAll();
                    ViewBag.Inventory = inventories.Select(s => s.Product.Id).Distinct().Count();
                    productViewModel.SalesLogs = salesLogs.Distinct().Take(5).OrderByDescending(s => s.Quantity).ToList();
                    List<Person> allPersons = personLogic.GetAll().ToList();

                    //List<Person> allRegisteredUsers = personLogic.GetAll().Count();
                    ViewBag.UserCount = allPersons.Count();
                    DateTime todaysDateTime = DateTime.Today;
                    DateTime todaysDateStarTime = DateTime.Today;
                    TimeSpan ts = new TimeSpan(23, 59, 0);
                    todaysDateTime = todaysDateTime.Date + ts;
                    ts = new TimeSpan(00, 00, 0);
                    var dateFrom = todaysDateStarTime.Date + ts;
                    ViewBag.TodaysSales = salesLogs.Count(s => s.DateOrdered >= dateFrom && s.DateOrdered <= todaysDateTime);
                }
                else
                {
                    return RedirectToAction("login", "Account", new { area = "Security" });
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            ViewBag.Role = TempData["RoleName"] as string;
            return View(productViewModel);
        }
        [Authorize(Roles = "SuperAdmin")]
        public ActionResult SetUp()
        {
            ViewBag.States = Utility.PopulateSateSelectListItems();
            ViewBag.ProductSamples = Utility.PopulateProductSample();
            ViewBag.StoreType = Utility.PopulateStoreTypeSelectListItem();
            return View();
        }

        [HttpPost]
        public ActionResult SetUp(RegisterViewModel viewModel)
        {
            try
            {
                if (viewModel != null)
                {
                    StoreSetupProductSampleLogic storeSetupProductSampleLogic = new StoreSetupProductSampleLogic();
                    UserLogic userLogic = new UserLogic();
                    RoleLogic roleLogic = new RoleLogic();
                    PersonLogic personLogic = new PersonLogic();
                    //StoreSetupProductSample storeSetupProductSample = storeSetupProductSampleLogic.GetModelBy(s => s.Store_Setup_ProductSample_Id == viewModel.StoreSetupProductSample.Id);
                    //if (storeSetupProductSample != null)
                    //{
                    //    SqlConnection sqlconn = new SqlConnection("data source=.;initial catalog=Abundance_SN_2;integrated security=True;");
                    //    string commandString = "";
                    //    SqlCommand sqlcmd = new SqlCommand();
                    //    int noOfRowsAffected = 0;

                    //    string fileName = storeSetupProductSample.Description;
                    //    string filePathName = String.Format("~/Content/Junk/{0}", fileName);
                    //    string filePath = Server.MapPath(filePathName);
                    //string filePath = "~/Content/images/StoreLogo/";
                    //HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                    //string pathForSaving = Server.MapPath("~/Content/images/StoreLogo/");
                    //string savedFileName = Path.Combine(pathForSaving, file.FileName);

                    //if (System.IO.File.Exists(savedFileName))
                    //{
                    //    System.IO.File.Delete(savedFileName);
                    //}
                    //file.SaveAs(savedFileName);
                    //viewModel.StoreLogoUrl = filePath + file.FileName;
                    Role role = roleLogic.GetModelBy(s => s.Name == "Admin");
                    //if (!System.IO.File.Exists(filePath))
                    //{

                    if (role != null)
                    {
                        using (TransactionScope scope = new TransactionScope())
                        {

                            User user = new User()
                            {
                                Name = viewModel.Person.Email,
                                Email = viewModel.Person.Email,
                                Password = "1234567",
                                Role = role
                            };
                            var createdUser = userLogic.Create(user);
                            Person person = new Person()
                            {
                                Surname = viewModel.Person.Surname,
                                FirstName = viewModel.Person.FirstName,
                                Email = viewModel.Person.Email,
                                PhoneNumber = viewModel.Person.PhoneNumber,
                                Address = viewModel.Person.Address,
                                StateId = viewModel.Person.StateId,
                                DateRegistered = DateTime.UtcNow,
                                User = createdUser
                            };
                            var createdPerson = personLogic.Create(person);
                            if (createdPerson != null)
                            {
                                StoreTypeLogic storeTypeLogic = new StoreTypeLogic();
                                var storenature = storeTypeLogic.GetModelBy(x => x.Id == viewModel.StoreTypes.Id);
                                StoreSetup storeSetup = new StoreSetup()
                                {
                                    Person = createdPerson,
                                    ShopName = viewModel.ShopName,
                                    StoreProductType = storenature.Name.ToUpper(),
                                    BusinessEmail = viewModel.BusinessEmail,
                                    BusinessName = viewModel.BusinessName,
                                    BusinessPhone = viewModel.Person.PhoneNumber,
                                    BusinessRcNo = viewModel.BusinessRegistrationNo,
                                    StoreAddress=viewModel.StoreAddress,
                                    AboutUs=viewModel.AboutUs,
                                    StoreLogoUrl = viewModel.StoreLogoUrl


                                };
                                StoreSetupLogic storeSetupLogic = new StoreSetupLogic();
                                storeSetupLogic.Create(storeSetup);
                            }

                            scope.Complete();
                        }
                    }
                    SetMessage("You have successfully SetUp Your Store. Upload Banners", Message.Category.Information);
                    return RedirectToAction("CreateHomePageSlider");
                    //}

                    //commandString = String.Format("USE [master] ALTER DATABASE Abundance_SN_2 SET SINGLE_USER WITH ROLLBACK IMMEDIATE RESTORE DATABASE Abundance_SN_2 FROM DISK = '{0}' WITH REPLACE", filePath);
                    //sqlcmd = new SqlCommand(commandString, sqlconn);
                    //sqlconn.Open();
                    //noOfRowsAffected = sqlcmd.ExecuteNonQuery();
                    //sqlconn.Close();
                    //if (role != null && noOfRowsAffected == -1)
                    //{
                    //    User user = new User()
                    //    {
                    //        Name = "Admin",
                    //        Email = viewModel.Person.Email,
                    //        Password = viewModel.Password,
                    //        Role = role
                    //    };
                    //    var createdUser = userLogic.Create(user);
                    //    Person person = new Person()
                    //    {
                    //        Surname = viewModel.Person.Surname,
                    //        FirstName = viewModel.Person.FirstName,
                    //        Email = viewModel.PersonalEmail,
                    //        PhoneNumber = viewModel.Person.PhoneNumber,
                    //        Address = viewModel.Person.Address,
                    //        StateId = viewModel.Person.StateId,
                    //        DateRegistered = DateTime.UtcNow,
                    //        User = createdUser
                    //    };
                    //    var createdPerson = personLogic.Create(person);
                    //    if (createdPerson != null)
                    //    {
                    //        StoreSetup storeSetup = new StoreSetup()
                    //        {
                    //            Person = createdPerson,
                    //            ShopName = viewModel.ShopName,
                    //            StoreProductType = viewModel.ShopType,
                    //            BusinessEmail = viewModel.BusinessEmail,
                    //            BusinessName = viewModel.BusinessName,
                    //            BusinessPhone = viewModel.Person.PhoneNumber,
                    //            BusinessRcNo = viewModel.BusinessRegistrationNo
                    //        };
                    //        StoreSetupLogic storeSetupLogic = new StoreSetupLogic();
                    //        storeSetupLogic.Create(storeSetup);
                    //        FormsAuthentication.SetAuthCookie(user.Name, false);
                    //    }
                    //}

                }
            }
            catch (Exception)
            {

                throw;
            }
            return RedirectToAction("DashBoard", "Home", new { area = "" });
        }
        public ActionResult ViewSetUp()
        {
            RegisterViewModel viewModel = new RegisterViewModel();
            try
            {
                StoreSetupLogic storeSetupLogic = new StoreSetupLogic();
                viewModel.StoreSetups = storeSetupLogic.GetAll();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return View(viewModel);
        }
        public ActionResult EditStoreSetUp(string storeId)
        {
            ViewBag.StoreType = Utility.PopulateStoreTypeSelectListItem();
            RegisterViewModel viewModel = new RegisterViewModel();
            try
            {
                if (storeId == null)
                {
                    throw new ArgumentException("Store Not Found");
                }
                var id = Convert.ToInt64(storeId);
                StoreSetupLogic storeSetupLogic = new StoreSetupLogic();
                viewModel.StoreSetup = storeSetupLogic.GetModelBy(s => s.Store_Setup_Id == id);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult EditStoreSetUp(RegisterViewModel model)
        {
            try
            {
                foreach (string file in Request.Files)
                {

                    string filePath = "~/Content/NewImages/";
                    HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                    string pathForSaving = Server.MapPath("~/Content/NewImages/");
                    string savedFileName = Path.Combine(pathForSaving, hpf.FileName);
                    if (hpf.FileName != "")
                    {
                        if (System.IO.File.Exists(savedFileName))
                        {
                            System.IO.File.Delete(savedFileName);
                        }
                        hpf.SaveAs(savedFileName);
                        model.StoreSetup.StoreLogoUrl = filePath + hpf.FileName;
                    }

                }

                if (model.StoreSetup != null)
                {

                    StoreSetupLogic storeSetupLogic = new StoreSetupLogic();
                    StoreTypeLogic storeTypeLogic = new StoreTypeLogic();
                    var modifyStore = storeSetupLogic.GetModelBy(x => x.Store_Setup_Id == model.StoreSetup.Id);
                    if (model.StoreTypes.Id > 0)
                    {
                        var storenature = storeTypeLogic.GetModelBy(x => x.Id == model.StoreTypes.Id);
                        model.StoreSetup.StoreProductType = storenature.Name.ToUpper();
                    }
                    else
                    {
                        model.StoreSetup.StoreProductType = modifyStore.StoreProductType;
                    }
                    if (model.StoreSetup.StoreLogoUrl == null || model.StoreSetup.StoreLogoUrl == "")
                    {
                        model.StoreSetup.StoreLogoUrl = modifyStore.StoreLogoUrl;
                    }
                    var storeSetup = storeSetupLogic.Modify(model.StoreSetup);
                    if (storeSetup)
                    {
                        SetMessage("Operation Succesful", Message.Category.Information);
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
            return RedirectToAction("ViewSetUp", "Home", new { area = "Admin" });
        }
        public ActionResult CreateHomePageSlider()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateHomePageSlider(HomePageSlider model)
        {
            try
            {
                //FileStream fs = new FileStream(imagePath, FileMode.Create);
                foreach (string file in Request.Files)
                {

                    string filePath = "~/Content/images/Sliders/";
                    HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                    string pathForSaving = Server.MapPath("~/Content/images/Sliders/");
                    string savedFileName = Path.Combine(pathForSaving, hpf.FileName);

                    if (System.IO.File.Exists(savedFileName))
                    {
                        System.IO.File.Delete(savedFileName);
                    }
                    hpf.SaveAs(savedFileName);
                    model.ImageUrl = filePath + hpf.FileName;

                }
                HomePageSliderLogic homePageSliderLogic = new HomePageSliderLogic();
                HomePageSlider homePageSlider = homePageSliderLogic.Create(model);
                if (homePageSlider != null)
                {
                    SetMessage("Operation Successful", Message.Category.Information);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            ModelState.Clear();
            return View();
        }



        public ActionResult EditHomePageSlider(string sliderId)
        {
            ProductViewModel viewModel = new ProductViewModel();
            try
            {
                if (sliderId == null)
                {
                    throw new ArgumentException("Home Slider was not Found");
                }
                int id = Convert.ToInt32(sliderId);
                HomePageSliderLogic homePageSliderLogic = new HomePageSliderLogic();
                viewModel.HomePageSlider = homePageSliderLogic.GetModelBy(s => s.Home_Page_Slider_Id == id);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult EditHomePageSlider(ProductViewModel model)
        {
            try
            {
                foreach (string file in Request.Files)
                {

                    string filePath = "~/Content/images/Sliders/";
                    HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                    string pathForSaving = Server.MapPath("~/Content/images/Sliders/");
                    string savedFileName = Path.Combine(pathForSaving, hpf.FileName);
                    if (hpf.FileName != "")
                    {
                        if (System.IO.File.Exists(savedFileName))
                        {
                            System.IO.File.Delete(savedFileName);
                        }
                        hpf.SaveAs(savedFileName);
                        model.HomePageSlider.ImageUrl = filePath + hpf.FileName;
                    }

                }
                if (model.HomePageSlider != null)
                {
                    HomePageSliderLogic homePageSliderLogic = new HomePageSliderLogic();
                    var homePageSlider = homePageSliderLogic.Modify(model.HomePageSlider);
                    if (homePageSlider)
                    {
                        SetMessage("Operation Succesful", Message.Category.Information);
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
            return RedirectToAction("ViewEditHomePageSliders", "Home", new { area = "Admin" });
        }

        public ActionResult DeleteEditHomePageSlider(string sliderId)
        {
            try
            {
                int Id = Convert.ToInt32(sliderId);
                if (sliderId == null)
                {
                    throw new ArgumentException("Home Slider Was not found to be deleted");
                }
                HomePageSliderLogic homePageSliderLogic = new HomePageSliderLogic();
                var deletedbrand = homePageSliderLogic.Delete(s => s.Home_Page_Slider_Id == Id);
                if (deletedbrand)
                {
                    SetMessage("Operation Successful", Message.Category.Information);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return RedirectToAction("ViewEditHomePageSliders", "Home", new { area = "Admin" });
        }
        public ActionResult ViewEditHomePageSliders()
        {
            ProductViewModel viewModel = new ProductViewModel();
            try
            {
                HomePageSliderLogic homePageSliderLogic = new HomePageSliderLogic();
                viewModel.HomePageSliders = homePageSliderLogic.GetAll();
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
                SalesLogLogic salesLogLogic = new SalesLogLogic();
                ProductLogic productLogic = new ProductLogic();
                viewModel.GroupCountInt = new List<int>();
                int year = DateTime.Today.Year;


                for (int i = 1; i < 12; i++)
                {
                    int salescountByMonth = 0;
                    int monthId = i;
                    salescountByMonth = salesLogLogic.GetModelsBy(s => s.Date_Ordered.Value.Month == monthId && s.Date_Ordered.Value.Year == year).Count;
                    viewModel.GroupCountInt.Add(salescountByMonth);

                }

                List<Product> products = new List<Product>();
                products = productLogic.GetAll().OrderByDescending(s => s.Visits).Take(5).ToList();
                List<SalesLogs> salesLogses = salesLogLogic.GetAll();
                viewModel.Products = products;
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
        [Authorize(Roles = "SuperAdmin")]
        public ActionResult CreateUser()
        {
            ViewBag.Roles = Utility.PopulateRoleSelectListItems();
            return View();
        }
        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        public ActionResult CreateUser(RegisterViewModel viewModel)
        {
            try
            {
                if (viewModel != null)
                {
                    ViewBag.Roles = Utility.PopulateRoleSelectListItems();
                    UserLogic userLogic = new UserLogic();
                    RoleLogic roleLogic = new RoleLogic();
                    PersonLogic personLogic = new PersonLogic();
                    var existUser = userLogic.GetModelsBy(x => x.Email == viewModel.Person.Email).LastOrDefault();
                    if (existUser == null)
                    {

                        using (TransactionScope scope = new TransactionScope())
                        {
                            Role role = roleLogic.GetModelBy(s => s.Name == "Admin");
                            //Role role = roleLogic.GetModelBy(x => x.Id == viewModel.Roles.Id);
                            User user = new User()
                            {
                                Name = viewModel.Person.Surname,
                                Email = viewModel.Person.Email,
                                Password = "1234567",
                                Role = role
                            };
                            var createdUser = userLogic.Create(user);
                            Person person = new Person()
                            {
                                Surname = viewModel.Person.Surname,
                                FirstName = viewModel.Person.FirstName,
                                Email = viewModel.Person.Email,
                                PhoneNumber = viewModel.Person.PhoneNumber,
                                DateRegistered = DateTime.UtcNow,
                                User = createdUser
                            };
                            var createdPerson = personLogic.Create(person);
                            scope.Complete();
                            return RedirectToAction("Dashboard", "Home", new { area = "Admin" });
                        }
                    }
                    else
                    {
                        SetMessage("Email Already Exist", Message.Category.Error);
                    }

                }
                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        public ActionResult Notifications()
        {
            CustomerViewModel viewModel = new CustomerViewModel();
            ReorderNotificationLogic reorderNotificationLogic = new ReorderNotificationLogic();
            viewModel.ReorderNotifications = reorderNotificationLogic.GetModelsBy(x => x.Status == true).OrderByDescending(x => x.NotificationDate).ToList();
            return View(viewModel);
        }
        public ActionResult DeleteNotifications(string notificationId)
        {
            try
            {
                int Id = Convert.ToInt32(notificationId);
                if (notificationId == null)
                {
                    throw new ArgumentException("Notification Was not found to be deleted");
                }
                ReorderNotificationLogic reorderNotificationLogic = new ReorderNotificationLogic();
                var deletedNotification = reorderNotificationLogic.Delete(s => s.Id == Id);
                if (deletedNotification)
                {
                    SetMessage("Operation Successful", Message.Category.Information);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return RedirectToAction("Notifications", "Home", new { area = "Admin" });
        }
        [Authorize(Roles = "SuperAdmin")]
        public ActionResult SetupCourierService()
        {
            CourierViewModel viewmodel = new CourierViewModel();
            try
            {
                ViewBag.CourierServiceId = viewmodel.CourierServiceSelectListItem;
                ViewBag.StateId = viewmodel.StateSelectListItem;
                ViewBag.CourierAreaId = viewmodel.CourierAreaSelectListItem;
                ViewBag.CourierChargesId = viewmodel.CourierChargesSelectListItem;
            }
            catch (Exception ex)
            {
                SetMessage("Error Occurred! " + ex.Message, Message.Category.Error);
            }

            return View(viewmodel);

        }
        public JsonResult CreateRecord(int tableType, string myRecordArray)
        {
            JsonResultModel result = new JsonResultModel();
            try
            {
                if (tableType > 0 && myRecordArray != null)
                {

                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    CourierArrayModel arrayJsonView = serializer.Deserialize<CourierArrayModel>(myRecordArray);
                    switch (tableType)
                    {
                        case 1:
                            result.Message = CreateCourier(arrayJsonView.CourierName, arrayJsonView.CourierPhone, arrayJsonView.CourierEmail, arrayJsonView.CourierAddress);
                            result.IsError = IsError;
                            break;
                        //case 2:
                        //    result.Message = CreateRegion(arrayJsonView.CourierRegion, arrayJsonView.CourierServiceId);
                        //    result.IsError = IsError;
                        //    break;
                        case 2:
                            result.Message = CreateArea(arrayJsonView.CourierArea, arrayJsonView.StateId);
                            result.IsError = IsError;
                            break;
                        case 3:
                            result.Message = CreateCharges(Convert.ToDecimal(arrayJsonView.CourierCharges));
                            result.IsError = IsError;
                            break;


                    }
                }
            }
            catch (Exception ex)
            {
                result.IsError = true;
                result.Message = ex.Message;
            }
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        public JsonResult PopulateCourierSetupTables(int tableType)
        {
            JsonResultModel result = new JsonResultModel();

            List<CourierArrayModel> courierService = new List<CourierArrayModel>();
            List<CourierArrayModel> courierAreaCharge = new List<CourierArrayModel>();
            List<CourierArrayModel> courierCharges = new List<CourierArrayModel>();
            try
            {
                switch (tableType)
                {
                    case 1:
                        CourierServiceLogic courierServiceLogic = new CourierServiceLogic();
                        result.CourierService = courierServiceLogic.GetAll();
                        for (int i = 0; i < result.CourierService.Count; i++)
                        {
                            courierService.Add(new CourierArrayModel()
                            {
                                IsError = false,
                                CourierServiceId = result.CourierService[i].Courier_Id.ToString(),
                                CourierAddress = result.CourierService[i].Address,
                                CourierEmail = result.CourierService[i].Email,
                                CourierName = result.CourierService[i].Courier_Name,
                                CourierPhone = result.CourierService[i].Phone_Number
                            });
                        }
                        break;
                    //case 2:
                    //    CourierRegionLogic courierRegionLogic = new CourierRegionLogic();
                    //    result.CourierRegion = courierRegionLogic.GetAll();
                    //    for (int i = 0; i < result.CourierRegion.Count; i++)
                    //    {

                    //        courierRegion.Add(new CourierArrayModel()
                    //        {
                    //            IsError = false,
                    //            CourierRegionId = result.CourierRegion[i].Region_Id.ToString(),
                    //            CourierRegion = result.CourierRegion[i].Region_Name,
                    //            CourierName = result.CourierRegion[i].CourierService.Courier_Name

                    //        });

                    //    }
                    //    break;
                    case 2:
                        CourierAreaChargesLogic courierAreaChargesLogic = new CourierAreaChargesLogic();
                        result.CourierAreaCharges = courierAreaChargesLogic.GetAll();
                        for (int i = 0; i < result.CourierAreaCharges.Count; i++)
                        {
                            courierAreaCharge.Add(new CourierArrayModel()
                            {
                                IsError = false,
                                CourierAreaId = result.CourierAreaCharges[i].Id.ToString(),
                                CourierArea = result.CourierAreaCharges[i].Area,
                                state = result.CourierAreaCharges[i].State.StateName
                                //CourierName = result.CourierAreaCharges[i].CourierService.Courier_Name,
                                //CourierRegion = result.CourierAreaCharges[i].CourierRegion.Region_Name


                            });

                        }
                        break;
                    case 3:
                        CourierChargesLogic courierChargesLogic = new CourierChargesLogic();
                        result.CourierCharges = courierChargesLogic.GetAll();
                        for (int i = 0; i < result.CourierCharges.Count; i++)
                        {
                            courierCharges.Add(new CourierArrayModel()
                            {
                                IsError = false,
                                CourierChargeId = result.CourierCharges[i].Id.ToString(),
                                CourierCharges = result.CourierCharges[i].Charges.ToString()
                            });

                        }
                        break;

                }
                switch (tableType)
                {
                    case 1:
                        return Json(courierService, JsonRequestBehavior.AllowGet);
                        break;
                    //case 2:
                    //    return Json(courierRegion, JsonRequestBehavior.AllowGet);
                    //    break;
                    case 2:
                        return Json(courierAreaCharge, JsonRequestBehavior.AllowGet);
                        break;
                    case 3:
                        return Json(courierCharges, JsonRequestBehavior.AllowGet);
                        break;

                }
            }
            catch (Exception ex)
            {
                result.IsError = true;
                result.Message = ex.Message;

            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult EditRecord(int tableType, string recordId, string myRecordArray, string action)
        {
            JsonResultModel result = new JsonResultModel();
            try
            {
                if (action == "edit" && !String.IsNullOrEmpty(recordId))
                {
                    switch (tableType)
                    {
                        case 1:
                            int Id = Convert.ToInt32(recordId);
                            CourierServiceLogic courierServiceLogic = new CourierServiceLogic();
                            CourierService courierService = new CourierService();
                            CourierArrayModel courierArrayModel = new CourierArrayModel();

                            if (Id > 0)
                            {
                                courierService = courierServiceLogic.GetModelBy(x => x.Courier_Id == Id);
                                if (courierService != null)
                                {
                                    courierArrayModel.IsError = false;
                                    courierArrayModel.Id = courierService.Courier_Id.ToString();
                                    courierArrayModel.CourierPhone = courierService.Phone_Number;
                                    courierArrayModel.CourierName = courierService.Courier_Name;
                                    courierArrayModel.CourierEmail = courierService.Email;
                                    courierArrayModel.CourierAddress = courierService.Address;
                                    return Json(courierArrayModel, JsonRequestBehavior.AllowGet);
                                }

                                else
                                {
                                    courierArrayModel.IsError = true;
                                    courierArrayModel.Message = "Record does not exist in the database.";
                                    return Json(courierArrayModel, JsonRequestBehavior.AllowGet);
                                }
                            }

                            else
                            {
                                courierArrayModel.IsError = true;
                                courierArrayModel.Message = "Edit parameter was not set.";
                                return Json(courierArrayModel, JsonRequestBehavior.AllowGet);
                            }
                            break;
                        case 2:
                            int areaId = Convert.ToInt32(recordId);
                            CourierAreaChargesLogic courierAreaChargesLogic = new CourierAreaChargesLogic();
                            CourierAreaCharges courierAreaCharges = new CourierAreaCharges();
                            CourierArrayModel areaArrayModel = new CourierArrayModel();
                            if (areaId > 0)
                            {
                                courierAreaCharges = courierAreaChargesLogic.GetModelBy(x => x.Id == areaId);
                                if (courierAreaCharges != null)
                                {
                                    areaArrayModel.IsError = false;
                                    areaArrayModel.Id = courierAreaCharges.Id.ToString();
                                    areaArrayModel.CourierArea = courierAreaCharges.Area;
                                    return Json(areaArrayModel, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    areaArrayModel.IsError = true;
                                    areaArrayModel.Message = "Record does not exist in the database.";
                                    return Json(areaArrayModel, JsonRequestBehavior.AllowGet);
                                }
                            }
                            else
                            {
                                areaArrayModel.IsError = true;
                                areaArrayModel.Message = "Edit parameter was not set.";
                                return Json(areaArrayModel, JsonRequestBehavior.AllowGet);
                            }
                            break;
                        case 3:
                            int chargeId = Convert.ToInt32(recordId);
                            CourierChargesLogic courierChargesLogic = new CourierChargesLogic();
                            CourierCharges courierCharges = new CourierCharges();
                            CourierArrayModel chargesArrayModel = new CourierArrayModel();
                            if (chargeId > 0)
                            {
                                courierCharges = courierChargesLogic.GetModelBy(x => x.Id == chargeId);
                                if (courierCharges != null)
                                {
                                    chargesArrayModel.Id = courierCharges.Id.ToString();
                                    chargesArrayModel.CourierCharges = courierCharges.Charges.ToString();
                                    return Json(chargesArrayModel, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    chargesArrayModel.IsError = true;
                                    chargesArrayModel.Message = "Record does not exist in the database.";
                                    return Json(chargesArrayModel, JsonRequestBehavior.AllowGet);
                                }
                            }
                            else
                            {
                                chargesArrayModel.IsError = true;
                                chargesArrayModel.Message = "Edit parameter was not set.";
                                return Json(chargesArrayModel, JsonRequestBehavior.AllowGet);
                            }
                            break;
                    }

                }
                else if (action == "save")
                {
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    CourierArrayModel arrayJsonView = serializer.Deserialize<CourierArrayModel>(myRecordArray);
                    switch (tableType)
                    {
                        case 1:
                            int id = Convert.ToInt32(arrayJsonView.Id);
                            result.Message = SaveCourierService(arrayJsonView.Id, arrayJsonView.CourierName, arrayJsonView.CourierPhone, arrayJsonView.CourierEmail, arrayJsonView.CourierAddress);
                            result.IsError = IsError;
                            break;
                        case 2:
                            result.Message = SaveCourierArea(arrayJsonView.Id, arrayJsonView.CourierArea);
                            result.IsError = IsError;
                            break;
                        case 3:
                            result.Message = SaveCourierCharges(arrayJsonView.Id, arrayJsonView.CourierCharges);
                            result.IsError = IsError;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                result.IsError = true;
                result.Message = ex.Message;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteRecord(int tableType, string recordId)
        {
            JsonResultModel result = new JsonResultModel();
            try
            {
                if (tableType > 0 && !string.IsNullOrEmpty(recordId))
                {
                    switch (tableType)
                    {
                        case 1:
                            int id = Convert.ToInt32(recordId);
                            CourierServiceLogic courierServiceLogic = new CourierServiceLogic();
                            bool deleted = courierServiceLogic.Delete(x => x.Courier_Id == id);
                            if (deleted)
                            {
                                result.IsError = false;
                                result.Message = "Recored Successfully Deleted";
                            }
                            else
                            {
                                result.IsError = true;
                                result.Message = "Delete operation failed.";
                            }

                            break;
                        case 2:
                            int areaId = Convert.ToInt32(recordId);
                            CourierAreaChargesLogic courierAreaChargesLogic = new CourierAreaChargesLogic();
                            bool areadeleted = courierAreaChargesLogic.Delete(x => x.Id == areaId);
                            if (areadeleted)
                            {
                                result.IsError = false;
                                result.Message = "Recored Successfully Deleted";
                            }
                            else
                            {
                                result.IsError = true;
                                result.Message = "Delete operation failed.";
                            }

                            break;
                        case 3:
                            int chargesId = Convert.ToInt32(recordId);
                            CourierChargesLogic courierChargesLogic = new CourierChargesLogic();
                            bool chargesdeleted = courierChargesLogic.Delete(x => x.Id == chargesId);
                            if (chargesdeleted)
                            {
                                result.IsError = false;
                                result.Message = "Recored Successfully Deleted";
                            }
                            else
                            {
                                result.IsError = true;
                                result.Message = "Delete operation failed.";
                            }
                            break;
                    }
                }
                else
                {
                    result.IsError = true;
                    result.Message = "Parameter was not set.";
                }
            }
            catch (Exception ex)
            {
                result.IsError = true;
                result.Message = ex.Message;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "SuperAdmin")]
        public ActionResult ManageCourierService()
        {
            CourierViewModel viewModel = new CourierViewModel();
            try
            {
                ViewBag.CourierAreaId = viewModel.CourierAreaSelectListItem;
                ViewBag.StateId = viewModel.StateSelectListItem;
                ViewBag.CourierAreaId = viewModel.CourierAreaSelectListItem;
                ViewBag.CourierChargesId = viewModel.CourierChargesSelectListItem;
                ViewBag.CourierServiceId = viewModel.CourierServiceSelectListItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View(viewModel);
        }
        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public ActionResult ManageCourierService(CourierViewModel viewModel)
        {
            try
            {
                CourierDetail courierDetail = new CourierDetail();
                //CourierServiceLogic courierServiceLogic = new CourierServiceLogic();
                //CourierAreaChargesLogic courierAreaChargesLogic = new CourierAreaChargesLogic();
                //CourierAreaCharges courierAreaCharges = new CourierAreaCharges();
                //State state = new State();
                CourierDetailLogic courierDetailLogic = new CourierDetailLogic();
                viewModel.CourierDetails = new List<CourierDetail>();
                viewModel.CourierDetails=courierDetailLogic.GetModelsBy(x => x.Courier_Id == viewModel.CourierService.Courier_Id);
                courierDetail.CourierCharges = new CourierCharges();
                courierDetail.CourierAreaCharges = new CourierAreaCharges();
                courierDetail.State = new State();
                courierDetail.CourierService = viewModel.CourierService;
                
                int blankCount = 3;
                if (viewModel.CourierDetails != null && viewModel.CourierDetails.Count < 1 && viewModel.CourierDetails.Count < 1)
                {
                    blankCount = 8;
                }
                for (int i = 0; i < blankCount; i++)
                {
                    viewModel.CourierDetails.Add(courierDetail);
                }

                RetainDropDown(viewModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            ViewBag.CourierServiceId = viewModel.CourierServiceSelectListItem;
            ViewBag.Id = viewModel.CourierService.Courier_Id;

            return View(viewModel);
        }
        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public ActionResult SaveCourierChargeDetail(CourierViewModel viewModel)
        {
            try
            {
                
                if(viewModel.CourierDetails.Count>0)
                {
                    CourierDetailLogic courierDetailLogic = new CourierDetailLogic();

                    courierDetailLogic.Modify(viewModel.CourierDetails);
                }
            }
            catch (Exception ex)
            {
                SetMessage(ex.Message, Message.Category.Error);
            }
            SetMessage("Courier Charges were updated successfully", Message.Category.Information);
            return RedirectToAction("ManageCourierService");
        }
        [NonAction]
        public string CreateCourier(string courierName, string courierPhone, string courierEmail, string courierAddress)
        {
            string message = "";
            try
            {
                CourierServiceLogic courierServiceLogic = new CourierServiceLogic();
                CourierService courierService = new CourierService()
                {
                    Courier_Name = courierName,
                    Address = courierAddress,
                    Email = courierEmail,
                    Phone_Number = courierPhone

                };
                courierServiceLogic.Create(courierService);
                IsError = false;
                message = "Courier Service created successfully.";
            }
            catch (Exception ex)
            {
                IsError = true;
                message = ex.Message;
            }

            return message;
        }

        public string CreateArea(string area, string statecode)
        {
            string message = "";
            try
            {
                if (String.IsNullOrEmpty(statecode))
                {
                    message = "Ensure You Select The Required Information";
                }
                StateLogic stateLogic = new StateLogic();
                var myState = stateLogic.GetModelBy(x => x.State_Id == statecode);
                if (myState != null)
                {
                    CourierAreaChargesLogic courierAreaChargesLogic = new CourierAreaChargesLogic();
                    CourierAreaCharges courierAreaCharges = new CourierAreaCharges()
                    {
                        Area = area,
                        State = myState,

                    };
                    courierAreaChargesLogic.Create(courierAreaCharges);
                    IsError = false;
                    message = "Courier Area Added successfully.";
                }

            }
            catch (Exception ex)
            {
                IsError = true;
                message = ex.Message;
            }

            return message;
        }
        public string CreateCharges(decimal charges)
        {
            string message = "";
            try
            {
                CourierChargesLogic courierChargesLogic = new CourierChargesLogic();
                CourierCharges courierCharges = new CourierCharges()
                {
                    Charges = charges
                };
                courierChargesLogic.Create(courierCharges);
                IsError = false;
                message = "Courier Area Added successfully.";
            }
            catch (Exception ex)
            {
                IsError = true;
                message = ex.Message;
            }

            return message;
        }
        public string SaveCourierService(string courierId, string couriername, string phone, string email, string address)
        {

            string message = "";
            try
            {
                if (!String.IsNullOrEmpty(courierId) && !String.IsNullOrEmpty(address) && !String.IsNullOrEmpty(couriername) && !String.IsNullOrEmpty(email) && !String.IsNullOrEmpty(phone))
                {
                    int Id = Convert.ToInt32(courierId);
                    CourierServiceLogic courierServiceLogic = new CourierServiceLogic();


                    CourierService courierService = courierServiceLogic.GetModelBy(x => x.Courier_Id == Id);
                    if (courierService != null)
                    {
                        courierService = new CourierService();
                        courierService.Courier_Id = Id;
                        courierService.Address = address;
                        courierService.Courier_Name = couriername;
                        courierService.Email = email;
                        courierService.Phone_Number = phone;
                        courierServiceLogic.Modify(courierService);
                        message = "Courier Service was modified Successfully";
                        IsError = false;
                    }
                    else
                    {
                        message = "Record not available for edit";
                        IsError = true;
                    }
                }
                else
                {
                    IsError = true;
                    message = "One or more of the parameters required was not set.";
                }

            }
            catch (Exception ex)
            {
                message = ex.Message;
                IsError = true;
            }
            return message;
        }

        public string SaveCourierArea(string areaId, string courierArea)
        {

            string message = "";
            try
            {
                if (!String.IsNullOrEmpty(areaId) && !String.IsNullOrEmpty(courierArea))
                {
                    int Id = Convert.ToInt32(areaId);
                    CourierAreaChargesLogic courierAreaChargesLogic = new CourierAreaChargesLogic();
                    CourierAreaCharges courierAreaCharges = courierAreaChargesLogic.GetModelBy(x => x.Id == Id);
                    if (courierAreaCharges != null)
                    {
                        courierAreaCharges.Area = courierArea;
                        courierAreaChargesLogic.Modify(courierAreaCharges);
                        message = "Courier Service was modified Successfully";
                        IsError = false;
                    }
                    else
                    {
                        message = "Record not available for edit";
                        IsError = true;
                    }
                }
                else
                {
                    IsError = true;
                    message = "One or more of the parameters required was not set.";
                }

            }
            catch (Exception ex)
            {
                message = ex.Message;
                IsError = true;
            }
            return message;
        }

        public string SaveCourierCharges(string chargeId, string charge)
        {

            string message = "";
            try
            {
                if (!String.IsNullOrEmpty(chargeId) && !String.IsNullOrEmpty(charge))
                {
                    int Id = Convert.ToInt32(chargeId);
                    decimal charges = Convert.ToDecimal(charge);
                    CourierChargesLogic courierChargesLogic = new CourierChargesLogic();
                    CourierCharges courierCharges = courierChargesLogic.GetModelBy(x => x.Id == Id);
                    if (courierCharges != null)
                    {
                        courierCharges.Charges = charges;
                        courierChargesLogic.Modify(courierCharges);
                        message = "Courier Charge was modified Successfully";
                        IsError = false;
                    }
                    else
                    {
                        message = "Record not available for edit";
                        IsError = true;
                    }
                }
                else
                {
                    IsError = true;
                    message = "One or more of the parameters required was not set.";
                }

            }
            catch (Exception ex)
            {
                message = ex.Message;
                IsError = true;
            }
            return message;
        }
        public void RetainDropDown(CourierViewModel viewModel)
        {
            
            try
            {
                //ViewBag.StateId = new SelectList(new List<State>(), ID, NAME);

                //if (vModel.feeDetail.Department != null && vModel.feeDetail.Department.Id > 0)
                //{
                //    ViewBag.Departments = new SelectList(vModel.DepartmentSelectListItem, Utility.VALUE, Utility.TEXT,
                //        vModel.feeDetail.Department.Id);
                //}
                //else
                //{
                //    ViewBag.Departments = viewmodel.DepartmentSelectListItem;
                //}

                //if (vModel.feeDetail.Programme != null && vModel.feeDetail.Programme.Id > 0)
                //{
                //    ViewBag.Programmes = new SelectList(vModel.ProgrammeSelectListItem, Utility.VALUE, Utility.TEXT,
                //        vModel.feeDetail.Programme.Id);
                //}
                //else
                //{
                //    ViewBag.Programmes = vModel.ProgrammeSelectListItem;
                //}
                if (viewModel.CourierDetails != null)
                {
                    for (int i = 0; i < viewModel.CourierDetails.Count; i++)
                    {
                        if (viewModel.CourierDetails[i].State != null && viewModel.CourierDetails[i].State.StateId!="")
                        {
                            ViewData["StateId" + i] = new SelectList(viewModel.StateSelectListItem, VALUE, TEXT,
                                viewModel.CourierDetails[i].State.StateId);
                        }
                        else
                        {
                            ViewData["StateId" + i] = new SelectList(viewModel.StateSelectListItem, VALUE, TEXT, 0);
                        }
                        if (viewModel.CourierDetails[i].CourierAreaCharges != null && Convert.ToInt32(viewModel.CourierDetails[i].CourierAreaCharges.Id) > 0)
                        {
                            ViewData["AreaId" + i] = new SelectList(viewModel.CourierAreaSelectListItem,
                                VALUE, TEXT, viewModel.CourierDetails[i].CourierAreaCharges.Id);
                        }
                        else
                        {
                            ViewData["AreaId" + i] = new SelectList(viewModel.CourierAreaSelectListItem,
                                VALUE, TEXT, 0);
                        }
                        if (viewModel.CourierDetails[i].CourierAreaCharges != null && Convert.ToInt32(viewModel.CourierDetails[i].CourierAreaCharges.Id) > 0)
                        {
                            ViewData["ChargeId" + i] = new SelectList(viewModel.CourierChargesSelectListItem, VALUE, TEXT,
                                viewModel.CourierDetails[i].CourierCharges.Id);
                        }
                        else
                        {
                            ViewData["ChargeId" + i] = new SelectList(viewModel.CourierChargesSelectListItem, VALUE, TEXT, 0);
                        }
                        //if (viewModel.CourierDetails[i].CourierService != null && Convert.ToInt32(viewModel.CourierDetails[i].CourierService.Courier_Id) > 0)
                        //{
                        //    ViewData["ServiceId" + i] = new SelectList(viewModel.CourierServiceSelectListItem, VALUE, TEXT,
                        //        viewModel.CourierDetails[i].CourierService.Courier_Id);
                        //}
                        //else
                        //{
                        //    ViewData["ServiceId" + i] = new SelectList(viewModel.CourierServiceSelectListItem, VALUE, TEXT, 0);
                        //}
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
    }
}

            