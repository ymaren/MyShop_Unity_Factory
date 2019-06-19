
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Core.Common.Dal;
using MyShop.Models;
using Store.Logic.ProductStore.Infustructure;
using Store.Logic.ProductStore.Models.ModifyModels;
using Store.Logic.ProductStore.Models.ViewModels;
using Store.Logic.ProductStore.Service.ModifyServices;
using Store.Logic.ProductStore.Service.ViewServices;

namespace StoreWeb.Controllers
{
    [Authorize(Roles = "dicUsersRole")]
    public class RolesController : Controller
    {
        private readonly IUserRoleViewService _userRoleViewService;
        private readonly IUserRoleModifyService _userRoleModifyService;
        private readonly ICredentionalViewService _credentionalViewService;
        
        public RolesController(IObjectFactory objectFactory)
        {
            _userRoleViewService = objectFactory.Create<IUserRoleViewService>();
            _userRoleModifyService = objectFactory.Create<IUserRoleModifyService>();
            _credentionalViewService = objectFactory.Create<ICredentionalViewService>();
        }

        
        public ViewResult Index()
        {
            return View(_userRoleViewService.ViewAll());
        }


        [HttpGet]
        public ViewResult Edit(int? Id)
        {
            UserRoleViewModel userRole = Id != null ? _userRoleViewService.ViewSingle(Convert.ToInt32(Id.ToString())) : new UserRoleViewModel();
            return View(new RoleViewModel(userRole, _credentionalViewService.ViewAll().ToList()));
        }

        [HttpPost]
        public ActionResult Edit(RoleViewModel role)
        {

            if (role.SelectedCredential != null)
            {
                List<CredentionalViewModel> list = _credentionalViewService.ViewAll().ToList();
                foreach (int item in role.SelectedCredential)
                {                    
                    role.userRole.Credential.Add(list.FirstOrDefault(c => c.Id == item));
                }
            }
            
            if (ModelState.IsValid)
            {
                UserRoleViewModel foundRole = _userRoleViewService.ViewSingle(role.userRole.Id);
                if (foundRole!=null)
                {
                    foundRole.UserRoleName = role.userRole.UserRoleName;
                    _userRoleModifyService.Update(role.userRole.Id,new UserRoleModifyModel(role.userRole.UserRoleName));
                    TempData["message"] = string.Format("Role \"{0}\"uploaded", role.userRole.UserRoleName);
                    
                }
                else
                {
                    _userRoleModifyService.Add(new UserRoleModifyModel (role.userRole.UserRoleName));
                    TempData["message"] = string.Format("Role\"{0}\"added", role.userRole.UserRoleName);
                    
                }

                return RedirectToAction("Index");
            }
            else
            {

                return View(role);
            }

        }

        [HttpPost]
        public ViewResult Create()
        {
            return View("Edit");
        }

        [HttpPost]
        public ActionResult Delete(int Id)
        {
            
            UserRoleViewModel foundUserRole = _userRoleViewService.ViewSingle(Id);

            if (foundUserRole != null)
            {
                _userRoleModifyService.Delete(foundUserRole.Id);
                TempData["message"] = string.Format("Role  was deleted");
                return RedirectToAction("Index");
            }
            else
            {
                TempData["message"] = string.Format("Role was not found");
            }
            return RedirectToAction("Index");


        }
    }

}


