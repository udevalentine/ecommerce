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
    public class WareHouseController : BaseController
    {
        WareHouseLogic wareHouseLogic = new WareHouseLogic();
        // GET: Admin/Vendor
        public ActionResult Index()
        {
            ProductViewModel productViewModel = new ProductViewModel();
            try
            {
                wareHouseLogic = new WareHouseLogic();
                productViewModel.WareHouses = wareHouseLogic.GetAll();
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
                wareHouseLogic = new WareHouseLogic();
                WareHouse wareHouse = wareHouseLogic.Create(product.WareHouse);
                if (wareHouse != null)
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
                wareHouseLogic = new WareHouseLogic();
                viewModel.WareHouse = wareHouseLogic.GetModelBy(v => v.Id == id);

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
                if (model.WareHouse != null)
                {
                    wareHouseLogic = new WareHouseLogic();
                    var wareHouse = wareHouseLogic.Modify(model.WareHouse);
                    if (wareHouse)
                    {
                        SetMessage("Operation Succesful", Message.Category.Information);
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
            return RedirectToAction("Index", "Vendor", new { area = "Admin" });
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
                wareHouseLogic = new WareHouseLogic();
                var deletedVednor = wareHouseLogic.Delete(s => s.Id == Id);
                if (deletedVednor)
                {
                    SetMessage("Operation Successful", Message.Category.Information);
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