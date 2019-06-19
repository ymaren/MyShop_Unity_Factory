

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Data.Entity;
using Core.Common.Dal;
using Store.Logic.ProductStore.Service.ViewServices;
using Store.Logic.ProductStore.Service.ModifyServices;
using Store.Logic.ProductStore.Infustructure;
using Store.Logic.ProductStore.Models.ViewModels;
using Store.Logic.ProductStore.Models.ModifyModels;

namespace StoreWeb.Controllers
{
    [Authorize(Roles = "dicProductGroup")]
    public class GroupController : Controller
    {
        
        private readonly IProductCategoryViewService _categoryViewService;
        private readonly IProductGroupViewService _groupViewService;
        private readonly IProductGroupModifyService _groupModifyService;

        public GroupController(IObjectFactory objectFactory)
        {
            _categoryViewService = objectFactory.Create<IProductCategoryViewService>();
            _groupViewService= objectFactory.Create<IProductGroupViewService>();
            _groupModifyService = objectFactory.Create<IProductGroupModifyService>();

        }

        [HttpGet]
        public ViewResult Index()
        {
           return View(_groupViewService.ViewAll());
        }

        [HttpGet]
        public ViewResult Edit(int? Id)
        {
            SelectList categorylist = new SelectList(_categoryViewService.ViewAll(), "Id", "CategoryName");
            ViewBag.Categories = categorylist;
            ProductGroupViewModel group = Id != null ? _groupViewService.ViewSingle(Convert.ToInt32(Id.ToString())) : new ProductGroupViewModel();
            
            return View(group);
        }

        [HttpPost]
        public ActionResult Edit(ProductGroupViewModel group)
        {
            
            if (ModelState.IsValid)
            {
                ProductGroupViewModel foundGroup = _groupViewService.ViewAll().FirstOrDefault(c => c.Id == group.Id);
                if (foundGroup!=null)
                {
                    
                    TempData["message"] = string.Format("Group \"{0}\"uploaded", group.GroupName);
                    _groupModifyService.Update(foundGroup.Id, 
                        new ProductGroupModifyModel(group.GroupName, group.GroupDescription,group.ProductCategoryid));
                }
                else
                {
                    _groupModifyService.Add(new ProductGroupModifyModel(group.GroupName, group.GroupDescription, group.ProductCategoryid));
                    TempData["message"] = string.Format("Group\"{0}\"added", group.GroupName);
                }
                
               return RedirectToAction("Index");
            }
            else
            {
                
                return View(group);
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

            ProductGroupViewModel foundGroup = _groupViewService.ViewAll().FirstOrDefault(c => c.Id == Id);

            if (foundGroup!=null)
            {
                _groupModifyService.Delete(foundGroup.Id);
                TempData["message"] = string.Format("Product group  was deleted");
                return RedirectToAction("Index");
            }
            else
            { 
                TempData["message"] = string.Format("Product group was not found");
            }
            return RedirectToAction("Index");
        }
    }

}


   