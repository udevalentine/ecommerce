using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;
using Abundance_SN.Business;
using Abundance_SN.Controllers;
using Abundance_SN.Model.Model;
using Abundance_SN.Models;

using Unit = Abundance_SN.Model.Model.Unit;
using System.Text.RegularExpressions;

namespace Abundance_SN.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class ProductController : BaseController
    {
        private const string TypeId = "Id";
        private const string TypeName = "Name";
        private const string Category_Id = "CateId";
        private const string Category_Name = "CateName";

        // GET: Admin/Product
        ProductLogic productLogic = new ProductLogic();
        ProductTypeLogic productTypeLogic = new ProductTypeLogic();
        CategoryLogic categoryLogic = new CategoryLogic();
        public ActionResult Index()
        {
            ProductViewModel viewModel = new ProductViewModel();
            try
            {

                productLogic = new ProductLogic();

                viewModel.Products = productLogic.GetAll();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return View(viewModel);
        }
        public ActionResult Create()
        {
            ProductViewModel viewModel = new ProductViewModel();
            try
            {
                PopulateDropdown(viewModel);
            }
            catch (Exception)
            {

                throw;
            }
            return View(viewModel);
        }

        [HttpPost]
        //public JsonResult CreateProduct(string productItems, string image, string productVariantArray, string productDescription)
        public JsonResult CreateProduct(string productItems, string image, string productVariantArray, string productDescription)
        {
            JsonResultModel result = new JsonResultModel();
            try
            {
                if (!string.IsNullOrEmpty(productItems) && !string.IsNullOrEmpty(image) &&
                    !string.IsNullOrEmpty(productVariantArray))
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        JavaScriptSerializer productModelserializer = new JavaScriptSerializer();
                        ProductModel productModel = productModelserializer.Deserialize<ProductModel>(productItems);

                        List<ProductModel> imageList = productModelserializer.Deserialize<List<ProductModel>>(image);
                        List<ProductModel> productVariantArrayList = productModelserializer.Deserialize<List<ProductModel>>(productVariantArray);

                        productLogic = new ProductLogic();
                        Product product = new Product();
                        product.ProductType = new ProductType()
                        {
                            Id = Convert.ToInt32(productModel.productTypeId)
                        };
                        product.Active = Convert.ToBoolean(productModel.activated);
                        product.Brand = new Brand()
                        {
                            Id = Convert.ToInt32(productModel.productBrand)
                        };
                        product.CanBedelivered = Convert.ToBoolean(productModel.canBeDelivered);
                         var productAdditionalInfoDecode= HttpUtility.UrlDecode(productModel.productAdditionalInfomation);
                        string productAdditionalInfo = Regex.Replace(productAdditionalInfoDecode, "<.*?>", String.Empty);
                        product.AdditionalInformation = productAdditionalInfo.ToUpper();
                        product.Description = productModel.productDescription.ToUpper();
                        product.Price = Convert.ToDecimal(productModel.productPrice);
                        product.MaxPrice = Convert.ToDecimal(productModel.MaxPrice);
                        if(productModel.productDiscount !=null && productModel.productDiscount!="")
                        {
                            product.Discount = Convert.ToInt32(productModel.productDiscount);
                        }
                        if(productModel.productWeight !=null && productModel.productWeight!="")
                        {
                            product.Weight = Convert.ToInt32(productModel.productWeight);
                        }

                        product.UnitMeasurement = new Unit()
                        {
                            Id = Convert.ToInt32(productModel.unitOfMeasurement)
                        };
                        product.ReorderLevel = Convert.ToInt32(productModel.reOrderLevel);
                        product.Shipping = Convert.ToBoolean(productModel.shipping);
                        //product.Sku = productModel.productSku;
                        product.Sku = "2";
                        product.Active = Convert.ToBoolean(productModel.activated);
                        product.ImageUrl = imageList[0].filePath;
                        product.Name = productModel.productName.ToUpper();

                        product = productLogic.Create(product);
                        if (product.Id > 0)
                        {
                            ProductVariant productVariant = new ProductVariant();
                            ProductVariantLogic productVariantLogic = new ProductVariantLogic();
                            productVariant.Product = product;
                            productVariant.Key1 = productModel.productVariantKey1;
                            productVariant.Key2 = productModel.productVariantKey2;
                            productVariant.Key3 = productModel.productVariantKey3;
                            productVariant.Value1 = productModel.productVariantValue1;
                            productVariant.Value2 = productModel.productVariantValue2;
                            productVariant.Value3 = productModel.productVariantValue3;
                            productVariant = productVariantLogic.Create(productVariant);

                            if (productVariant.Id > 0)
                            {
                                ProductVariantOptionsLogic productVariantOptionsLogic = new ProductVariantOptionsLogic();
                                InventoryLogic inventoryLogic = new InventoryLogic();
                                for (int i = 0; i < productVariantArrayList.Count; i++)
                                {
                                    if (Convert.ToBoolean(productVariantArrayList[i].select))
                                    {
                                        ProductVariantOptions productVariantOptions = new ProductVariantOptions();
                                        Inventory inventory = new Inventory();
                                        productVariantOptions.ProductOptionName = productVariantArrayList[i].Name.Trim('\n');
                                        productVariantOptions.ProductVariant = productVariant;
                                        productVariantOptions.Price = Convert.ToDecimal(productVariantArrayList[i].Price);
                                        inventory.Product = product;
                                        inventory.Quantity = Convert.ToInt32(productVariantArrayList[i].Quantity);
                                        productVariantOptions = productVariantOptionsLogic.Create(productVariantOptions);
                                        inventory.ProductVariantOptions = productVariantOptions;
                                        if (inventory.Quantity > 0)
                                        {
                                            inventoryLogic.Create(inventory);
                                        }
                                    }

                                }
                                ProductImageGallaryLogic productImageGallaryLogic = new ProductImageGallaryLogic();
                                List<ProductImageGallary> productImageGallaries = new List<ProductImageGallary>();
                                for (int i = 0; i < imageList.Count; i++)
                                {

                                    ProductImageGallary productImageGallary = new ProductImageGallary();
                                    productImageGallary.Product = product;
                                    productImageGallary.ImageUrl = imageList[i].filePath;
                                    productImageGallaries.Add(productImageGallary);

                                }
                                if (productImageGallaries.Count > 0)
                                {
                                    productImageGallaryLogic.Create(productImageGallaries);
                                }

                            }

                        }

                        scope.Complete();
                        //GIVE JSON MODAL REPORT
                        result.IsError = false;
                        result.Message = "Operation Successful!";
                    }

                }
                else
                {
                    return Json("Error, No data received", JsonRequestBehavior.AllowGet);

                }
            }
            catch (Exception ex)
            {
                result.IsError = true;
                result.Message = ex.Message;
                return Json(result, JsonRequestBehavior.AllowGet);
                //return Json("Error" + ex.Message, JsonRequestBehavior.AllowGet);
            }

            //return Json("success", JsonRequestBehavior.AllowGet);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(string ProNo)
        {
            int id = Convert.ToInt32(ProNo);
            ProductViewModel viewModel = new ProductViewModel();
            try
            {
                if (ProNo == null)
                {
                    throw new ArgumentException("Product was not Found");
                }
                productLogic = new ProductLogic();
                ProductVariantLogic productVariantLogic = new ProductVariantLogic();
                ProductVariantOptionsLogic productOptionsLogic = new ProductVariantOptionsLogic();
                InventoryLogic inventoryLogic = new InventoryLogic();

                viewModel.Product = productLogic.GetModelBy(v => v.Id == id);
                viewModel.ProductVariant = productVariantLogic.GetModelBy(v => v.Product_Id == viewModel.Product.Id);
                
if (viewModel.ProductVariant != null)
                {
                    viewModel.ProductVariantOptionses = productOptionsLogic.GetModelsBy(p => p.Product_Variant_Id == viewModel.ProductVariant.Id);
                    viewModel.ProductVariantOptionses.ForEach(p =>
                    {
                        var variantInventory = inventoryLogic.GetModelBy(x => x.Product_Variant_Option_Id == p.Id);
                        p.VariantQuantity = variantInventory != null ? variantInventory.Quantity : 0;
                    });
                }
                //long productOtionId = Convert.ToInt64(viewModel.ProductVariantOptionses.FirstOrDefault().Id);
                //if(viewModel.ProductVariantOptionses !=null)
                //{
                //    viewModel.Inventorieses = inventoryLogic.GetModelsBy(x => x.Product_Id == viewModel.Product.Id);
                //}

            }
            catch (Exception ex)
            {
                throw ex;
            }
            PopulateDropdown(viewModel);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(ProductViewModel model)
        {
            try
            {

                if (model.Product != null)
                {

                    productLogic = new ProductLogic();
                    ProductVariantLogic productVariant = new ProductVariantLogic();
                    ProductVariantOptionsLogic productVariantOptionsLogic = new ProductVariantOptionsLogic();
                    InventoryLogic inventoryLogic = new InventoryLogic();
                    Inventory inventory = new Inventory();
                    var editedVariantOption = false;
                    var editedVariant = false;
                    var editedInventory = false;
                    var editedProduct = false;
                    List<decimal?> priceList = new List<decimal?>();
                    HttpPostedFileBase file1 = Request.Files["variantImage-46"];
                    foreach (string file in Request.Files)
                    {
                        string filePath = "~/Content/NewImages/";
                        HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                        string pathForSaving = Server.MapPath("~/Content/NewImages/");
                        string savedFileName = Path.Combine(pathForSaving, hpf.FileName);

                        if (!string.IsNullOrEmpty(hpf.FileName) && !file.Contains('-'))
                        {
                            if (System.IO.File.Exists(savedFileName))
                            {
                                System.IO.File.Delete(savedFileName);
                            }
                            hpf.SaveAs(savedFileName);
                            model.Product.ImageUrl = filePath + hpf.FileName;
                        }
                    }
                    //assigning the sum of the product variant quantity to the reoderlevel in the product table
                    //model.Product.ReorderLevel = model.Inventorieses.Sum(x => x.Quantity);
                    //To update product image gallary when the option image is updated
                    List<string> gallaryImageUrl = new List<string>();
                    ProductImageGallaryLogic productImageGallaryLogic = new ProductImageGallaryLogic();
                    model.ProductImageGallaries= productImageGallaryLogic.GetModelsBy(x => x.Product_Id == model.Product.Id);
                    using (var transaction = new TransactionScope())
                    {
                        if (model.ProductVariant != null)
                        {
                            editedVariant = productVariant.Modify(model.ProductVariant);
                            for (int i = 0; i < model.ProductVariantOptionses.Count; i++)
                            {
                                string filePath = "~/Content/NewImages/";
                                string requestFileName = "file-" + model.ProductVariantOptionses[i].Id;
                                HttpPostedFileBase hpf = Request.Files[requestFileName] as HttpPostedFileBase;
                                string pathForSaving = Server.MapPath("~/Content/NewImages/");
                                string savedFileName = Path.Combine(pathForSaving, hpf.FileName);

                                if (!string.IsNullOrEmpty(hpf.FileName))
                                {
                                    if (System.IO.File.Exists(savedFileName))
                                    {
                                        System.IO.File.Delete(savedFileName);
                                    }
                                    hpf.SaveAs(savedFileName);
                                    model.ProductVariantOptionses[i].ImageUrl = filePath + hpf.FileName;
                                    model.ProductVariantOptionses[i].ProductVariant = model.ProductVariant;
                                    gallaryImageUrl.Add(filePath + hpf.FileName);

                                }
                                long variantOptionId = model.ProductVariantOptionses[i].Id;
                                inventory = inventoryLogic.GetModelBy(x => x.Product_Variant_Option_Id == variantOptionId);
                                if (inventory == null)
                                {
                                    Inventory createInventory = new Inventory();
                                    createInventory.Product = new Product();
                                    createInventory.ProductVariantOptions = new ProductVariantOptions();
                                    createInventory.Product.Id = model.Product.Id;
                                    createInventory.ProductVariantOptions.Id = variantOptionId;
                                    createInventory.Quantity = model.ProductVariantOptionses[i].VariantQuantity;
                                    inventoryLogic.Create(createInventory);
                                }
                                else
                                {
                                    inventory.Quantity = model.ProductVariantOptionses[i].VariantQuantity;
                                    editedInventory = inventoryLogic.Modify(inventory);
                                }
                               
                                //Get the variant prices in an array
                                var variantOptionPrice = model.ProductVariantOptionses[i].Price;
                                priceList.Add(variantOptionPrice);
                            }

                            var productVariantOptions = model.ProductVariantOptionses.FirstOrDefault();
                            if (productVariantOptions != null)
                                productVariantOptions.ProductVariant = model.ProductVariant;
                            editedVariantOption = productVariantOptionsLogic.Modify(model.ProductVariantOptionses);
                        }
                        if(model.ProductImageGallaries.Count>0 && gallaryImageUrl.Count>0)
                        {
                            if(model.ProductImageGallaries.Count< gallaryImageUrl.Count)
                            {
                                var newGallaryRow = Convert.ToInt32(gallaryImageUrl.Count() - model.ProductImageGallaries.Count());
                                for(int i=0; i<newGallaryRow; i++)
                                {
                                    model.ProductImageGallaries[i].Product.Id = model.Product.Id;
                                    model.ProductImageGallaries[i].ImageUrl = "NULL";
                                    productImageGallaryLogic.Create(model.ProductImageGallaries);
                                }
                                
                            }
                            model.ProductImageGallaries = productImageGallaryLogic.GetModelsBy(x => x.Product_Id == model.Product.Id);
                            for (int j=0; j<model.ProductImageGallaries.Count; j++)
                            {
                                if (j == gallaryImageUrl.Count())
                                {
                                    break;
                                }
                                model.ProductImageGallaries[j].ImageUrl = gallaryImageUrl[j];
                                productImageGallaryLogic.Modify(model.ProductImageGallaries);

                            }
                            model.Product.ImageUrl = gallaryImageUrl[0];
                        }
                        if (editedVariantOption)
                        {
                            //Ensure that the smallest price of the variant is assigned to the price and the highest to the maxPrice in the product table
                            priceList = priceList.OrderBy(x => x).ToList();
                            model.Product.Price = priceList[0].Value;
                            model.Product.MaxPrice = priceList[priceList.Count - 1].Value;
                            //to remove the html tags 
                            if (model.Product.AdditionalInformation != null)
                            {
                                string productAdditionalInfo = Regex.Replace(model.Product.AdditionalInformation, "<.*?>", String.Empty);
                                model.Product.AdditionalInformation = productAdditionalInfo;
                            }
                                
                            editedProduct = productLogic.Modify(model.Product);

                        }
                        else
                        {
                            //to remove the html tags 
                            if(model.Product.AdditionalInformation!=null)
                            {
                                string productAdditionalInfo = Regex.Replace(model.Product.AdditionalInformation, "<.*?>", String.Empty);
                                //string productDescription = Regex.Replace(model.Product.Description, "<.*?>", String.Empty);
                                model.Product.AdditionalInformation = productAdditionalInfo;
                            }
                            
                            editedProduct = productLogic.Modify(model.Product);
                        }
                        transaction.Complete();
                    }
                    
                    if (editedProduct || editedVariantOption|| editedInventory)
                    {
                        SetMessage("Operation Succesful", Message.Category.Information);
                    }
                    else
                    {
                        SetMessage("Error Occured! No Item found to be modified", Message.Category.Error);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index", "Product", new { area = "Admin" });
        }

        public ActionResult Delete(string ProNo)
        {
            int id = Convert.ToInt32(ProNo);

            try
            {
                if (ProNo == null)
                {
                    throw new ArgumentException("Product was not Found");
                }
                productLogic = new ProductLogic();
                Product product = productLogic.GetModelBy(v => v.Id == id);
                var editedProduct = productLogic.Delete(s => s.Id == product.Id);
                if (editedProduct)
                {
                    SetMessage("Operation Succesful", Message.Category.Information);
                }


            }
            catch (DbEntityValidationException db)
            {
                foreach (var error in db.EntityValidationErrors)
                {

                }
                throw;
            }
            return RedirectToAction("Index", "Product", new { area = "Admin" });
        }

        public ActionResult ViewProductOrderLog()
        {
            ProductViewModel viewModel = new ProductViewModel();
            try
            {

                ProductOrderLogLogic productOrderLogLogic = new ProductOrderLogLogic();

                viewModel.ProductOrderLogses = productOrderLogLogic.GetAll();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return View(viewModel);
        }
        public ActionResult CreateProductOrder()
        {
            ProductViewModel viewModel = new ProductViewModel();
            try
            {
                PopulateDropdown(viewModel);
            }
            catch (Exception)
            {

                throw;
            }
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult CreateProductOrder(ProductViewModel viewModel)
        {
            try
            {

                ProductOrderLogLogic productOrderLogLogic = new ProductOrderLogLogic();

                viewModel.ProductOrderLogs.User = new User()
                {
                    Id = 4
                };
                //string date = DateTime.UtcNow.Date.ToString("yy-MM-dd");
                viewModel.ProductOrderLogs.DateRequested = DateTime.UtcNow.Date;
                viewModel.ProductOrderLogs.CreateAt = DateTime.UtcNow.Date;
                viewModel.ProductOrderLogs.UpdatedAt = DateTime.UtcNow.Date;
                ProductOrderLogs createorder = productOrderLogLogic.Create(viewModel.ProductOrderLogs);
                if (createorder != null)
                {
                    SetMessage("Operation Successful", Message.Category.Information);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            ModelState.Clear();
            PopulateDropdown(viewModel);
            return View();
        }
        public ActionResult EditProductOrder(string ProNo)
        {
            int id = Convert.ToInt32(ProNo);
            ProductViewModel viewModel = new ProductViewModel();
            try
            {
                if (ProNo == null)
                {
                    throw new ArgumentException("Product was not Found");
                }
                ProductOrderLogLogic productOrderLogic = new ProductOrderLogLogic();
                viewModel.ProductOrderLogs = productOrderLogic.GetModelBy(v => v.Id == id);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            PopulateDropdown(viewModel);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult EditProductOrder(ProductViewModel model)
        {
            try
            {
                if (model.ProductOrderLogs != null)
                {

                    ProductOrderLogLogic productOrderLogLogic = new ProductOrderLogLogic();
                    var editedProduct = productOrderLogLogic.Modify(model.ProductOrderLogs);
                    if (editedProduct)
                    {
                        SetMessage("Operation Succesful", Message.Category.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("ViewProductOrderLog", "Product", new { area = "Admin" });
        }

        public ActionResult DeleteProductOrder(string ProNo)
        {
            int id = Convert.ToInt32(ProNo);

            try
            {
                if (ProNo == null)
                {
                    throw new ArgumentException("Product was not Found");
                }

                ProductOrderLogLogic productOrderLogLogic = new ProductOrderLogLogic();
                ProductSupplyLogsLogic productSupplyLogsLogic = new ProductSupplyLogsLogic();
                ProductOrderLogs productOrderLogs = productOrderLogLogic.GetModelBy(v => v.Id == id);
                ProductSupplyLogs productSupplyLogs = productSupplyLogsLogic.GetModelBy(ps => ps.Product_Id == productOrderLogs.Id);

                if (productSupplyLogs == null)
                {
                    var editedProduct = productLogic.Delete(s => s.Id == productOrderLogs.Id);
                    if (editedProduct)
                    {
                        SetMessage("Operation Succesful", Message.Category.Information);
                    }
                }
                else
                {
                    SetMessage("Error Product Order Cannot be deleted because Supply already Exist", Message.Category.Information);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("ViewProductOrderLog", "Product", new { area = "Admin" });
        }


        public ActionResult ViewProductSupplyLog()
        {
            ProductViewModel viewModel = new ProductViewModel();
            try
            {

                ProductSupplyLogsLogic productSupplyLogsLogic = new ProductSupplyLogsLogic();

                viewModel.ProductSupplyLogses = productSupplyLogsLogic.GetAll();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return View(viewModel);
        }
        public ActionResult CreateProductSupplyOrder()
        {
            ProductViewModel viewModel = new ProductViewModel();
            try
            {
                PopulateDropdown(viewModel);
            }
            catch (Exception)
            {

                throw;
            }
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult CreateProductSupplyOrder(ProductViewModel viewModel)
        {
            try
            {
                ProductOrderLogLogic productOrderLogLogic = new ProductOrderLogLogic();
                ProductOrderLogs productOrderLogs = productOrderLogLogic.GetModelsBy(s => s.Product_Id == viewModel.ProductSupplyLogs.Product.Id && s.Vendor_Id == viewModel.ProductSupplyLogs.Vendors.Id && s.Date_Requested == viewModel.ProductSupplyLogs.ProductOrderLogs.DateRequested).LastOrDefault();

                if (productOrderLogs == null)
                {
                    SetMessage("No Product order Found", Message.Category.Error);
                }
                else
                {
                    if (viewModel.ProductSupplyLogs.Quantity != productOrderLogs.Quantity)
                    {
                        SetMessage("Please View check the Product Order again and supply the quantity specified", Message.Category.Error);
                    }
                    else
                    {
                        UserLogic userLogic = new UserLogic();
                        User user = userLogic.GetModelBy(s => s.Name == User.Identity.Name);
                        ProductSupplyLogsLogic productSupplyLogsLogic = new ProductSupplyLogsLogic();
                        InventoryLogic inventoriesLogic = new InventoryLogic();

                        viewModel.ProductSupplyLogs.User = user;
                        viewModel.ProductSupplyLogs.SupplyDate = DateTime.UtcNow.Date;
                        viewModel.ProductSupplyLogs.CreatedAt = DateTime.UtcNow.Date;
                        viewModel.ProductSupplyLogs.UpdateAt = DateTime.UtcNow.Date;
                        viewModel.ProductSupplyLogs.ProductOrderLogs = new ProductOrderLogs()
                        {
                            Id = productOrderLogs.Id
                        };
                        ProductSupplyLogs createorder = productSupplyLogsLogic.Create(viewModel.ProductSupplyLogs);
                        if (createorder != null)
                        {
                            Inventory inventories = new Inventory()
                            {
                                Product = productOrderLogs.Product,
                                Quantity = viewModel.ProductSupplyLogs.Quantity,


                            };
                            var createdInventory = inventoriesLogic.Create(inventories);
                            SetMessage("Operation Successful", Message.Category.Information);
                        }
                    }


                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            ModelState.Clear();
            PopulateDropdown(viewModel);
            return View();
        }
        public ActionResult EditProductSupplyOrder(string ProNo)
        {
            int id = Convert.ToInt32(ProNo);
            ProductViewModel viewModel = new ProductViewModel();
            try
            {
                if (ProNo == null)
                {
                    throw new ArgumentException("Product was not Found");
                }
                ProductSupplyLogsLogic productSupplyLogsLogic = new ProductSupplyLogsLogic();
                viewModel.ProductSupplyLogs = productSupplyLogsLogic.GetModelBy(v => v.Id == id);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            PopulateDropdown(viewModel);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult EditProductSupplyOrder(ProductViewModel model)
        {
            try
            {
                if (model.ProductOrderLogs != null)
                {

                    ProductSupplyLogsLogic productSupplyLogsLogic = new ProductSupplyLogsLogic();
                    var editedProduct = productSupplyLogsLogic.Modify(model.ProductSupplyLogs);
                    if (editedProduct)
                    {
                        SetMessage("Operation Succesful", Message.Category.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("ViewProductOrderLog", "Product", new { area = "Admin" });
        }

        public ActionResult DeleteProductSupplyOrder(string ProNo)
        {
            int id = Convert.ToInt32(ProNo);

            try
            {
                if (ProNo == null)
                {
                    throw new ArgumentException("Product was not Found");
                }

                ProductSupplyLogsLogic productSupplyLogsLogic = new ProductSupplyLogsLogic();
                ProductSupplyLogs productSupplyLogs = productSupplyLogsLogic.GetModelBy(ps => ps.Product_Id == id);

                if (productSupplyLogs != null)
                {
                    var editedProduct = productLogic.Delete(s => s.Id == id);
                    if (editedProduct)
                    {
                        SetMessage("Operation Succesful", Message.Category.Information);
                    }
                }
                else
                {
                    SetMessage("Error Product Order Cannot be deleted because Supply already Exist", Message.Category.Information);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("ViewProductOrderLog", "Product", new { area = "Admin" });
        }
        public void PopulateDropdown(ProductViewModel viewModel)
        {
            try
            {
                if (viewModel.Product == null)
                {
                    ViewBag.BrandsId = Utility.PopulateBrandsSelectListItem();
                    ViewBag.ProductTypeId = new SelectList(new List<ProductType>(), TypeId, TypeName);
                    //ViewBag.ProductTypeId = Utility.PopulateProductTypeselectListItem();
                    ViewBag.UnitMeasurementId = Utility.PopulateUnitOfMeasurementSelectListItem();
                    ViewBag.ProductId = Utility.PopulateProductselectListItem();
                    ViewBag.VendorId = Utility.PopulateVendorselectListItem();
                    ViewBag.Category = Utility.PopulateCategorySelectListItem();
                    ViewBag.SupplyOrderId = new SelectList(Utility.VALUE, Utility.TEXT);
                }
                else
                {
                    if (viewModel.Product.Brand != null && viewModel.Product.Brand.Id > 0)
                    {
                        ViewBag.BrandsId = new SelectList(Utility.PopulateBrandsSelectListItem(), Utility.VALUE, Utility.TEXT, viewModel.Product.Brand.Id);
                    }
                    else
                    {
                        ViewBag.BrandsId = Utility.PopulateBrandsSelectListItem();
                    }
                    if (viewModel.Category != null && viewModel.Category.Id > 0)
                    {
                        ViewBag.Category = new SelectList(Utility.PopulateCategorySelectListItem(), Utility.VALUE, Utility.TEXT, viewModel.Category.Id);
                    }
                    else
                    {
                        ViewBag.Category = Utility.PopulateCategorySelectListItem();
                    }
                    if (viewModel.Product.ProductType != null && viewModel.Product.ProductType.Id > 0)
                    {
                        ViewBag.ProductTypeId = new SelectList(Utility.PopulateProductTypeselectListItem(), Utility.VALUE, Utility.TEXT, viewModel.Product.ProductType.Id);
                    }
                    else
                    {
                        ViewBag.ProductTypeId = Utility.PopulateProductTypeselectListItem();
                    }
                    if (viewModel.Product.UnitMeasurement != null && viewModel.Product.UnitMeasurement.Id > 0)
                    {
                        ViewBag.UnitMeasurementId = new SelectList(Utility.PopulateUnitOfMeasurementSelectListItem(), Utility.VALUE, Utility.TEXT, viewModel.Product.Id);
                    }
                    else
                    {
                        ViewBag.UnitMeasurementId = Utility.PopulateUnitOfMeasurementSelectListItem();
                    }

                    if (viewModel.ProductOrderLogs != null && viewModel.ProductOrderLogs.Id > 0)
                    {
                        if (viewModel.ProductOrderLogs.Product.Id > 0 && viewModel.ProductOrderLogs.Product != null)
                        {
                            ViewBag.ProductId = new SelectList(Utility.PopulateProductselectListItem(), Utility.VALUE,
                                Utility.TEXT, viewModel.ProductOrderLogs.Product.Id);
                        }
                        else
                        {
                            ViewBag.ProductId = Utility.PopulateProductselectListItem();
                        }
                        if (viewModel.ProductOrderLogs.Vendors.Id > 0 && viewModel.ProductOrderLogs.Vendors != null)
                        {
                            ViewBag.ProductId = new SelectList(Utility.PopulateVendorselectListItem(), Utility.VALUE,
                                Utility.TEXT, viewModel.Vendors.Id);
                        }
                        else
                        {
                            ViewBag.VendorId = Utility.PopulateVendorselectListItem();
                        }

                    }
                    else
                    {
                        ViewBag.ProductId = Utility.PopulateProductselectListItem();
                        ViewBag.VendorId = Utility.PopulateVendorselectListItem();
                    }


                }



            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public ActionResult ManageCategory()
        {
            ProductViewModel viewModel = new ProductViewModel();
            try
            {
                categoryLogic = new CategoryLogic();
                viewModel.Categories = categoryLogic.GetAll();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(viewModel);
        }

        public ActionResult ViewProductTypes()
        {
            ProductViewModel viewModel = new ProductViewModel();
            try
            {

                ProductTypeLogic productTypeLogic = new ProductTypeLogic();

                viewModel.ProductTypes = productTypeLogic.GetAll();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return View(viewModel);
        }

        public ActionResult CreateProductType()
        {
            ViewBag.Category = Utility.PopulateCategorySelectListItem();
            return View();
        }

        [HttpPost]
        public ActionResult CreateProductType(ProductViewModel product)
        {
            try
            {
                ProductTypeLogic productTypelogic = new ProductTypeLogic();
                product.ProductType.Category = new Category()
                {
                    Id = product.Category.Id
                };
                ProductType productType = productTypelogic.Create(product.ProductType);
                if (productType != null)
                {
                    SetMessage("Operation Successful", Message.Category.Information);
                    ViewBag.Category = Utility.PopulateCategorySelectListItem();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            ModelState.Clear();
            return View();
        }



        public ActionResult EditProductType(string ProNo)
        {
            ProductViewModel viewModel = new ProductViewModel();
            try
            {
                int id = Convert.ToInt32(ProNo);
                if (ProNo == null)
                {
                    throw new ArgumentException("Vendor was not Found");
                }
                ProductTypeLogic productTypeLogic = new ProductTypeLogic();
                viewModel.ProductType = productTypeLogic.GetModelBy(v => v.Id == id);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult EditProductType(ProductViewModel model)
        {
            try
            {
                if (model.ProductType != null)
                {
                    ProductTypeLogic productTypeLogic = new ProductTypeLogic();
                    var productType = productTypeLogic.Modify(model.ProductType);
                    if (productType)
                    {
                        SetMessage("Operation Succesful", Message.Category.Information);
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
            return RedirectToAction("ViewProductTypes", "Product", new { area = "Admin" });
        }

        public ActionResult DeleteProductType(string ProNo)
        {
            try
            {
                int Id = Convert.ToInt32(ProNo);
                if (ProNo == null)
                {
                    throw new ArgumentException("ProductType Was not found to be deleted");
                }
                ProductTypeLogic productTypeLogic = new ProductTypeLogic();
                var deletedProductType = productTypeLogic.Delete(s => s.Id == Id);
                if (deletedProductType)
                {
                    SetMessage("Operation Successful", Message.Category.Information);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return RedirectToAction("ViewProductTypes", "Product", new { area = "Admin" });
        }
        public ActionResult ViewUnits()
        {
            ProductViewModel viewModel = new ProductViewModel();
            try
            {

                UnitLogic unitLogic = new UnitLogic();

                viewModel.Units = unitLogic.GetAll();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return View(viewModel);
        }
        public ActionResult CreateUnit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateUnit(ProductViewModel product)
        {
            try
            {
                UnitLogic unitLogic = new UnitLogic();
                Unit unit = unitLogic.Create(product.Unit);
                if (unit != null)
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



        public ActionResult EditUnit(string ProNo)
        {
            ProductViewModel viewModel = new ProductViewModel();
            try
            {
                int id = Convert.ToInt32(ProNo);
                if (ProNo == null)
                {
                    throw new ArgumentException("Unit was not Found");
                }
                UnitLogic unitLogic = new UnitLogic();
                viewModel.Unit = unitLogic.GetModelBy(v => v.Id == id);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult EditUnit(ProductViewModel model)
        {
            try
            {
                if (model.Unit != null)
                {
                    UnitLogic unitLogic = new UnitLogic();
                    var unit = unitLogic.Modify(model.Unit);
                    if (unit)
                    {
                        SetMessage("Operation Succesful", Message.Category.Information);
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
            return RedirectToAction("ViewUnits", "Product", new { area = "Admin" });
        }

        public ActionResult DeleteUnit(string ProNo)
        {
            try
            {
                int Id = Convert.ToInt32(ProNo);
                if (ProNo == null)
                {
                    throw new ArgumentException("Unit Was not found to be deleted");
                }
                UnitLogic unitLogic = new UnitLogic();
                var deletedUnit = unitLogic.Delete(s => s.Id == Id);
                if (deletedUnit)
                {
                    SetMessage("Operation Successful", Message.Category.Information);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return RedirectToAction("ViewUnits", "Product", new { area = "Admin" });
        }

        public ActionResult DeleteCategory(string ProNo)
        {
            try
            {
                int Id = Convert.ToInt32(ProNo);
                if (ProNo == null)
                {
                    throw new ArgumentException("Category Not Found Was not found to be deleted");
                }
                CategoryLogic categoryLogic = new CategoryLogic();
                var deletedCategory = categoryLogic.Delete(s => s.Category_Id == Id);
                if (deletedCategory)
                {
                    SetMessage("Operation Successful", Message.Category.Information);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return RedirectToAction("ManageCategory", "Product", new { area = "Admin" });
        }

        public ActionResult ViewBrands()
        {
            ProductViewModel viewModel = new ProductViewModel();
            try
            {
                BrandLogic brandLogic = new BrandLogic();
                viewModel.Brands = brandLogic.GetAll();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return View(viewModel);
        }
        public ActionResult CreateBrand()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateBrand(ProductViewModel product)
        {
            try
            {
                BrandLogic brandLogic = new BrandLogic();
                Brand brand = brandLogic.Create(product.Brand);
                if (brand != null)
                {
                    SetMessage("Operation Successful", Message.Category.Information);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            ModelState.Clear();
            return RedirectToAction("ViewBrands", "Product", new { area = "Admin" });
        }



        public ActionResult EditBrand(string ProNo)
        {
            ProductViewModel viewModel = new ProductViewModel();
            try
            {
                int id = Convert.ToInt32(ProNo);
                if (ProNo == null)
                {
                    throw new ArgumentException("Brand was not Found");
                }
                BrandLogic brandLogic = new BrandLogic();
                viewModel.Brand = brandLogic.GetModelBy(v => v.Id == id);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult EditBrand(ProductViewModel model)
        {
            try
            {
                if (model.Brand != null)
                {
                    BrandLogic brandLogic = new BrandLogic();
                    var brand = brandLogic.Modify(model.Brand);
                    if (brand)
                    {
                        SetMessage("Operation Succesful", Message.Category.Information);
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
            return RedirectToAction("ViewBrands", "Product", new { area = "Admin" });
        }


        public ActionResult EditCategory(string ProNo)
        {
            ProductViewModel viewModel = new ProductViewModel();
            try
            {
                if (ProNo == null)
                {
                    throw new ArgumentException("Category was not Found");
                }
                int id = Convert.ToInt32(ProNo);

                CategoryLogic categoryLogic = new CategoryLogic();
                viewModel.Category = categoryLogic.GetModelBy(x => x.Category_Id == id);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult EditCategory(ProductViewModel model)
        {
            try
            {
                string imageUrl = "";
                foreach(string file in Request.Files)
                {
                    string filePath = "~/Content/NewImages/CategoryImages/";
                    HttpPostedFileBase hpf=Request.Files[file]as HttpPostedFileBase;
                    string pathForSaving = Server.MapPath("~/Content/NewImages/CategoryImages/");
                    string saveFileName = Path.Combine(pathForSaving, hpf.FileName);
                    if(hpf.FileName!="")
                    {
                        if(System.IO.File.Exists(saveFileName))
                        {
                            System.IO.File.Delete(saveFileName);
                        }
                        hpf.SaveAs(saveFileName);
                        model.Category.ImageUrl = filePath + hpf.FileName;
                    }
                }
                if (model.Category != null)
                {
                    CategoryLogic categoryLogic = new CategoryLogic();
                    var category = categoryLogic.Modify(model.Category);
                    if (category)
                    {
                        SetMessage("Operation Succesful", Message.Category.Information);
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
            return RedirectToAction("ManageCategory", "Product", new { area = "Admin" });
        }



        public ActionResult DeleteBrand(string ProNo)
        {
            try
            {
                int Id = Convert.ToInt32(ProNo);
                if (ProNo == null)
                {
                    throw new ArgumentException("Brand Was not found to be deleted");
                }
                BrandLogic brandLogic = new BrandLogic();
                var deletedbrand = brandLogic.Delete(s => s.Id == Id);
                if (deletedbrand)
                {
                    SetMessage("Operation Successful", Message.Category.Information);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return RedirectToAction("ViewBrands", "Product", new { area = "Admin" });
        }
        public JsonResult CreateNewProductType(string productType, string CategoryId)
        {
            try
            {
                if (string.IsNullOrEmpty(productType) && string.IsNullOrEmpty(CategoryId))
                {
                    return null;
                }

                ProductTypeLogic productTypeLogic = new ProductTypeLogic();
                ProductType type = new ProductType();
                List<ProductType> productTypes = new List<ProductType>();
                int id = Convert.ToInt32(CategoryId);
                type.Name = productType.ToUpper();
                type.Active = true;
                type.Category = new Category()
                {
                    Id = id
                };
                var createdProductType = productTypeLogic.Create(type);
                if (createdProductType != null)
                {
                    productTypes = productTypeLogic.GetAll();
                }

                return Json(new SelectList(productTypes, "Id", "Name"), JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public JsonResult CreateNewProductCategory(string category, string description)
        {
            try
            {
                if (string.IsNullOrEmpty(category) && string.IsNullOrEmpty(description))
                {
                    return null;
                }

                CategoryLogic categoryLogic = new CategoryLogic();
                Category cate = new Category();
                List<Category> categories = new List<Category>();
                cate.Name = category.ToUpper();
                cate.Description = description.ToUpper();
                var createCategory = categoryLogic.Create(cate);
                if (createCategory != null)
                {
                    categories = categoryLogic.GetAll();
                }

                return Json(new SelectList(categories, "CateId", "CateName"), JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public JsonResult CreateNewProductBrand(string productBrand)
        {
            try
            {
                if (string.IsNullOrEmpty(productBrand))
                {
                    return null;
                }

                BrandLogic brandLogic = new BrandLogic();
                Brand brand = new Brand();
                List<Brand> brands = new List<Brand>();

                brand.Name = productBrand.ToUpper();
                brand.Active = true;
                var createdProductBrand = brandLogic.Create(brand);
                if (createdProductBrand != null)
                {
                    brands = brandLogic.GetAll();
                }

                return Json(new SelectList(brands, "Id", "Name"), JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JsonResult CreateNewUnit(string unit)
        {
            try
            {
                if (string.IsNullOrEmpty(unit))
                {
                    return null;
                }

                UnitLogic unitLogic = new UnitLogic();
                Unit unitOfMeasurement = new Unit();
                List<Unit> units = new List<Unit>();

                unitOfMeasurement.Name = unit;
                unitOfMeasurement.Active = true;
                var createdUnit = unitLogic.Create(unitOfMeasurement);
                if (createdUnit != null)
                {
                    units = unitLogic.GetAll();
                }

                return Json(new SelectList(units, "Id", "Name"), JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult UploadProduct()
        {
            ProductViewModel productViewModel = new ProductViewModel();
            try
            {
                ViewBag.Category = Utility.PopulateCategorySelectListItem();
                ViewBag.ProductType = Utility.PopulateProductTypeselectListItem();
                ViewBag.Brand = Utility.PopulateBrandsSelectListItem();
                ViewBag.Unit = Utility.PopulateUnitOfMeasurementSelectListItem();
                productViewModel.UploadFormat = new List<ProductUploadFormat>();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return View(productViewModel);
        }

        [HttpPost]
        public ActionResult UploadProduct(ProductViewModel product)
        {
            try
            {
                string pathForSaving = Server.MapPath("~/Content/ExcelUploads");
                string savedFileName = "";

                List<ProductUploadFormat> productUploads = new List<ProductUploadFormat>();

                foreach (string file in Request.Files)
                {
                    HttpPostedFileBase hpf = Request.Files[file];
                    if (hpf.ContentLength == 0)
                    {
                        continue;
                    }

                    if (CreateFolderIfNeeded(pathForSaving))
                    {
                        FileInfo fileInfo = new FileInfo(hpf.FileName);
                        string fileExtension = fileInfo.Extension;
                        string newFile = "Product Upload" + "__";
                        string newFileName = newFile + DateTime.Now.ToString().Replace("/", "").Replace(":", "").Replace(" ", "") + fileExtension;

                        savedFileName = Path.Combine(pathForSaving, newFileName);
                        hpf.SaveAs(savedFileName);
                        IExcelServiceManager excelServiceManager = new ExcelServiceManager();
                        DataSet dsProductList = excelServiceManager.UploadExcel(savedFileName);
                        if (dsProductList != null && dsProductList.Tables[0].Rows.Count > 0)
                        {
                            //int count = 0;
                            for (int i = 0; i < dsProductList.Tables[0].Rows.Count; i++)
                            {
                                ProductUploadFormat uploadFormat = new ProductUploadFormat();
                                uploadFormat.Sn = dsProductList.Tables[0].Rows[i][0].ToString().Trim();
                                uploadFormat.Name = dsProductList.Tables[0].Rows[i][1].ToString().Trim();
                                uploadFormat.Description = dsProductList.Tables[0].Rows[i][2].ToString().Trim();
                                uploadFormat.ProductType = product.ProductType.Name;
                                uploadFormat.Brand = product.Brand.Name;
                                uploadFormat.Price = dsProductList.Tables[0].Rows[i][3].ToString().Trim();
                                uploadFormat.Weight = dsProductList.Tables[0].Rows[i][4].ToString().Trim();
                                uploadFormat.Sku = dsProductList.Tables[0].Rows[i][6].ToString().Trim();
                                uploadFormat.UnitMeasurement = product.Unit.Name;
                                productUploads.Add(uploadFormat);
                            }
                            product.UploadFormat = productUploads;
                            TempData["Product"] = product;

                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return View(product);
        }

        public ActionResult SaveUpload()
        {
            try
            {
                ProductViewModel productViewModel = new ProductViewModel();
                productViewModel = (ProductViewModel)TempData["UploadResultViewModel"];
                BrandLogic brandLogic = new BrandLogic();
                UnitLogic unitLogic = new UnitLogic();
                ProductTypeLogic productTypeLogic = new ProductTypeLogic();
                int count = 0;

                if (productViewModel != null)
                {
                    for (int i = 0; i < productViewModel.UploadFormat.Count; i++)
                    {
                        using (TransactionScope scope = new TransactionScope())
                        {
                            Product product = new Product();
                            product.Name = productViewModel.UploadFormat[i].Name;
                            product.Description = productViewModel.UploadFormat[i].Description;
                            product.Sku = productViewModel.UploadFormat[i].Sku;
                            product.ImageUrl = "~/Content/images/tag.png";
                            product.UnitMeasurement = new Unit()
                            {
                                Name = productViewModel.UploadFormat[i].UnitMeasurement
                            };
                            product.Price = Convert.ToDecimal(productViewModel.UploadFormat[i].Price);
                            product.Weight = Convert.ToInt32(productViewModel.UploadFormat[i].Weight);
                            var checkBrand = brandLogic.GetModelBy(s => s.Name.Contains(productViewModel.UploadFormat[i].Brand));
                            if (checkBrand != null)
                            {
                                product.Brand = checkBrand;
                            }
                            product.Brand = new Brand()
                            {
                                Name = productViewModel.UploadFormat[i].Brand
                            };
                            var checkUnit = unitLogic.GetModelBy(u => u.Name.Contains(productViewModel.UploadFormat[i].UnitMeasurement));
                            if (checkUnit != null)
                            {
                                product.UnitMeasurement = checkUnit;
                            }
                            var checkProductType = productTypeLogic.GetModelBy(pt => pt.Name.Contains(productViewModel.UploadFormat[i].ProductType));
                            if (checkProductType != null)
                            {
                                product.ProductType = checkProductType;
                            }
                            product.ProductType = new ProductType()
                            {
                                Name = productViewModel.UploadFormat[i].ProductType
                            };

                            var createdProduct = productLogic.Create(product);
                            if (createdProduct != null)
                            {
                                count += 1;
                            }
                            scope.Complete();
                        }

                    }
                    if (count > 0)
                    {
                        SetMessage("Operation Successful", Message.Category.Information);
                    }
                    else
                    {
                        SetMessage(count + "Could not be Uploaded Please crossCheck and try again", Message.Category.Error);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return RedirectToAction("UploadProduct", "Product", new { area = "Admin" });
        }

        private bool CreateFolderIfNeeded(string path)
        {
            try
            {
                bool result = true;
                if (!Directory.Exists(path))
                {
                    try
                    {
                        Directory.CreateDirectory(path);
                    }
                    catch (Exception)
                    {

                        result = false;
                    }
                }

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private DataSet ReadExcelResult(string filepath)
        {
            DataSet Result = null;
            try
            {
                string xConnStr = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + filepath + ";" +
                                  "Extended Properties=Excel 8.0;";
                OleDbConnection connection = new OleDbConnection(xConnStr);

                connection.Open();
                DataTable sheet = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                DataRow dataRow = sheet.Rows[0];

                string sheetName = dataRow[2].ToString().Replace("'", "");
                OleDbCommand command = new OleDbCommand("Select * FROM [" + sheetName + "]", connection);
                // Create DbDataReader to Data Worksheet

                OleDbDataAdapter MyData = new OleDbDataAdapter();
                MyData.SelectCommand = command;
                DataSet ds = new DataSet();
                ds.Clear();
                MyData.Fill(ds);
                connection.Close();

                Result = ds;


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;

        }
        public ActionResult DownloadProductSample()
        {
            try
            {
                List<ProductUploadFormat> productFormartList = new List<ProductUploadFormat>();

                int count = 1;
                for (int i = 0; i < 150; i++)
                {
                    ProductUploadFormat prodcutFormat = new ProductUploadFormat();
                    prodcutFormat.Sn = count.ToString();
                    if (i == 0)
                    {

                        prodcutFormat.Name = "Coca-Cola";
                        prodcutFormat.Description = "1 Ltr";
                        prodcutFormat.Price = "100";
                        prodcutFormat.Weight = "1 ltr";
                        prodcutFormat.Sku = "1";
                    }
                    productFormartList.Add(prodcutFormat);
                    count++;
                }


                IExcelServiceManager excelServiceManager = new ExcelServiceManager();
                MemoryStream ms = excelServiceManager.DownloadExcel(productFormartList);
                ms.WriteTo(System.Web.HttpContext.Current.Response.OutputStream);
                System.Web.HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + "productUploadSample.xlsx");
                System.Web.HttpContext.Current.Response.StatusCode = 200;
                System.Web.HttpContext.Current.Response.End();
            }
            catch (Exception ex)
            {
                SetMessage("Error! " + ex.Message, Message.Category.Error);
            }

            return View();
        }
        //public async Task<JsonResult> CreateProductVariant(string productId, string productItems, string productVariantArray)
        //{
        //    try
        //    {

        //        if (!string.IsNullOrEmpty(productId) && !string.IsNullOrEmpty(productItems))
        //        {
        //            JavaScriptSerializer productModelserializer = new JavaScriptSerializer();
        //            ProductModel productModel = productModelserializer.Deserialize<ProductModel>(productItems);
        //            List<ProductModel> productVariantArrayList = productModelserializer.Deserialize<List<ProductModel>>(productVariantArray);

        //            ProductVariantLogic productVariantLogic = new ProductVariantLogic();
        //            int prodId = Convert.ToInt32(productId);
        //            ProductVariant productVariant = productVariantLogic.GetModelBy(p => p.Product_Id == prodId);
        //            var existingProductKey = productVariantLogic.GetModelsBy(p => p.Product_Id == prodId && p.Key_1.Contains(productModel.productVariantKey1) || p.Key_2.Contains(productModel.productVariantKey2) || p.Key_3.Contains(productModel.productVariantKey3));
        //            if (productVariant != null)
        //            {
        //                if (existingProductKey != null)
        //                {
        //                    if (!string.IsNullOrEmpty(productVariant.Key1) && productVariant.Key1.Contains(productModel.productVariantKey1))
        //                    {
        //                        productVariant.Key1 = productModel.productVariantKey1;
        //                        productVariant.Value1 = productModel.productVariantValue1;
        //                    }
        //                    if (!string.IsNullOrEmpty(productVariant.Key2)  && productVariant.Key2.Contains(productModel.productVariantKey2))
        //                    {
        //                        productVariant.Key1 = productModel.productVariantKey2;
        //                        productVariant.Value1 = productModel.productVariantValue2;
        //                    }
        //                    if (!string.IsNullOrEmpty(productVariant.Key3)  && productVariant.Key3.Contains(productModel.productVariantKey3))
        //                    {
        //                        productVariant.Key1 = productModel.productVariantKey3;
        //                        productVariant.Value1 = productModel.productVariantValue3;
        //                    }

        //                    //productVariant = productVariantLogic.Modify(productVariant);
        //                    if (productVariant.Id > 0)
        //                    {
        //                        List<ProductVariantOptions> productVariantOptionsList = new List<ProductVariantOptions>();
        //                        for (int i = 0; i < productVariantArrayList.Count; i++)
        //                        {
        //                            if (Convert.ToBoolean(productVariantArrayList[i].select))
        //                            {
        //                                ProductVariantOptions productVariantOptions = new ProductVariantOptions();
        //                                productVariantOptions.ProductOptionName = productVariantArrayList[i].Name;
        //                                productVariantOptions.ProductVariant = productVariant;
        //                                productVariantOptions.Price = Convert.ToDecimal(productVariantArrayList[i].Price);
        //                                productVariantOptionsList.Add(productVariantOptions);
        //                            }

        //                        }
        //                        if (productVariantOptionsList.Count > 0)
        //                        {
        //                            ProductVariantOptionsLogic productVariantOptionsLogic = new ProductVariantOptionsLogic();
        //                            productVariantOptionsLogic.Create(productVariantOptionsList);

        //                        }
        //                        return Json("Operation successful", JsonRequestBehavior.AllowGet);
        //                    }
        //                }

        //                if (string.IsNullOrEmpty(productVariant.Key1) || productVariant.Key1.Contains(',') && !string.IsNullOrEmpty(productModel.productVariantKey1))
        //                {
        //                    productVariant.Key1 = productModel.productVariantKey1;
        //                    productVariant.Value1 = productModel.productVariantValue1;
        //                }
        //                if (string.IsNullOrEmpty(productVariant.Key2) || productVariant.Key2.Contains(',') && !string.IsNullOrEmpty(productModel.productVariantKey2))
        //                {
        //                    productVariant.Key1 = productModel.productVariantKey2;
        //                    productVariant.Value1 = productModel.productVariantValue2;
        //                }
        //                if (string.IsNullOrEmpty(productVariant.Key3) || productVariant.Key3.Contains(',') && !string.IsNullOrEmpty(productModel.productVariantKey3))
        //                {
        //                    productVariant.Key1 = productModel.productVariantKey3;
        //                    productVariant.Value1 = productModel.productVariantValue3;
        //                }
        //                productVariant = productVariantLogic.Create(productVariant);
        //                if (productVariant.Id > 0)
        //                {
        //                    List<ProductVariantOptions> productVariantOptionsList = new List<ProductVariantOptions>();
        //                    for (int i = 0; i < productVariantArrayList.Count; i++)
        //                    {
        //                        if (Convert.ToBoolean(productVariantArrayList[i].select))
        //                        {
        //                            ProductVariantOptions productVariantOptions = new ProductVariantOptions();
        //                            productVariantOptions.ProductOptionName = productVariantArrayList[i].Name;
        //                            productVariantOptions.ProductVariant = productVariant;
        //                            productVariantOptions.Price = Convert.ToDecimal(productVariantArrayList[i].Price);
        //                            productVariantOptionsList.Add(productVariantOptions);
        //                        }

        //                    }
        //                    if (productVariantOptionsList.Count > 0)
        //                    {
        //                        ProductVariantOptionsLogic productVariantOptionsLogic = new ProductVariantOptionsLogic();
        //                        productVariantOptionsLogic.Create(productVariantOptionsList);

        //                    }
        //                    return Json("Operation successful", JsonRequestBehavior.AllowGet);
        //                }
        //            }
        //            else
        //            {
        //                productVariant = new ProductVariant
        //                {
        //                    Product = new Product()
        //                    {
        //                        Id = prodId
        //                    },
        //                    Key1 = productModel.productVariantKey1,
        //                    Value1 = productModel.productVariantValue1
        //                };



        //                if (!string.IsNullOrEmpty(productModel.productVariantKey2))
        //                {
        //                    productVariant.Key2 = productModel.productVariantKey2;
        //                    productVariant.Value2 = productModel.productVariantValue2;
        //                }
        //                if (!string.IsNullOrEmpty(productModel.productVariantKey3))
        //                {
        //                    productVariant.Key2 = productModel.productVariantKey3;
        //                    productVariant.Value3 = productModel.productVariantValue3;
        //                }
        //                productVariant = productVariantLogic.Create(productVariant);
        //                if (productVariant.Id > 0)
        //                {
        //                    List<ProductVariantOptions> productVariantOptionsList = new List<ProductVariantOptions>();
        //                    for (int i = 0; i < productVariantArrayList.Count; i++)
        //                    {
        //                        if (Convert.ToBoolean(productVariantArrayList[i].select))
        //                        {
        //                            ProductVariantOptions productVariantOptions = new ProductVariantOptions();
        //                            productVariantOptions.ProductOptionName = productVariantArrayList[i].Name;
        //                            productVariantOptions.ProductVariant = productVariant;
        //                            productVariantOptions.Price = Convert.ToDecimal(productVariantArrayList[i].Price);
        //                            productVariantOptionsList.Add(productVariantOptions);
        //                        }

        //                    }
        //                    if (productVariantOptionsList.Count > 0)
        //                    {
        //                        ProductVariantOptionsLogic productVariantOptionsLogic = new ProductVariantOptionsLogic();
        //                        productVariantOptionsLogic.Create(productVariantOptionsList);

        //                    }
        //                    return Json("Operation successful", JsonRequestBehavior.AllowGet);
        //                }
        //            }

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        return Json("Error", JsonRequestBehavior.AllowGet);

        //    }
        //    return Json("Error", JsonRequestBehavior.AllowGet);
        //}

        [HttpPost] 
        public JsonResult UploadImage()
        {

            string path = null;
            productLogic = new ProductLogic();
            HttpPostedFileBase file = Request.Files["productImage"];
            //string imagePath = Server.MapPath("~/Content/images/");
            string imagePath = Server.MapPath("~/Content/NewImages/");

            if (file != null && file.ContentLength != 0)
            {

                path = Path.Combine(imagePath, file.FileName);
                //if (System.IO.File.Exists(path))
                //{
                //    System.IO.File.Delete(path);
                //    return Json(new { status = true, JsonRequestBehavior.AllowGet });
                //}
                file.SaveAs(Path.Combine(imagePath, file.FileName));
                string image = ("~/Content/NewImages/" + file.FileName);
                return Json(image, JsonRequestBehavior.AllowGet);
            }
            return Json(new { status = false, JsonRequestBehavior.AllowGet });
        }
        public JsonResult RemoveUploadImage()
        {

            string path = null;
            productLogic = new ProductLogic();
            HttpPostedFileBase file = Request.Files["productImage"];
            //string imagePath = Server.MapPath("~/Content/images/");
            string imagePath = Server.MapPath("~/Content/NewImages/");

            if (file != null && file.ContentLength != 0)
            {

                path = Path.Combine(imagePath, file.FileName);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                    return Json(new { status = true, JsonRequestBehavior.AllowGet });
                }
                //file.SaveAs(Path.Combine(imagePath, file.FileName));
                //string image = Server.MapPath("/Content/NewImages/" + file.FileName);
                //return Json(image, JsonRequestBehavior.AllowGet);
            }
            return Json(new { status = false, JsonRequestBehavior.AllowGet });
        }
        public JsonResult PopulateProductTypeByCategoryId(string categoryId)
        {
            try
            {
                if (string.IsNullOrEmpty(categoryId))
                {
                    return null;
                }

                int id = Convert.ToInt32(categoryId);
                List<ProductType> prductTypeDropdown = productTypeLogic.GetModelsBy(x => x.Category_Id == id);
                return Json(new SelectList(prductTypeDropdown, TypeId, TypeName), JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public JsonResult CreateProductCategory(FormCollection formData)
        {
            HttpPostedFileBase file = Request.Files["image"];

            string categoryName = formData["categoryName"].ToString().ToUpper();
            string description = formData["description"].ToString().ToUpper();

            JsonResultModel result = new JsonResultModel();
            try
            {
                if (string.IsNullOrEmpty(categoryName) && string.IsNullOrEmpty(description))
                {
                    return null;
                }
                string path = null;
                string image;
                string imagePath = Server.MapPath("~/Content/NewImages/CategoryImages/");
                if (file != null && file.ContentLength != 0)
                {

                    path = Path.Combine(imagePath, file.FileName);
                    //if (System.IO.File.Exists(path))
                    //{
                    //    System.IO.File.Delete(path);
                    //    return Json(new { status = true, JsonRequestBehavior.AllowGet });
                    //}
                    file.SaveAs(Path.Combine(imagePath, file.FileName));
                    image = "~/Content/NewImages/CategoryImages/" + file.FileName;
                    Category category = new Category()
                    {
                        Description = description,
                        Name = categoryName,
                        ImageUrl = image
                    };
                    categoryLogic.Create(category);
                    result.IsError = false;
                    result.Message = "Operation Successful!";
                }
                else
                {
                    Category category1 = new Category()
                    {
                        Description = description,
                        Name = categoryName,
                    };
                    categoryLogic.Create(category1);
                    result.IsError = false;
                    result.Message = "Operation Successful!";
                }

            }
            catch (Exception ex)
            {
                result.IsError = true;
                result.Message = ex.Message;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
    public class JsonResultModel
    {
        public bool IsError { get; set; }
        public string Message { get; set; }
        public List<CourierService> CourierService { get; set; }
        //public List<CourierRegion> CourierRegion { get; set; }
        public List<CourierAreaCharges> CourierAreaCharges { get; set; }
        public List<CourierCharges> CourierCharges { get; set; }
    }
}