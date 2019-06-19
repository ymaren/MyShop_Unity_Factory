using Core.Common.Dal;
using Store.Logic.ProductStore.Infustructure;
using Store.Logic.ProductStore.Models.ModifyModels;
using Store.Logic.ProductStore.Models.ViewModels;
using Store.Logic.ProductStore.Service.ModifyServices;
using Store.Logic.ProductStore.Service.ViewServices;
using System;
using System.Linq;
using System.Web.Mvc;

namespace StoreWeb.Controllers
{
    [Authorize(Roles = "dicCategories")]
    public class CategoryController : Controller
    {
        private readonly IProductCategoryViewService _categoryViewService;
        private readonly IProductCategoryModifyService _categoryModifyService;
        public CategoryController(IObjectFactory objectFactory)
        {
            _categoryViewService = objectFactory.Create<IProductCategoryViewService>();
            _categoryModifyService = objectFactory.Create<IProductCategoryModifyService>();
        }
        [HttpGet]
        public ViewResult Index()
        {
            return View(_categoryViewService.ViewAll());
        }

        [HttpGet]
        public ViewResult Edit(int? Id)
        {
            ProductCategoryViewModel prodcategory = Id!=null? _categoryViewService.ViewSingle (Convert.ToInt32(Id.ToString())) : new ProductCategoryViewModel();
            return View(prodcategory);
        }

        [HttpPost]
        public ActionResult Edit(ProductCategoryViewModel category)
        {

            if (ModelState.IsValid)
            {
                ProductCategoryViewModel foundCategory = _categoryViewService.ViewAll().FirstOrDefault(c => c.Id == category.Id);

                if (foundCategory != null)
                {

                    TempData["message"] = string.Format("Category \"{0}\"uploaded", category.CategoryName);
                    _categoryModifyService.Update(category.Id, new ProductCategoryModifyModel(category.CategoryName, category.CategoryDescription));

                }
                else
                {
                    _categoryModifyService.Add(new ProductCategoryModifyModel(category.CategoryName, category.CategoryDescription));
                    TempData["message"] = string.Format("Category\"{0}\"added", category.CategoryName);
                }

                return RedirectToAction("Index");
            }
            else
            {
                return View(category);
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

            ProductCategoryViewModel foundCategory = _categoryViewService.ViewAll().FirstOrDefault(c => c.Id == Id);

            if (foundCategory != null)
            {

                TempData["message"] = string.Format("Product category  was deleted");
                _categoryModifyService.Delete(Id);
                return RedirectToAction("Index");
            }
            else
            {
                TempData["message"] = string.Format("Product category was not found");
            }

            return RedirectToAction("Index");
        }
    }

}


