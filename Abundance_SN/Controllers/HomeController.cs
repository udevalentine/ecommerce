using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Abundance_SN.Business;
using Abundance_SN.Model.Model;
using Abundance_SN.Models;
using Microsoft.Ajax.Utilities;

namespace Abundance_SN.Controllers
{
    public class HomeController : Controller
    {
        private const string Id = "Product_Variant_Id";
        private const string Name = "Value_1";
         
        ProductVariantLogic productVariant = new ProductVariantLogic();
        ProductVariantOptionsLogic productOptionLogic = new ProductVariantOptionsLogic();
        ProductVariantOptions variantOption = new ProductVariantOptions();


        public ActionResult Index()
        {
             ProductViewModel viewModel = new ProductViewModel();
            try
            
            {

                InventoryLogic inventoryLogic = new InventoryLogic();
                ProductTypeLogic productTypeLogic = new ProductTypeLogic();
                HomePageSliderLogic sliderLogic = new HomePageSliderLogic();
                SalesLogLogic salesLogLogic = new SalesLogLogic();
                viewModel.SalesLogs = new List<SalesLogs>();
                var dealWeekList = salesLogLogic.GetModelsBy().GroupBy(i => i.Product.Id).OrderByDescending(d => d.Count()).Take(5).Select(d => d.Key).ToList();
                for (int i = 0; i < dealWeekList.Count; i++)
                {
                    long id = Convert.ToInt64(dealWeekList[i]);
                     var myProduct = salesLogLogic.GetModelsBy(x => x.Product_Id == id).FirstOrDefault();
                    viewModel.SalesLogs.Add(myProduct);
                }
                viewModel.Products = new List<Product>();
                viewModel.ProductTypes = productTypeLogic.GetAll();
                viewModel.Inventorieses = inventoryLogic.GetModelsBy(s =>s.Quantity > 0 && s.PRODUCT.Active);
                var distintProduct = viewModel.Inventorieses.Select(p => p.Product.Id).Distinct().ToList();
                //var distinctProductType = viewModel.Inventorieses.Select(b => b.Product.ProductType.Category.Id).Distinct().ToList();
                var distinctProductType = viewModel.Inventorieses.Select(b => b.Product.ProductType.Category.Id).ToList();
                for (int i = 0; i < distintProduct.Count; i++)
                {
                    ProductLogic productLogic = new ProductLogic();
                    Product product = new Product();
                    long productId = distintProduct[i];
                    product = productLogic.GetModelBy(s => s.Id == productId);
                    viewModel.Products.Add(product);
                }
                viewModel.GroupCountInt = distinctProductType;
                viewModel.HomePageSliders = sliderLogic.GetAll();
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            return View(viewModel);
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        //public JsonResult PopulateProductVariant(string productId)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(productId))
        //        {
        //            return null;
        //        }
        //        List<SelectListItem> value1SelectListItems = new List<SelectListItem>();
        //        int id = Convert.ToInt32(productId);
        //        List<ProductVariant> productVariantDropdown = productVariant.GetModelsBy(x => x.Product_Id == id);
        //        string variantId=productVariantDropdown.FirstOrDefault().Id.ToString();
        //        var dropDown=productVariantDropdown.FirstOrDefault().Value1;
        //        if(dropDown!=null)
        //        {
        //            string[] splitProductVariant = dropDown.Split(',');
        //            for (int j = 0; j < splitProductVariant.Length; j++)
        //            {
        //                if (!string.IsNullOrEmpty(splitProductVariant[j]))
        //                {
        //                    SelectListItem key1SelesItem = new SelectListItem();
        //                    key1SelesItem.Value = variantId;
        //                    key1SelesItem.Text = splitProductVariant[j];
        //                    value1SelectListItems.Add(key1SelesItem);
        //                }
        //            }
        //        }

        //        return Json(new SelectList(value1SelectListItems, "Value","Text"), JsonRequestBehavior.AllowGet); 

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public JsonResult PopulateKey2ValueDropdown(string variantId, string key1Value)
        //{
        //    List<ProductVariantOptions> distinctProcutVariantOptions = new List<ProductVariantOptions>();
        //    ProductVariantOptions variantOptions = new ProductVariantOptions();
        //    try
        //    {
        //        if (string.IsNullOrEmpty(variantId) && string.IsNullOrEmpty(key1Value))
        //        {
        //            return null;
        //        }

        //        int id = Convert.ToInt32(variantId);
        //        string variantSize = key1Value.Trim();
        //        List<ProductVariant> value2List = productVariant.GetModelsBy(x => x.Product_Variant_Id == id && x.Value_1.Contains(variantSize));
        //        List<SelectListItem> value2SelectListItems = new List<SelectListItem>();
        //        if (value2List.Count > 0)
        //        {
        //            var firstList = value2List.FirstOrDefault().Value2;
        //            if (firstList != null)
        //            {
        //                string[] splitProductVariantValue2 = firstList.Split(',');
        //                for (int j = 0; j < splitProductVariantValue2.Length; j++)
        //                {
        //                    if (!string.IsNullOrEmpty(splitProductVariantValue2[j]))
        //                    {
        //                        SelectListItem key2SelesItem = new SelectListItem();
        //                        key2SelesItem.Value = id.ToString();
        //                        key2SelesItem.Text = splitProductVariantValue2[j];
        //                        value2SelectListItems.Add(key2SelesItem);
        //                    }
        //                }
        //            }
        //            return Json(new SelectList(value2SelectListItems, "Value", "Text"), JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //        {
        //            ProductVariantOptionsLogic productVariantOptions = new ProductVariantOptionsLogic();
        //            List<ProductVariantOptions> exactProduct = productVariantOptions.GetModelsBy(x => x.Product_Variant_Id == id && x.Product_Option_Name.Contains(variantSize));
        //            if (exactProduct.Count > 0)
        //            {
        //                var productOption = exactProduct.FirstOrDefault();
        //                if (productOption != null)
        //                {
        //                    variantOptions.Id = productOption.Id;
        //                    variantOptions.Price = productOption.Price ?? productOption.ProductVariant.Product.DiscountAmount;
        //                    variantOptions.ProductVariant = new ProductVariant
        //                    {
        //                        Product = productOption.ProductVariant.Product,
        //                        Key1 = productOption.ProductVariant.Key1,
        //                        Value1 = key1Value
        //                    };
        //                    string cartImage = productOption.ProductVariant.Product.ImageUrl.Replace('~', ' ');
        //                    variantOptions.ImageUrl = productOption.ImageUrl ?? cartImage.TrimStart();
                            
        //                }
        //            }
        //            return Json(variantOptions, JsonRequestBehavior.AllowGet);
        //        }
                

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public JsonResult PopulateKey3ValueDropdown(string variantId, string key1Value, string key2Value)
        //{
        //    List<ProductVariantOptions> distinctProcutVariantOptions = new List<ProductVariantOptions>();
        //    ProductVariantOptions variantOptions = new ProductVariantOptions();
        //    try
        //    {
        //        if (string.IsNullOrEmpty(variantId) && string.IsNullOrEmpty(key1Value) && string.IsNullOrEmpty(key2Value))
        //        {
        //            return null;
        //        }

        //        int id = Convert.ToInt32(variantId);
        //        string variantSize = key1Value.Trim();
        //        string variantColour = key2Value.Trim();
        //        List<ProductVariant> value3List = productVariant.GetModelsBy(x => x.Product_Variant_Id == id && x.Value_1.Contains(variantSize) && x.Value_1.Contains(variantColour));
        //        List<SelectListItem> value3SelectListItems = new List<SelectListItem>();
        //        if (value3List.Count > 0)
        //        {
        //            var firstList = value3List.FirstOrDefault().Value3;
        //            if (firstList != null)
        //            {
        //                string[] splitProductVariantValue3 = firstList.Split(',');
        //                for (int j = 0; j < splitProductVariantValue3.Length; j++)
        //                {
        //                    if (!string.IsNullOrEmpty(splitProductVariantValue3[j]))
        //                    {
        //                        SelectListItem key3SelesItem = new SelectListItem();
        //                        key3SelesItem.Value = id.ToString();
        //                        key3SelesItem.Text = splitProductVariantValue3[j];
        //                        value3SelectListItems.Add(key3SelesItem);
        //                    }
        //                }
        //            }
        //            return Json(new SelectList(value3SelectListItems, "Value", "Text"), JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //        {
        //            ProductVariantOptionsLogic productVariantOptions = new ProductVariantOptionsLogic();
        //            List<ProductVariantOptions> exactProduct = productVariantOptions.GetModelsBy(x => x.Product_Variant_Id == id && x.Product_Option_Name.Contains(variantSize) && x.Product_Option_Name.Contains(variantColour));
        //            if (exactProduct.Count > 0)
        //            {
        //                var productOption = exactProduct.FirstOrDefault();
        //                if (productOption != null)
        //                {
        //                    variantOptions.Id = productOption.Id;
        //                    variantOptions.Price = productOption.Price ?? productOption.ProductVariant.Product.DiscountAmount;
        //                    variantOptions.ProductVariant = new ProductVariant
        //                    {
        //                        Product = productOption.ProductVariant.Product,
        //                        Key1 = productOption.ProductVariant.Key1,
        //                        Key2 = productOption.ProductVariant.Key2,
        //                        Value1 = key1Value,
        //                        Value2 = key2Value

        //                    };
        //                    string cartImage = productOption.ProductVariant.Product.ImageUrl.Replace('~', ' ');
        //                    variantOptions.ImageUrl = productOption.ImageUrl ?? cartImage.TrimStart();
                            
        //                }
                        
        //            }
        //            return Json(variantOptions, JsonRequestBehavior.AllowGet);
        //        }
                
                
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public JsonResult GetProductVariantProperties(string variantId, string key1Value, string key2Value)
        //{

        //    try
        //    {

        //        if (string.IsNullOrEmpty(variantId) && string.IsNullOrEmpty(key1Value) && string.IsNullOrEmpty(key2Value))
        //        {
        //            return null;
        //        }
        //            int id = Convert.ToInt32(variantId);
        //        string variantSize = key1Value.Trim();
        //        string variantColour = key2Value.Trim();

        //        List<ProductVariantOptions> productvariant = productOptionLogic.GetModelsBy(x => x.Product_Variant_Id == id && x.Product_Option_Name.Contains(variantSize) && x.Product_Option_Name.Contains(variantColour));
        //            if (productvariant.Count > 0)
        //            {
        //                var productOption = productvariant.LastOrDefault();
        //                if (productOption != null)
        //                {
        //                    variantOption.Id = productOption.Id;
        //                    variantOption.Price = productOption.Price ?? productOption.ProductVariant.Product.DiscountAmount;
        //                    variantOption.ProductVariant = new ProductVariant
        //                    {
        //                        Product = productOption.ProductVariant.Product,
        //                        Key1 = productOption.ProductVariant.Key1,
        //                        Key2 = productOption.ProductVariant.Key2,
        //                        Key3 = productOption.ProductVariant.Key3,
        //                        Value1 = key1Value,
        //                        Value2 = key2Value
        //                    };
        //                    string cartImage = productOption.ProductVariant.Product.ImageUrl.Replace('~', ' ');
        //                    variantOption.ImageUrl = productOption.ImageUrl ?? cartImage.TrimStart();

        //                }

        //            }

        //        return Json(variantOption, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}
        public JsonResult PopulateProductVariant(string productId)
        {
            try
            {
                if (string.IsNullOrEmpty(productId))
                {
                    return null;
                }
                List<SelectListItem> value1SelectListItems = new List<SelectListItem>();
                int id = Convert.ToInt32(productId);
                List<ProductVariant> productVariantDropdown = productVariant.GetModelsBy(x => x.Product_Id == id);
                string variantId = productVariantDropdown.FirstOrDefault().Id.ToString();
                var dropDown = productVariantDropdown.FirstOrDefault().Value1;
                if (dropDown != null)
                {
                    string[] splitProductVariant = dropDown.Split(',');
                    for (int j = 0; j < splitProductVariant.Length; j++)
                    {
                        if (!string.IsNullOrEmpty(splitProductVariant[j]))
                        {
                            SelectListItem key1SelesItem = new SelectListItem();
                            key1SelesItem.Value = variantId;
                            key1SelesItem.Text = splitProductVariant[j];
                            value1SelectListItems.Add(key1SelesItem);
                        }
                    }
                }

                return Json(new SelectList(value1SelectListItems, "Value", "Text"), JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public JsonResult PopulateKey2ValueDropdown(string variantId, string key1Value)
        {
            List<ProductVariantOptions> distinctProcutVariantOptions = new List<ProductVariantOptions>();
            InventoryLogic inventoryLogic = new InventoryLogic();
            try
            {
                if (!string.IsNullOrEmpty(Id) && !string.IsNullOrEmpty(key1Value))
                {
                    int id = Convert.ToInt32(variantId);
                    long productOptionId = 0;
                    ProductVariantOptionsLogic productOptionLogic = new ProductVariantOptionsLogic();
                    List<ProductVariantOptions> productVariantOptions = productOptionLogic.GetModelsBy(pv => pv.Product_Option_Name.Contains(key1Value) && pv.Product_Variant_Id == id);

                    if (productVariantOptions.Count > 0)
                    {
                        for (int j = 0; j < productVariantOptions.Count; j++)
                        {
                            string[] splittedProductVariantOption = productVariantOptions[j].ProductOptionName.Split('-');
                            ProductVariantOptions variantOptions = new ProductVariantOptions();
                            var productVariantOption = splittedProductVariantOption.LastOrDefault();
                            if (productVariantOption != null && !productVariantOption.Contains("*") && productVariantOptions[j].ProductVariant.Key2 != null)
                            {

                                variantOptions.Id = productVariantOptions[j].Id;
                                //get the quantity of the variant from inventory, to avoid shopping a variant with zero or negative quantity
                                var productInventory=inventoryLogic.GetModelBy(x => x.Product_Variant_Option_Id == variantOptions.Id);
                                variantOptions.Price = productVariantOptions[j].Price ?? productVariantOptions[j].ProductVariant.Product.DiscountAmount;
                                //effect the price if discount exist for the product 
                                if (productVariantOptions[j].ProductVariant.Product.Discount > 0)
                                {
                                    var discountedValue = (variantOptions.Price * productVariantOptions[j].ProductVariant.Product.Discount / 100);
                                    variantOptions.Price = variantOptions.Price - discountedValue;
                                }
                                variantOptions.ProductOptionName = splittedProductVariantOption.LastOrDefault();
                                variantOptions.ProductVariant = new ProductVariant
                                {
                                    Key2 = productVariantOptions[j].ProductVariant.Key2,
                                    Id = productVariantOptions[j].ProductVariant.Id,
                                    Key1 = productVariantOptions[j].ProductVariant.Key1,
                                    Value1 = key1Value,
                                    Product = productVariantOptions[j].ProductVariant.Product
                                    


                                };

                                string cartImage = productVariantOptions[j].ProductVariant.Product.ImageUrl.Replace('~', ' ');
                                variantOptions.ImageUrl = productVariantOptions[j].ImageUrl ?? cartImage.TrimStart();
                                //variantOptions.Price = productVariantOptions[j].Price ?? productVariantOptions[j].ProductVariant.Product.DiscountAmount;
                                variantOptions.VariantQuantity = productInventory.Quantity;
                                distinctProcutVariantOptions.Add(variantOptions);

                            }
                            else if (productVariantOption != null && productVariantOption.Contains("*"))
                            {

                                splittedProductVariantOption = productVariantOption.Split('*');
                                variantOptions.Id = productVariantOptions[j].Id;
                                //get the quantity of the variant from inventory, to avoid shopping a variant with zero or negative quantity
                                var productInventory = inventoryLogic.GetModelBy(x => x.Product_Variant_Option_Id == variantOptions.Id);
                                variantOptions.Price = productVariantOptions[j].Price ?? productVariantOptions[j].ProductVariant.Product.DiscountAmount;
                                //effect the price if discount exist for the product 
                                if (productVariantOptions[j].ProductVariant.Product.Discount > 0)
                                {
                                    var discountedValue = (variantOptions.Price * productVariantOptions[j].ProductVariant.Product.Discount / 100);
                                    variantOptions.Price = variantOptions.Price - discountedValue;
                                }
                                variantOptions.ProductOptionName = splittedProductVariantOption.FirstOrDefault();
                                variantOptions.ProductVariant = new ProductVariant()
                                {
                                    Key2 = productVariantOptions[j].ProductVariant.Key2,
                                    Id = productVariantOptions[j].ProductVariant.Id,
                                    Key1 = productVariantOptions[j].ProductVariant.Key1,
                                    Value1 = key1Value,
                                    Product = productVariantOptions[j].ProductVariant.Product
                                };

                                string cartImage = productVariantOptions[j].ProductVariant.Product.ImageUrl.Replace('~', ' ');
                                variantOptions.ImageUrl = productVariantOptions[j].ImageUrl ?? cartImage.TrimStart();
                                variantOptions.VariantQuantity = productInventory.Quantity;
                                bool exists = distinctProcutVariantOptions.Any(name => name.ProductOptionName == variantOptions.ProductOptionName);
                                if (exists == false)
                                {
                                    distinctProcutVariantOptions.Add(variantOptions);
                                }

                            }
                            else
                            {

                                variantOptions.Id = productVariantOptions[j].Id;
                                
                                //variantOptions.Price = productVariantOptions[j].ProductVariant.Price ?? productVariantOptions[j].ProductVariant.Product.DiscountAmount;
                                variantOptions.Price = productVariantOptions[j].Price ?? productVariantOptions[j].ProductVariant.Product.DiscountAmount;
                                //effect the price if discount exist for the product 
                                if (productVariantOptions[j].ProductVariant.Product.Discount > 0)
                                {
                                    var discountedValue = (variantOptions.Price * productVariantOptions[j].ProductVariant.Product.Discount / 100);
                                    variantOptions.Price = variantOptions.Price - discountedValue;
                                }
                                variantOptions.ProductOptionName = productVariantOptions[j].ProductOptionName;
                                variantOptions.ProductVariant = new ProductVariant()
                                {
                                    Id = (int)productVariantOptions[j].Id,
                                    Key1 = productVariantOptions[j].ProductVariant.Key1,
                                    Value1 = key1Value,
                                    Product = productVariantOptions[j].ProductVariant.Product
                                };
                                productOptionId = productVariantOptions[j].Id;
                                string cartImage = productVariantOptions[j].ProductVariant.Product.ImageUrl.Replace('~', ' ');
                                variantOptions.ImageUrl = productVariantOptions[j].ImageUrl ?? cartImage.TrimStart();
                                //get the quantity of the variant from inventory, to avoid shopping a variant with zero or negative quantity
                                var productInventory = inventoryLogic.GetModelBy(x => x.Product_Variant_Option_Id == productOptionId);
                                variantOptions.VariantQuantity = productInventory.Quantity;
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

        public JsonResult PopulateKey3ValueDropdown(string variantId, string key1Value, string key2Value)
        {
            List<ProductVariantOptions> distinctProcutVariantOptions = new List<ProductVariantOptions>();
            InventoryLogic inventoryLogic = new InventoryLogic();

            try
            {
                if (!string.IsNullOrEmpty(variantId) && !string.IsNullOrEmpty(key2Value))
                {
                    int id = Convert.ToInt32(variantId);
                    ProductVariantOptionsLogic productOptionLogic = new ProductVariantOptionsLogic();
                    ProductVariantOptions variantOptions = new ProductVariantOptions();
                    List<ProductVariantOptions> productVariantOptions = productOptionLogic.GetModelsBy(pv => pv.Product_Option_Name.Contains(key2Value) && pv.Product_Variant_Id == id);
                    var productOption = productOptionLogic.GetModelsBy(pv => pv.Product_Option_Name.Contains(key2Value) && pv.Product_Variant_Id == id && pv.Product_Option_Name.Contains(key1Value)).LastOrDefault();
                    if (productOption != null)
                    {
                        variantOptions.Id = productOption.Id;
                        //get the quantity of the variant from inventory, to avoid shopping a variant with zero or negative quantity
                        var productInventory = inventoryLogic.GetModelBy(x => x.Product_Variant_Option_Id == variantOptions.Id);
                        variantOptions.Price = productOption.Price ?? productOption.ProductVariant.Product.DiscountAmount;
                        //effect the price if discount exist for the product 
                        if (productOption.ProductVariant.Product.Discount > 0)
                        {
                            var discountedValue = (variantOptions.Price * productOption.ProductVariant.Product.Discount/ 100);
                            variantOptions.Price = variantOptions.Price - discountedValue;
                        }
                        variantOptions.ProductVariant = new ProductVariant
                        {
                            Product = new Product()
                            {
                                Id = productOption.ProductVariant.Product.Id
                            },
                            Key3 = productOption.ProductVariant.Key3,
                            Key1 = productOption.ProductVariant.Key1,
                            Key2 = productOption.ProductVariant.Key2,
                            Value1 = key1Value,
                            Value2 = key2Value
                        };
                        string cartImage = productOption.ProductVariant.Product.ImageUrl.Replace('~', ' ');
                        variantOptions.ImageUrl = productOption.ImageUrl ?? cartImage;
                        variantOptions.VariantQuantity = productInventory.Quantity;
                        //distinctProcutVariantOptions.Add(variantOptions);
                        if (productVariantOptions.Count > 0)
                        {
                            for (int j = 0; j < productVariantOptions.Count; j++)
                            {
                                string[] splittedProductVariantOption = productVariantOptions[j].ProductOptionName.Split('*');
                                variantOptions.ProductOptionName = splittedProductVariantOption.LastOrDefault().TrimStart();
                                variantOptions.Id = productOption.Id;
                                variantOptions.Price = productVariantOptions[j].Price ?? productVariantOptions[j].ProductVariant.Product.DiscountAmount;
                                //effect the price if discount exist for the product 
                                if (productVariantOptions[j].ProductVariant.Product.Discount > 0)
                                {
                                    var discountedValue = (variantOptions.Price * productVariantOptions[j].ProductVariant.Product.Discount / 100);
                                    variantOptions.Price = variantOptions.Price - discountedValue;
                                }

                                variantOptions.ProductVariant = new ProductVariant
                                {
                                    Product = new Product()
                                    {
                                        Id = productVariantOptions[j].ProductVariant.Product.Id
                                    },
                                    Key3 = variantOptions.ProductVariant.Key3,
                                    Key1 = productOption.ProductVariant.Key1,
                                    Key2 = productOption.ProductVariant.Key2,
                                    Value1 = key1Value,
                                    Value2 = key2Value
                                };
                                variantOptions.ImageUrl = productOption.ImageUrl ?? cartImage;
                                variantOptions.VariantQuantity = productInventory.Quantity;
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
        public JsonResult GetProductVariantProperties(string key1Value, string key2Value, string key3Value, string variantId)
        {
            ProductVariantOptions variantOptions = new ProductVariantOptions();
            InventoryLogic inventoryLogic = new InventoryLogic();
            try
            {
                if (!string.IsNullOrEmpty(key1Value) && !string.IsNullOrEmpty(key2Value) && !string.IsNullOrEmpty(key3Value) && !string.IsNullOrEmpty(variantId))
                {
                    int productVariantId = Convert.ToInt32(variantId);
                    ProductVariantOptionsLogic optionLogic = new ProductVariantOptionsLogic();
                    List<ProductVariantOptions> productVariantOptions = optionLogic.GetModelsBy(pv => pv.Product_Variant_Id == productVariantId && pv.Product_Option_Name.Contains(key1Value) && pv.Product_Option_Name.Contains(key2Value) && pv.Product_Option_Name.Contains(key3Value));

                    if (productVariantOptions.Count > 0)
                    {
                        var productOption = productVariantOptions.LastOrDefault();
                        if (productOption != null)
                        {
                            variantOptions.Id = productOption.Id;
                            var productInventory = inventoryLogic.GetModelBy(x => x.Product_Variant_Option_Id == variantOptions.Id);
                            variantOptions.Price = productOption.Price ?? productOption.ProductVariant.Product.DiscountAmount;
                            //effect the price if discount exist for the product 
                            if (productOption.ProductVariant.Product.Discount > 0)
                            {
                                var discountedValue = (variantOptions.Price * productOption.ProductVariant.Product.Discount / 100);
                                variantOptions.Price = variantOptions.Price - discountedValue;
                            }
                            variantOptions.ProductVariant = new ProductVariant
                            {
                                Product = productOption.ProductVariant.Product,
                                Key1 = productOption.ProductVariant.Key1,
                                Key2 = productOption.ProductVariant.Key2,
                                Key3 = productOption.ProductVariant.Key3,
                                Value1 = key1Value,
                                Value2 = key2Value,
                                Value3 = key3Value
                            };
                            string cartImage = productOption.ProductVariant.Product.ImageUrl.Replace('~', ' ');
                            variantOptions.ImageUrl = productOption.ImageUrl ?? cartImage.TrimStart();
                            variantOptions.VariantQuantity = productInventory.Quantity;

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
