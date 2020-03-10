using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Abundance_SN.Business;
using Abundance_SN.Controllers;
using Abundance_SN.Model.Model;
using Abundance_SN.Models;
using JavaScriptEngineSwitcher.Core;
using PagedList;

namespace Abundance_SN.Areas.Customer.Controllers
{
    public class ProductController : BaseController
    {
        int PageSize = 12;
        public ActionResult ViewProduct(string Id, string miniFliterValue, string maxFliterValue, int? page)
        {
            ProductViewModel viewModel = new ProductViewModel();
            try
            {
                if (Id == null)
                {
                    return RedirectToAction("Index", "Home", new { area = "" });
                }
                int id = Convert.ToInt32(Id);
                InventoryLogic inventoryLogic = new InventoryLogic();
                ProductTypeLogic productTypeLogic = new ProductTypeLogic();
                ProductLogic productLogic = new ProductLogic();
                BrandLogic brandLogic = new BrandLogic();
                CategoryLogic categoryLogic = new CategoryLogic();
                viewModel.Products = new List<Product>();
                var productTypeName = productTypeLogic.GetModelBy(x => x.Id == id);
                 var categoryId=Convert.ToInt16(productTypeName.Category.Id);
                if (productTypeName != null)
                {
                    ViewBag.ProductTypeName = productTypeName.Name.ToUpper().Trim();
                }
                if (!string.IsNullOrEmpty(miniFliterValue) && !string.IsNullOrEmpty(maxFliterValue))
                {
                    decimal maxPrice = Convert.ToDecimal(maxFliterValue);
                    decimal miniPrice = Convert.ToDecimal(miniFliterValue);

                   var inventoryByProductType = inventoryLogic.GetModelsBy(s => s.PRODUCT.Product_Type_Id == id && s.Quantity > 0 && s.PRODUCT.Active);
                    viewModel.Inventorieses =
                        inventoryByProductType.Where(s => s.Product.DiscountAmount >= miniPrice && s.Product.DiscountAmount <= maxPrice).ToList();
                    viewModel.ProductTypes = productTypeLogic.GetAll();
                    viewModel.Brands = brandLogic.GetAll();
                    var distinctProductId = viewModel.Inventorieses.Select(s => s.Product.Id).Distinct().ToList();
                    for (int i = 0; i < distinctProductId.Count; i++)
                    {
                        Product product = new Product();
                        long productId = distinctProductId[i];
                        product = productLogic.GetModelBy(s => s.Id == productId);
                        viewModel.Products.Add(product);
                    }
                    if (viewModel.Products.Count == 0)
                    {
                        SetMessage("No Search Result Found", Message.Category.Error);
                    }
                    viewModel.GroupCountInt = viewModel.Inventorieses.Select(p => p.Product.ProductType.Category.Id).Distinct().ToList();
                    viewModel.GroupCount = viewModel.Inventorieses.Select(s => s.Product.Brand.Id).Distinct().ToList();
                    viewModel.MiniFilterValue = miniFliterValue;
                    viewModel.MaxFilterValue = maxFliterValue;
                }
                else
                {
                    viewModel.Inventorieses = inventoryLogic.GetModelsBy(s => s.PRODUCT.Product_Type_Id == id && s.Quantity > 0 && s.PRODUCT.Active);
                    viewModel.ProductTypes = productTypeLogic.GetAll();
                    //var productByProductType = productLogic.GetAll().Where(x => x.ProductType.Id == id && x.Active).ToList();
                    viewModel.PagedProducts = productLogic.GetAll().Where(s => s.ProductType.Id == id && s.Active).ToPagedList(page ?? 1, PageSize);
                    viewModel.Products = productLogic.GetAll();
                    //ViewBag.ProductOfProductType = productByProductType;
                    viewModel.Brands = brandLogic.GetAll();
                    var distinctProductId = viewModel.Inventorieses.Select(s => s.Product.Id).Distinct().ToList();
                    for (int i = 0; i < distinctProductId.Count; i++)
                    {
                        Product product = new Product();
                        long productId = distinctProductId[i];
                        product = productLogic.GetModelBy(s => s.Id == productId);
                        viewModel.Products.Add(product);
                    }
                   
                    viewModel.GroupCountInt = viewModel.Inventorieses.Select(p => p.Product.ProductType.Category.Id).Distinct().ToList();
                    viewModel.GroupCount = viewModel.Inventorieses.Select(s => s.Product.Brand.Id).Distinct().ToList();

                }
                ViewBag.Id = id;
                ViewBag.ProductTypeDropDown = Utility.PopulateProductTypeselectListItem(categoryId);

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return View(viewModel);
        }

        public ActionResult ViewSingleProduct(string Id)
        {
            ProductViewModel productViewModel = new ProductViewModel();
            try
            {

                if (string.IsNullOrEmpty(Id))
                {
                    throw new Exception("Product could not be found");
                }
                int id = Convert.ToInt32(Id);
                ProductLogic productLogic = new ProductLogic();
                ProductVariantLogic productVariantLogic = new ProductVariantLogic();
                InventoryLogic inventoryLogic = new InventoryLogic();
                ProductImageGallaryLogic productImageGallaryLogic = new ProductImageGallaryLogic();
                productViewModel.Product = productLogic.GetModelBy(p => p.Id == id );
                //get the ProductType Id of the selected Product
                int productTypeId=productViewModel.Product.ProductType.Id;
                var productViewedRecently=productLogic.GetAll().Where(x => x.ProductType.Id == productTypeId).OrderByDescending(x=>x.Visits).Take(6).ToList();
                ViewBag.ProductTypeViewed = productViewedRecently;
                productLogic.UpdateVisit(productViewModel.Product);
                productViewModel.ProductVariants = productVariantLogic.GetModelsBy(pv => pv.Product_Id == id);
                productViewModel.Products = new List<Product>();
                productViewModel.Inventorieses = inventoryLogic.GetModelsBy(s => s.Quantity > 0 && s.PRODUCT.Active);
                var distintProductId = productViewModel.Inventorieses.Select(s => s.Product.Id).Distinct().ToList();
                List<SelectListItem> value1SelectListItems = new List<SelectListItem>();
                List<SelectListItem> value2SelectListItems = new List<SelectListItem>();
                List<SelectListItem> value3SelectListItems = new List<SelectListItem>();

                for (int i = 0; i < distintProductId.Count; i++)
                {
                    Product product = new Product();
                    long productId = distintProductId[i];
                    product = productLogic.GetModelBy(s => s.Id == productId);
                    productViewModel.Products.Add(product);
                }

                if (productViewModel.ProductVariants.Count > 0)
                {
                    for (int i = 0; i < productViewModel.ProductVariants.Count; i++)
                    {
                        List<SelectListItem> colorSelectListItems = new List<SelectListItem>();
                        SelectListItem list = new SelectListItem();
                        list.Value = "";
                        list.Text = "--Select--";
                        value1SelectListItems.Add(list);
                        value2SelectListItems.Add(list);
                        value3SelectListItems.Add(list);
                        colorSelectListItems.Add(list);

                        string[] splitedKeyVariants1 = productViewModel.ProductVariants[i].Value1.Split(',');

                        for (int j = 0; j < splitedKeyVariants1.Length; j++)
                        {
                            if (!string.IsNullOrEmpty(splitedKeyVariants1[j]))
                            {
                                SelectListItem key1SelesItem = new SelectListItem();
                                key1SelesItem.Value = productViewModel.ProductVariants[i].Id.ToString();
                                key1SelesItem.Text = splitedKeyVariants1[j];
                                value1SelectListItems.Add(key1SelesItem);
                            }
                        }
                        ViewBag.value1SelectListItems = value1SelectListItems;
                        ViewBag.value2SelectListItems = new SelectList(new List<SelectListItem>(), "Id", "Name");
                        ViewBag.value3SelectListItems = new SelectList(new List<SelectListItem>(), "Id", "Name");

                    }
                }
                var distinctBrand = productViewModel.Inventorieses.Select(b => b.Id).Distinct().ToList();
                productViewModel.GroupCount = distinctBrand;
                CustomerReviewLogic customerReviewLogic = new CustomerReviewLogic();
                productViewModel.CustomerReviews = customerReviewLogic.GetModelsBy(s => s.Product_Id == id);
                productViewModel.ProductImageGallaries = productImageGallaryLogic.GetModelsBy(s => s.Product_Id == id);

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return View(productViewModel);
        }

        public ActionResult ViewProductByBrand(string Id)
        {
            ProductViewModel viewModel = new ProductViewModel();
            try
            {
                if (Id == null)
                {
                    return RedirectToAction("Index", "Home", new { area = "" });
                }
                int id = Convert.ToInt32(Id);
                ProductLogic productLogic = new ProductLogic();
                ProductTypeLogic productTypeLogic = new ProductTypeLogic();
                BrandLogic brandLogic = new BrandLogic();
                List<Brand> brand = new List<Brand>();
                viewModel.Products = productLogic.GetModelsBy(s => s.Brand_Id == id && s.Active);
                viewModel.ProductTypes = productTypeLogic.GetAll();
                viewModel.Brands = brandLogic.GetAll();

                for (int i = 0; i < viewModel.Products.Count; i++)
                {
                    brand.Add(viewModel.Products[i].Brand);
                }
                var distinctBrand = brand.Select(b => b.Id).Distinct().ToList();
                viewModel.GroupCount = distinctBrand;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return View(viewModel);
        }
        public ActionResult ViewProductByCategory(string Id, int? page)
        {
            ProductViewModel viewModel = new ProductViewModel();
            try
            {
                if (string.IsNullOrEmpty(Id))
                {
                    throw new ArgumentNullException("Id");
                }
                int id = Convert.ToInt32(Id);
                ProductLogic productLogic = new ProductLogic();
                List<ProductType> productTypes = new List<ProductType>();
                ProductTypeLogic productTypeLogic = new ProductTypeLogic();
                BrandLogic brandLogic = new BrandLogic();
                List<Brand> brand = new List<Brand>();
                CategoryLogic categoryLogic = new CategoryLogic();
                var categories = categoryLogic.GetModelBy(x => x.Category_Id == id);
                if(categories !=null)
                {
                    ViewBag.CategoryName = categories.Name.ToUpper().Trim();
                }

                ViewBag.Id = Id;
                //viewModel.Products = productLogic.GetModelsBy(s => s.PRODUCT_TYPE.Category_Id == id && s.Active);
                viewModel.PagedProducts = productLogic.GetModelsBy(s => s.PRODUCT_TYPE.Category_Id == id && s.Active).ToPagedList(page ?? 1, PageSize);
                viewModel.Products = productLogic.GetAll();
                //ViewBag.ProductByCategory = viewModel.Products;
                //for (int i = 0; i < viewModel.Products.Count; i++)
                //{
                //    productTypes.Add(viewModel.Products[i].ProductType);
                //}

                //var distinctProductType = viewModel.Products.Select(p => p.ProductType.Category.Id).Distinct().ToList();
                //viewModel.GroupCountInt = distinctProductType;
                //viewModel.ProductTypes = productTypeLogic.GetAll();
                //viewModel.Brands = brandLogic.GetAll();

                //for (int i = 0; i < viewModel.Products.Count; i++)
                //{
                //    brand.Add(viewModel.Products[i].Brand);
                //}
                //var distinctBrand = brand.Select(b => b.Id).Distinct().ToList();
                //viewModel.GroupCount = distinctBrand;
                ViewBag.CategoryDropDown = Utility.PopulateCategorySelectListItem();


            }
            catch (Exception ex)
            {

                throw ex;
            }
            return View(viewModel);
        }

        public ActionResult ViewProductByFilter(string fliter, int? page)
        {
            ProductViewModel productViewModel = new ProductViewModel();
            try
            {
                productViewModel.Products = new List<Product>();
                InventoryLogic inventoryLogic = new InventoryLogic();
                ProductTypeLogic productTypeLogic = new ProductTypeLogic();
                ProductLogic productLogic = new ProductLogic();
                BrandLogic brandLogic = new BrandLogic();
                List<Brand> brand = new List<Brand>();

                if (!string.IsNullOrEmpty(fliter))
                {
                    ViewBag.FilterWord = fliter;
                    //productViewModel.Inventorieses = inventoryLogic.GetModelsBy(p => p.PRODUCT.Name.Contains(fliter) || p.PRODUCT.PRODUCT_TYPE.Name.Contains(fliter) || p.PRODUCT.BRAND.Name.Contains(fliter) || p.PRODUCT.Description.Contains(fliter) && p.PRODUCT.Active);
                    productViewModel.PagedInventorys = inventoryLogic.GetModelsBy(p => p.PRODUCT.Name.Contains(fliter) || p.PRODUCT.PRODUCT_TYPE.Name.Contains(fliter) || p.PRODUCT.BRAND.Name.Contains(fliter) || p.PRODUCT.Description.Contains(fliter) && p.PRODUCT.Active).ToPagedList(page ?? 1, PageSize);
                    productViewModel.Products = productLogic.GetAll();
                    productViewModel.Inventorieses = inventoryLogic.GetModelsBy(p => p.PRODUCT.Name.Contains(fliter) || p.PRODUCT.PRODUCT_TYPE.Name.Contains(fliter) || p.PRODUCT.BRAND.Name.Contains(fliter) || p.PRODUCT.Description.Contains(fliter) && p.PRODUCT.Active);
                    if (productViewModel.Inventorieses.Count > 0)
                    {
                        productViewModel.ProductTypes = productTypeLogic.GetAll();
                        productViewModel.Brands = brandLogic.GetAll();
                        var distinctProduct = productViewModel.Inventorieses.Select(p => p.Product.Id).Distinct().ToList();
                        for (int i = 0; i < distinctProduct.Count; i++)
                        {
                            Product product = new Product();
                            long productId = distinctProduct[i];
                            product = productLogic.GetModelBy(s => s.Id == productId);
                            productViewModel.Products.Add(product);
                        }
                        var distinctBrand = productViewModel.Inventorieses.Select(p => p.Product.Brand.Id).Distinct().ToList();
                        var distintCategory = productViewModel.Inventorieses.Select(p => p.Product.ProductType.Category.Id).Distinct().ToList();
                        productViewModel.GroupCountInt = distintCategory;
                        productViewModel.GroupCount = distinctBrand;
                    }
                    else
                    {
                        SetMessage("No Search Result Found", Message.Category.Error);
                    }

                }
                else
                {
                    var inventory = inventoryLogic.GetAll();
                    productViewModel.ProductTypes = productTypeLogic.GetAll();
                    productViewModel.Brands = brandLogic.GetAll();

                    for (int i = 0; i < inventory.Count; i++)
                    {
                        brand.Add(inventory[i].Product.Brand);
                    }
                    var distinctBrand = brand.Select(b => b.Id).Distinct().ToList();
                    var distintCategory = productViewModel.Products.Select(p => p.ProductType.Category.Id).Distinct().ToList();
                    productViewModel.GroupCountInt = distintCategory;
                    productViewModel.GroupCount = distinctBrand;
                    productViewModel.PagedInventorys = null;
                    productViewModel.Inventorieses = null;
                    SetMessage("No Search Query Found", Message.Category.Error);
                }



            }
            catch (Exception ex)
            {

                throw ex;
            }
            return View(productViewModel);
        }

        public JsonResult ViewProductByFilteredPrice(string fiilterurl, string miniFliterValue, string maxFliterValue, string Id)
        {
            string urlQueryString = null;
            try
            {
                if (!string.IsNullOrEmpty(fiilterurl) && !string.IsNullOrEmpty(miniFliterValue) && !string.IsNullOrEmpty(maxFliterValue))
                {
                    urlQueryString = fiilterurl + "?Id=" + Id + "&miniFliterValue=" + miniFliterValue + "&maxFliterValue=" + maxFliterValue;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Json(urlQueryString, JsonRequestBehavior.AllowGet);
        }

        // public JsonResult ViewProductByFilteredPrice(string miniFliterValue, string maxFliterValue, string Id)
        //{
        //      List<Product> products = new List<Product>();
        //    try
        //    {
        //        int productTypeId = Convert.ToInt32(Id);
        //        if (!string.IsNullOrEmpty(miniFliterValue) && !string.IsNullOrEmpty(maxFliterValue))
        //        {
        //            decimal maxPrice = Convert.ToDecimal(maxFliterValue);
        //            decimal miniPrice = Convert.ToDecimal(miniFliterValue);
        //            ProductLogic productLogic = new ProductLogic();
        //            products = productLogic.GetModelsBy(s => s.Product_Type_Id == productTypeId && (s.Price >= miniPrice && s.Price <= maxPrice));
                   
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }
        //    return Json(products, JsonRequestBehavior.AllowGet);
        //}

          

        public ActionResult CreateCustomerReview(ProductViewModel viewModel)
        {
            try
            {
                CustomerReviewLogic customerReviewLogic = new CustomerReviewLogic();
                if (viewModel.CustomerReview != null)
                {
                    viewModel.CustomerReview.Product = new Product()
                    {
                        Id = viewModel.Product.Id
                    };

                    var customerReview = customerReviewLogic.Create(viewModel.CustomerReview);
                    if (customerReview != null)
                    {
                        SetMessage("Thank you for your Review it has successfully been submitted", Message.Category.Information);
                    }
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return RedirectToAction("ViewSingleProduct", "Product", new { area = "Customer", Id = viewModel.Product.Id });
        }

        public JsonResult PopulateKey2DropDown(string key2, string Id)
        {
            List<ProductVariantOptions> distinctProcutVariantOptions = new List<ProductVariantOptions>();
            try
            {
                if (!string.IsNullOrEmpty(Id) && !string.IsNullOrEmpty(key2))
                {
                    int id = Convert.ToInt32(Id);
                    ProductVariantOptionsLogic productOptionLogic = new ProductVariantOptionsLogic();
                    List<ProductVariantOptions> productVariantOptions = productOptionLogic.GetModelsBy(pv => pv.Product_Option_Name.Contains(key2) && pv.Product_Variant_Id == id);
                  
                    if (productVariantOptions.Count > 0)
                    {
                        for (int j = 0; j < productVariantOptions.Count; j++)
                        {
                            string[] splittedProductVariantOption = productVariantOptions[j].ProductOptionName.Split('-');
                            ProductVariantOptions variantOptions = new ProductVariantOptions();
                            var productVariantOption = splittedProductVariantOption.LastOrDefault();
                            if (productVariantOption != null && !productVariantOption.Contains("*"))
                            {
                                
                                variantOptions.Id = productVariantOptions[j].Id;
                                variantOptions.Price = productVariantOptions[j].ProductVariant.Price ?? productVariantOptions[j].ProductVariant.Product.DiscountAmount;
                                variantOptions.ProductOptionName = splittedProductVariantOption.LastOrDefault();
                                variantOptions.ProductVariant = new ProductVariant
                                {
                                    Key2 = productVariantOptions[j].ProductVariant.Key2,
                                    Id = productVariantOptions[j].ProductVariant.Id,
                                    Key1 = productVariantOptions[j].ProductVariant.Key1,
                                    Value1 = key2,
                                    Product = productVariantOptions[j].ProductVariant.Product
                                };

                                string cartImage = productVariantOptions[j].ProductVariant.Product.ImageUrl.Replace('~',' ');
                                variantOptions.ImageUrl = productVariantOptions[j].ImageUrl ?? cartImage.TrimStart();
                                variantOptions.Price = productVariantOptions[j].Price ?? productVariantOptions[j].ProductVariant.Product.DiscountAmount;
                                 distinctProcutVariantOptions.Add(variantOptions);
                                
                            } 
                            else if (productVariantOption != null && productVariantOption.Contains("*"))
                            {

                                splittedProductVariantOption = productVariantOption.Split('*');
                                variantOptions.Id = productVariantOptions[j].ProductVariant.Id;
                                variantOptions.Price = productVariantOptions[j].ProductVariant.Price ?? productVariantOptions[j].ProductVariant.Product.DiscountAmount;
                                variantOptions.ProductOptionName = splittedProductVariantOption.FirstOrDefault();
                                variantOptions.ProductVariant = new ProductVariant()
                                {
                                    Key2 = productVariantOptions[j].ProductVariant.Key2,
                                      Id = productVariantOptions[j].ProductVariant.Id,
                                       Key1 = productVariantOptions[j].ProductVariant.Key1,
                                    Value1 = key2,
                                    Product = productVariantOptions[j].ProductVariant.Product
                                };

                                string cartImage = productVariantOptions[j].ProductVariant.Product.ImageUrl.Replace('~', ' ');
                                variantOptions.ImageUrl = productVariantOptions[j].ImageUrl ?? cartImage.TrimStart();
                                bool exists = distinctProcutVariantOptions.Any(name => name.ProductOptionName == variantOptions.ProductOptionName);
                                if (exists == false)
                                {
                                    distinctProcutVariantOptions.Add(variantOptions);
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
            return Json(distinctProcutVariantOptions, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PopulateKey3DropDown(string key2, string Id, string key1)
        {
            List<ProductVariantOptions> distinctProcutVariantOptions = new List<ProductVariantOptions>();

            try
            {
                if (!string.IsNullOrEmpty(Id) && !string.IsNullOrEmpty(key2))
                {
                    int id = Convert.ToInt32(Id);
                    ProductVariantOptionsLogic productOptionLogic = new ProductVariantOptionsLogic();
                    ProductVariantOptions variantOptions = new ProductVariantOptions();
                    List<ProductVariantOptions> productVariantOptions = productOptionLogic.GetModelsBy(pv => pv.Product_Option_Name.Contains(key2) && pv.Product_Variant_Id == id);
                    var productOption = productOptionLogic.GetModelsBy(pv => pv.Product_Option_Name.Contains(key2) && pv.Product_Variant_Id == id && pv.Product_Option_Name.Contains(key1)).LastOrDefault();
                    if (productOption != null)
                    {
                        variantOptions.Id = productOption.Id;
                        variantOptions.Price = productOption.Price ?? productOption.ProductVariant.Product.DiscountAmount;
                        variantOptions.ProductVariant = new ProductVariant
                        {
                            Product = new Product()
                            {
                                Id = productOption.ProductVariant.Product.Id
                            },
                            Key3 = productOption.ProductVariant.Key3,
                            Key1 = productOption.ProductVariant.Key1,
                            Key2 = productOption.ProductVariant.Key2,
                            Value1 = key1,
                            Value2 = key2
                        };
                        string cartImage = productOption.ProductVariant.Product.ImageUrl.Replace('~', ' ');
                        variantOptions.ImageUrl = productOption.ImageUrl ?? cartImage;
                        //distinctProcutVariantOptions.Add(variantOptions);
                        if (productVariantOptions.Count > 0)
                        {
                            for (int j = 0; j < productVariantOptions.Count; j++)
                            {
                                string[] splittedProductVariantOption = productVariantOptions[j].ProductOptionName.Split('*');
                                variantOptions.ProductOptionName = splittedProductVariantOption.LastOrDefault().TrimStart();
                                variantOptions.Id = productOption.Id;
                                variantOptions.Price = productVariantOptions[j].Price ?? productVariantOptions[j].ProductVariant.Product.DiscountAmount;
                                variantOptions.ProductVariant = new ProductVariant
                                {
                                    Product = new Product()
                                    {
                                        Id = productVariantOptions[j].ProductVariant.Product.Id
                                    },
                                    Key3 = variantOptions.ProductVariant.Key3,
                                    Key1 = productOption.ProductVariant.Key1,
                                    Key2 = productOption.ProductVariant.Key2,
                                    Value1 = key1,
                                    Value2 = key2
                                };
                                variantOptions.ImageUrl = productOption.ImageUrl ?? cartImage;
                                bool exists = distinctProcutVariantOptions.Any(name => name.ProductOptionName == variantOptions.ProductOptionName && name.Id == variantOptions.Id);
                                if (exists == false)
                                {
                                    distinctProcutVariantOptions.Add(variantOptions);
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
            return Json(distinctProcutVariantOptions, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetProductVariantOptions(string key1, string key2, string key3)
        {
            ProductVariantOptions variantOptions = new ProductVariantOptions();
            try
            {
                if (!string.IsNullOrEmpty(key1) && !string.IsNullOrEmpty(key2) && !string.IsNullOrEmpty(key3))
                {
                    ProductVariantOptionsLogic productOptionLogic = new ProductVariantOptionsLogic();
                    List<ProductVariantOptions> productVariantOptions = productOptionLogic.GetModelsBy(pv => pv.Product_Option_Name.Contains(key1) && pv.Product_Option_Name.Contains(key2) && pv.Product_Option_Name.Contains(key3));

                    if (productVariantOptions.Count > 0)
                    {
                        var productOption = productVariantOptions.LastOrDefault();
                        if (productOption != null)
                        {
                            variantOptions.Id = productOption.Id;
                            variantOptions.Price = productOption.Price ?? productOption.ProductVariant.Product.DiscountAmount;
                            variantOptions.ProductVariant = new ProductVariant
                            {
                               Product = productOption.ProductVariant.Product,
                               Key1 = productOption.ProductVariant.Key1,
                               Key2 = productOption.ProductVariant.Key2,
                               Key3 = productOption.ProductVariant.Key3,
                               Value1 = key1,
                               Value2 = key2,
                               Value3 = key3
                            };
                            string cartImage = productOption.ProductVariant.Product.ImageUrl.Replace('~', ' ');
                            variantOptions.ImageUrl = productOption.ImageUrl ?? cartImage.TrimStart();

                        }
                    }
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Json(variantOptions, JsonRequestBehavior.AllowGet);
        }
    }
}