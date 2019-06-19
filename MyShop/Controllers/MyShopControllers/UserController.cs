
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Core.Common.Dal;
using Store.Logic.ProductStore.Service.ViewServices;
using Store.Logic.ProductStore.Service.ModifyServices;
using Store.Logic.ProductStore.Infustructure;
using Store.Logic.ProductStore.Models.ViewModels;

using Store.Logic.ProductStore.Models.ModifyModels;
using MyShop.Models;

namespace StoreWeb.Controllers
{
    [Authorize(Roles = "dicUser")]
    public class UserController : Controller
    {

       

        private readonly IUserViewService _userViewService;
        private readonly IUserModifyService _userModifyService;
        private readonly IUserRoleViewService _userRoleViewService;
        private readonly ICredentionalViewService _credentionalViewService;


        public UserController(IObjectFactory objectFactory)
        {
            _userViewService = objectFactory.Create<IUserViewService>();
            _userModifyService = objectFactory.Create<IUserModifyService>();
            _userRoleViewService = objectFactory.Create<IUserRoleViewService>();
            _credentionalViewService = objectFactory.Create<ICredentionalViewService>();
        }

        // GET: User
        public ActionResult Index()
        {
            ViewBag.Roles = new SelectList(_userRoleViewService.ViewAll(), "Id", "UserRoleName");
            List<UserViewModel> users = _userViewService.ViewAll().ToList();
            return View(users);
        }
      
        public ActionResult IndexSearch(int? UserRoleId)
        {
            ViewBag.Roles = new SelectList(_userRoleViewService.ViewAll(), "UserRoleId", "UserRoleName");
            var users = _userViewService.ViewAll().Where(u => UserRoleId == null||  u.Id== UserRoleId);
            return PartialView(users.ToList());
        }

        public ActionResult RoleTemplateIndexSearch(int? UserRoleId)
        {
            ViewBag.Roles = new SelectList(_userRoleViewService.ViewAll(), "UserRoleId", "UserRoleName");
            var user = _userViewService.ViewAll().FirstOrDefault();
            if (UserRoleId != null)
            {
                var role = _userRoleViewService.ViewAll().First(r => UserRoleId == null || r.Id == UserRoleId);
                user.Credential.Clear();
                user.Credential = role.Credential;
            }
            return PartialView(new UserCredViewModel(user, _credentionalViewService.ViewAll().ToList()));
        }

        public ActionResult CreateChange(int? Id)
        {

            UserViewModel user = _userViewService.ViewSingle(Id??0);

            SelectList roles = new SelectList(_userRoleViewService.ViewAll(), "Id", "UserRoleName",user!=null?user.UserRoleId:0);
            ViewBag.Roles = roles;
            return View( new UserCredViewModel(user?? new UserViewModel(), _credentionalViewService.ViewAll().ToList()));
        }

        [HttpPost]
        public ActionResult CreateChange(UserCredViewModel userViewModel)
        {
            if (userViewModel.SelectedCredential != null)
            {
                foreach (int item in userViewModel.SelectedCredential)
                {
                    userViewModel.user.Credential.Add(_credentionalViewService.ViewAll().FirstOrDefault(c => c.Id == item));
                }
            }

            
            if (ModelState.IsValid)
            {
                UserViewModel foundUser = _userViewService.ViewSingle(userViewModel.user.Id);
               
                if (foundUser!=null)
                {
                   
                    

                    _userModifyService.Update(foundUser.Id,new UserModifyModel(userViewModel.user.UserEmail, userViewModel.user.UserPassword,
                        userViewModel.user.UserName, userViewModel.user.UserCountry, userViewModel.user.UserAddress, userViewModel.user.UserRoleId
                        ));
                    TempData["message"] = string.Format("User \"{0}\" uploaded", userViewModel.user.UserEmail);
                }
                else
                {
                    _userModifyService.Add(new UserModifyModel(userViewModel.user.UserEmail, userViewModel.user.UserPassword,
                        userViewModel.user.UserName, userViewModel.user.UserCountry, userViewModel.user.UserAddress, userViewModel.user.UserRoleId
                        ));
                    TempData["message"] = string.Format("User\"{0}\" uploaded ", userViewModel.user.UserEmail);
                   
                }
                
                return RedirectToAction("Index");
            }
            else
            {
                SelectList roles = new SelectList(_userRoleViewService.ViewAll(), "Id", "UserRoleName", userViewModel.user != null ? userViewModel.user.UserRoleId : 0);
                ViewBag.Roles = roles;
                // Что-то не так со значениями данных
                return View(userViewModel);
            }
        }

        [HttpPost]
        public ActionResult Delete(int UserId)
        {
            UserViewModel foundUser = _userViewService.ViewSingle(UserId); ;

            if (foundUser != null)
            {

                TempData["message"] = string.Format("User  was deleted");
                _userModifyService.Delete(foundUser.Id);
                
                return RedirectToAction("Index");
            }
            else
            {
                TempData["message"] = string.Format("User was not found");
            }

            return RedirectToAction("Index");
        }


}
}