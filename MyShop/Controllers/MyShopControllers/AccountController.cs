using Core.Common.Dal;
using MyShop.Models;
using Store.Logic.ProductStore.Infustructure;
using Store.Logic.ProductStore.Models.ModifyModels;
using Store.Logic.ProductStore.Models.ViewModels;
using Store.Logic.ProductStore.Service.ModifyServices;
using Store.Logic.ProductStore.Service.ViewServices;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;


namespace MyShop.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {

        private readonly IUserViewService _userViewService;
        private readonly IUserModifyService _userModifyService;
        public AccountController (IObjectFactory objectFactory)
        {

            _userViewService = objectFactory.Create<IUserViewService>();
            _userModifyService = objectFactory.Create<IUserModifyService>();
        }

        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                string result = "Вы не авторизованы";
            }

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LogViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, true);
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                       if (HttpContext.User.IsInRole("Admin"))
                        return RedirectToAction("Menu", "Home");
                       else
                       return RedirectToAction("Index", "Cart");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный пароль или логин");
                }
            }
            return View(model);
        }
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult Register(UserViewModel newUser)
        {
            if (ModelState.IsValid)
            {
                 newUser.UserRoleId = 3;
                UserViewModel foundUser = _userViewService.ViewAll().FirstOrDefault(c => c.Id == newUser.Id);
                if (foundUser != null)
                {
                    _userModifyService.Update(newUser.Id, new UserModifyModel(newUser.UserEmail, newUser.UserPassword,
                        newUser.UserName, newUser.UserCountry, newUser.UserAddress, newUser.UserRoleId
                        ));
                    FormsAuthentication.SignOut();
                    FormsAuthentication.SetAuthCookie(newUser.UserEmail, true);
                    
                    return RedirectToAction("Index","Home");
                }
                else
                {
                    _userModifyService.Add(new UserModifyModel(newUser.UserEmail, newUser.UserPassword,
                        newUser.UserName, newUser.UserCountry, newUser.UserAddress, newUser.UserRoleId
                        ));
                    
                    return RedirectToAction("Login");
                }
                
            }
            return View(newUser);
        }
        public ActionResult Register(string returnUrl)
        {
            
            ViewBag.LastURL = returnUrl;
            if (HttpContext.User.Identity.IsAuthenticated)
                return View(_userViewService.ViewAll().FirstOrDefault(c => c.UserEmail == HttpContext.User.Identity.Name));


            return View(new UserViewModel());
        }


        private bool ValidateUser(string login, string password)
        {
            bool isValid = false;
                      
                try
                {
                UserViewModel user = _userViewService.ViewAll().FirstOrDefault(c=>c.UserEmail==login && c.UserPassword==password); 

                    if (user != null)
                    {
                        isValid = true;
                    }
                }
                catch
                {
                    isValid = false;
                }
            
            return isValid;
        }
    }
}