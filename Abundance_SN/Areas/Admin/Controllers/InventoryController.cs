using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Abundance_SN.Business;
using Abundance_SN.Controllers;
using Abundance_SN.Model.Model;
using Abundance_SN.Models;

namespace Abundance_SN.Areas.Admin.Controllers
{
     [Authorize(Roles = "Admin,SuperAdmin")]
    public class InventoryController : BaseController
    {
        private InventoryLogic inventoriesLogic;
        // GET: Admin/Inventory
        public ActionResult Index()
        {
            ProductViewModel productViewModel = new ProductViewModel();
            try
            {
                inventoriesLogic = new InventoryLogic();
                productViewModel.Inventorieses = inventoriesLogic.GetAll();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return View(productViewModel);
        }

        public ActionResult Create()
        {
            ViewBag.ProductId = Utility.PopulateProductselectListItem();
            ViewBag.ProductVariantOption = new SelectList(new List<ProductVariantOptions>(), Utility.ID, Utility.NAME);
            return View();
        }

        [HttpPost]
        public ActionResult Create(ProductViewModel product)
        {
            try
            {
                inventoriesLogic = new InventoryLogic();

                var checkInventory = inventoriesLogic.GetModelBy(i => i.Product_Id == product.Inventory.Product.Id && i.Product_Variant_Option_Id == product.Inventory.ProductVariantOptions.Id);
                if (checkInventory == null)
                {
                    Inventory inventories = inventoriesLogic.Create(product.Inventory);
                    if (inventories != null)
                    {
                        SetMessage("Operation Successful", Message.Category.Information);
                     
                    }
                }
                else
                {
                    SetMessage("Product Already exist Goto Manage Inventory to continue",Message.Category.Error);
                }
               
            }
            catch (Exception ex)
            {

                throw ex;
            }
            ModelState.Clear();
            ViewBag.ProductId = Utility.PopulateProductselectListItem();
            ViewBag.ProductVariantOption = new SelectList(new List<ProductVariantOptions>(), Utility.ID, Utility.NAME);
            return View();
        }



        public ActionResult Edit(string ProNo)
        {
            ProductViewModel viewModel = new ProductViewModel();
            try
            {
                int id = Convert.ToInt32(ProNo);
                if (ProNo == null)
                {
                    throw new ArgumentException("Vendor was not Found");
                }
                inventoriesLogic = new InventoryLogic();
                ProductVariantOptionsLogic productVariantOptionsLogic = new ProductVariantOptionsLogic();
                viewModel.Inventory = inventoriesLogic.GetModelBy(v => v.Id == id);
                string NAME = "ProductOptionName";
                if (viewModel.Inventory != null)
                {
                    ViewBag.ProductId = new SelectList(Utility.PopulateProductselectListItem(), Utility.VALUE, Utility.TEXT, viewModel.Inventory.Id);
                    ViewBag.ProductVariantOption = new SelectList(productVariantOptionsLogic.GetModelsBy(p => p.PRODUCT_VARIANT.Product_Id == viewModel.Inventory.Product.Id), Utility.ID, NAME,viewModel.Inventory.ProductVariantOptions.Id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(ProductViewModel model)
        {
            try
            {
                if (model.Inventory != null && model.ProductVariantOptions != null)
                {
                    inventoriesLogic = new InventoryLogic();
                    var inventory = inventoriesLogic.Modify(model.Inventory,model.ProductVariantOptions);
                    if (inventory)
                    {
                        SetMessage("Operation Succesful", Message.Category.Information);
                    }
                }


            }
            catch (Exception)
            {

                throw;
            }
            return RedirectToAction("Index", "Inventory", new { area = "Admin" });
        }

        public ActionResult Delete(string ProNo)
        {
            try
            {
                int Id = Convert.ToInt32(ProNo);
                if (ProNo == null)
                {
                    throw new ArgumentException("Vendor Was not found to be deleted");
                }
                inventoriesLogic = new InventoryLogic();
                var deletedVednor = inventoriesLogic.Delete(s => s.Id == Id);
                if (deletedVednor)
                {
                    SetMessage("Operation Successful", Message.Category.Information);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return RedirectToAction("Index", "Inventory", new { area = "Admin" });
        }

         public JsonResult GetProductVariantOption(string productId)
         {    
             List<ProductVariantOptions> productVariantOptions = new List<ProductVariantOptions>();
             try
             {
                 if (!string.IsNullOrEmpty(productId))
                 {
                     ProductVariantOptionsLogic productVariantOptionLogic = new ProductVariantOptionsLogic();
                     long prodcutOptionId = Convert.ToInt64(productId);
                     productVariantOptions = productVariantOptionLogic.GetModelsBy(s => s.PRODUCT_VARIANT.Product_Id == prodcutOptionId);
                  
                 }
             }
             catch (Exception ex)
             {
                 
                 throw ex;
             }
             return Json(productVariantOptions, JsonRequestBehavior.AllowGet);
         }
    }
}