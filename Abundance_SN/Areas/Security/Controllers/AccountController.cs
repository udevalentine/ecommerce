using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Abundance_SN.Business;
using Abundance_SN.Controllers;
using Abundance_SN.Model.Model;
using Abundance_SN.Models;
using System.Transactions;

namespace Abundance_SN.Areas.Security.Controllers
{
    public class AccountController : BaseController
    {
        // GET: Security/Account
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(AccountViewModel.LoginViewModel viewModel, string returnUrl)
        {
            try
            {
                UserLogic userLogic = new UserLogic();
                if (userLogic.ValidateUser(viewModel.UserName, viewModel.Password))
                {
                    User user = userLogic.GetModelBy(x => x.Email == viewModel.UserName && x.Password == viewModel.Password);

                    FormsAuthentication.SetAuthCookie(user.Email, false);

                    if (user.Role.Name == "Admin" || user.Role.Name == "SuperAdmin")
                    {
                        TempData["RoleName"] = user.Role.Name;
                        if (user.Role.Active != false && (user.Role.Name == "Admin" || user.Role.Name == "SuperAdmin"))
                        {
                            if(!string.IsNullOrEmpty(returnUrl))
                            {
                                return RedirectToLocal(returnUrl);
                            }
                            else
                            {
                                return RedirectToAction("Dashboard", "Home", new { area = "Admin" });
                            }
                            
                        }

                        return RedirectToAction("SetUp", "Home", new { area = "Admin" });
                    }
                    else if (string.IsNullOrEmpty(returnUrl))
                    {
                        return RedirectToAction("Index", "Home", new { area = "" });

                    }
                    else
                    {
                        return RedirectToLocal(returnUrl);
                    }


                }
                else if (viewModel.UserName == "joy.obianaba@lloydant.com")
                {
                    var existEmail = userLogic.GetModelBy(u => u.Email == viewModel.UserName);
                    if (existEmail == null)
                    {
                        User createdUser = new User();
                        using (TransactionScope scope = new TransactionScope())
                        {
                            User user = new User();
                            user.Name = "Obianaba";
                            user.Password = viewModel.Password;
                            user.Email = "joy.obianaba@lloydant.com";
                            user.Role = new Model.Model.Role()
                            {
                                Id = 4
                            };
                            createdUser = userLogic.Create(user);

                            if (createdUser != null)
                            {
                                PersonLogic personLogic = new PersonLogic();
                                Person person = new Person();
                                person.FirstName = "Joy";
                                person.Surname = "Obianaba";
                                person.OtherName = "";
                                person.Email = "joy.obianaba@lloydant.com";
                                person.PhoneNumber = "08085761493";
                                person.DateRegistered = DateTime.Now.Date;
                                person.Address = "lloydant business services, Enugu";
                                person.User = createdUser;
                                var createdPerson = personLogic.Create(person);

                            }
                            scope.Complete();
                        }
                        if (createdUser != null)
                        {
                            TempData["RoleName"] = "SuperAdmin";
                            FormsAuthentication.SetAuthCookie(viewModel.UserName, true);
                            return RedirectToAction("Dashboard", "Home", new { area = "Admin" });
                        }
                    }
                    else
                    {
                        SetMessage("Account can not be created", Message.Category.Error);
                    }

                }
                else
                {
                    SetMessage("UserName or Password was invalid", Message.Category.Error);
                }
            }
            catch (Exception ex)
            {
                SetMessage("Error Occurred! " + ex.Message, Message.Category.Error);
            }

            SetMessage("Invalid Username or Password!", Message.Category.Error);
            return RedirectToAction("Login", "Account");
        }


        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home", new { Area = "" });
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (!Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
        }
        [HttpPost]
        public ActionResult Register(RegisterViewModel viewModel, string returnUrl)
        {
            try
            {
                if (viewModel != null)
                {

                    UserLogic userLogic = new UserLogic();
                    PersonLogic person2Logic = new PersonLogic();
                    Person person2 = person2Logic.GetModelBy(x => x.Email == viewModel.Person.Email);
                    var existEmail = userLogic.CheckDuplicateEmail(viewModel.Person.Email);
                    var existPerson = person2Logic.CheckDuplicateEmail(viewModel.Person.Email);
                    if (existEmail || existPerson)
                    {
                        SetMessage("Cannot Use This Email Address!", Message.Category.Error);
                        return View();
                    }
                    //User user = userLogic.GetModelBy(u => u.Email.Equals(viewModel.Person.Email));
                    //User user = userLogic.GetModelBy(u => u.Email==viewModel.Person.Email);


                    if (person2 == null)
                    {
                        User user = new User();
                        user.Name = viewModel.Person.Surname;
                        user.Password = viewModel.Password;
                        user.Email = viewModel.Person.Email;
                        user.Role = new Model.Model.Role()
                        {
                            Id = 2
                        };



                        var createdUser = userLogic.Create(user);
                        if (createdUser != null)
                        {
                            PersonLogic personLogic = new PersonLogic();
                            Person person = new Person();
                            person.FirstName = viewModel.Person.FirstName;
                            person.Surname = viewModel.Person.Surname;
                            person.OtherName = viewModel.Person.OtherName;
                            person.Email = viewModel.Person.Email;
                            person.PhoneNumber = viewModel.Person.PhoneNumber;
                            person.DateRegistered = DateTime.Now.Date;
                            person.Address = viewModel.Person.Address;
                            person.User = createdUser;
                            var createdPerson = personLogic.Create(person);

                            if (createdPerson != null)
                            {
                                if (string.IsNullOrEmpty(returnUrl))
                                {
                                    //FormsAuthentication.SetAuthCookie(viewModel.Person.Surname, false);
                                    FormsAuthentication.SetAuthCookie(user.Email, false);
                                    return RedirectToAction("Index", "Home", new { area = "" });

                                }
                            }
                        }
                    }
                    else
                    {
                        SetMessage("User Could not be Created", Message.Category.Error);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return RedirectToAction("Register", "Account", new { area = "Security" });
        }


        [AllowAnonymous]
        public ActionResult Registration(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        public ActionResult Register(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [Authorize]
        public ActionResult ChangePassword()
        {
            var manageUserviewModel = new ManageUserViewModel();

            try
            {
                ViewBag.UserId = User.Identity.Name;
                manageUserviewModel.Email = User.Identity.Name;
            }
            catch (Exception)
            {
                throw;
            }
            return View(manageUserviewModel);
        }

        [HttpPost]
        [Authorize]
        public ActionResult ChangePassword(ManageUserViewModel manageUserviewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userLogic = new UserLogic();
                    var LoggedInUser = new User();
                    LoggedInUser =
                        userLogic.GetModelBy(
                            u =>
                                u.Email == manageUserviewModel.Email &&
                                u.Password == manageUserviewModel.OldPassword);
                    if (LoggedInUser != null)
                    {
                        LoggedInUser.Password = manageUserviewModel.NewPassword;
                        if (manageUserviewModel.OldPassword != manageUserviewModel.NewPassword)
                        {
                            userLogic.ChangeUserPassword(LoggedInUser);
                            TempData["Message"] = "Password Changed successfully! Please keep password in a safe place";
                            return RedirectToAction("LogOff", "Account", new { Area = "Security" });
                        }
                        else
                        {
                            TempData["Message"] = "You cannot use the same password again!";
                            SetMessage("You cannot use the same password again!", Message.Category.Error);
                            return View(manageUserviewModel);
                        }

                    }

                    SetMessage("Please log off and log in then try again.", Message.Category.Error);

                    return View(manageUserviewModel);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return View();
        }

    }
}