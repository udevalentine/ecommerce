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
    public class UserController : BaseController
    {
        // GET: Admin/User
        public ActionResult ViewRole()
        {
            UserViewModels userViewModel = new UserViewModels();
            try
            {
                RoleLogic roleLogic = new RoleLogic();
                userViewModel.Roles = roleLogic.GetAll();
            }
            catch (Exception)
            {
                
                throw;
            }
            return View(userViewModel);
        }
        public ActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateRole(UserViewModels model)
        {
            try
            {
                RoleLogic roleLogic = new RoleLogic();
                Role role = roleLogic.Create(model.Role);
                if (role != null)
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



        public ActionResult EditRole(string ProNo)
        {
            UserViewModels viewModel = new UserViewModels();
            try
            {
                int id = Convert.ToInt32(ProNo);
                if (ProNo == null)
                {
                    throw new ArgumentException("Role was not Found");
                }
                RoleLogic roleLogic = new RoleLogic();
                viewModel.Role = roleLogic.GetModelBy(v => v.Id == id);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult EditRole(UserViewModels model)
        {
            try
            {
                if (model.Role != null)
                {
                    RoleLogic roleLogic = new RoleLogic();
                    var role = roleLogic.Modify(model.Role);
                    if (role)
                    {
                        SetMessage("Operation Succesful", Message.Category.Information);
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }

            return RedirectToAction("ViewRole", "User", new { area = "Admin" });
        }

        public ActionResult DeleteRole(string ProNo)
        {
            try
            {
                int Id = Convert.ToInt32(ProNo);
                if (ProNo == null)
                {
                    throw new ArgumentException("Unit Was not found to be deleted");
                }
                RoleLogic roleLogic = new RoleLogic();
                var deletedUnit = roleLogic.Delete(s => s.Id == Id);
                if (deletedUnit)
                {
                    SetMessage("Operation Successful", Message.Category.Information);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return RedirectToAction("ViewRole", "User", new { area = "Admin" });
        }


    }
}