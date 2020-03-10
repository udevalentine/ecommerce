using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Abundance_SN.Business;
using Abundance_SN.Controllers;
using Abundance_SN.Model.Entity;
using Abundance_SN.Model.Model;
using Abundance_SN.Models;

namespace Abundance_SN.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class VendorController : BaseController
    {
        VendorLogic vendorsLogic = new VendorLogic();
        // GET: Admin/Vendor
        public ActionResult Index()
        {
            ProductViewModel productViewModel = new ProductViewModel();
            try
            {
                vendorsLogic = new VendorLogic();
                productViewModel.VendorList = vendorsLogic.GetAll();
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            return View(productViewModel);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ProductViewModel product)
        {
            try
            { 
                vendorsLogic = new VendorLogic();
                Vendors vendors = vendorsLogic.Create(product.Vendors);
                if (vendors != null)
                {
                   SetMessage("Operation Successful",Message.Category.Information); 
                }
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            ModelState.Clear();
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
                  vendorsLogic = new VendorLogic();
                  viewModel.Vendors = vendorsLogic.GetModelBy(v => v.Id == id);

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
                if (model.Vendors != null)
                {
                   vendorsLogic = new VendorLogic();
                    var editedVendor = vendorsLogic.Modify(model.Vendors);
                    if (editedVendor)
                    {
                        SetMessage("Operation Succesful", Message.Category.Information);
                    }
                }

            }
            catch (Exception)
            {
                    
                throw;
            }
            return RedirectToAction("Index", "Vendor", new {area = "Admin"});
        }

        public ActionResult Delete(string id)
        {
            try
            {
                int Id = Convert.ToInt32(id);
                if (id == null)
                {
                    throw new ArgumentException("Vendor Was not found to be deleted");
                }
                vendorsLogic = new VendorLogic();
                var deletedVednor = vendorsLogic.Delete(s => s.Id == Id);
                if (deletedVednor)
                {
                    SetMessage("Operation Successful",Message.Category.Information);
                }
            }
            catch (Exception ex)
            {
                    
                throw ex;
            }
            return RedirectToAction("Index", "Vendor", new { area = "Admin" });
        }
    }
}