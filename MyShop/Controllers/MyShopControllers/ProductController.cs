
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Data.Entity;
using Core.Common.Dal;
using Store.Logic.ProductStore.Infustructure;
using Store.Logic.ProductStore.Service.ModifyServices;
using Store.Logic.ProductStore.Service.ViewServices;
using Store.Logic.ProductStore.Models.ViewModels;
using Store.Logic.ProductStore.Models.ModifyModels;

namespace StoreWeb.Controllers
{
    [Authorize(Roles = "dicProduct")]
    public class ProductController : Controller
    {

      
        private readonly IProductViewService   _productViewService;
        private readonly IProductModifyService _productModifyService;
        private readonly IProductGroupViewService _groupViewService;
        public ProductController(IObjectFactory objectFactory)
        {
            _productViewService = objectFactory.Create<IProductViewService>();
            _productModifyService = objectFactory.Create<IProductModifyService>();
            _groupViewService = objectFactory.Create<IProductGroupViewService>();
        }

        public ViewResult Index()
        {
           return View(_productViewService.ViewAll());
        }
        [HttpGet]
        public ViewResult Edit(int? Id)
        {
            SelectList groups = new SelectList(_groupViewService.ViewAll(), "Id", "GroupName");
            ViewBag.Groups = groups;
            ProductViewModel product = Id != null ? _productViewService.ViewSingle(Convert.ToInt32(Id.ToString())) : new ProductViewModel();
            return View(product);
        }
        [HttpGet]
        public string Load(int? Id)
        {
            SelectList groups = new SelectList(_groupViewService.ViewAll(), "Id", "GroupName");
            ViewBag.Groups = groups;
            ProductViewModel product = _productViewService.ViewAll().FirstOrDefault(p => p.Id == Id);
            return product.Name;
        }

        [HttpPost]
        public ActionResult Edit(ProductViewModel product, HttpPostedFileBase upload)
        {
            
            if (ModelState.IsValid)
            {
                ProductViewModel foundProduct = _productViewService.ViewAll().FirstOrDefault(c => c.Id == product.Id);
                if (foundProduct!=null)
                {
                     TempData["message"] = string.Format("Product \"{0}\" saved", product.Name);
                    _productModifyService.Update(product.Id, new ProductModifyModel( product.ProductCode,
                        product.Name,
                        product.Price,
                        product.Description,
                        product.ProductGroupId)
                        );
                    
                }
                else
                {
                    _productModifyService.Add(
                        new ProductModifyModel(product.ProductCode,
                        product.Name,
                        product.Price,
                        product.Description,
                        product.ProductGroupId)
                        );                    
                    TempData["message"] = string.Format("Product\"{0}\"saved", product.Name);
                }
                if (upload != null)
                {
                    // получаем имя файла
                    string fileName = System.IO.Path.GetFileName(upload.FileName);
                    // сохраняем файл в папку Pictures в проекте
                    upload.SaveAs(Server.MapPath("~/Pictures/"+product.Id+".jpg"));
                    TempData["message"] = string.Format("Foto add\"{0}\"", product.Name);
                    SelectList groups = new SelectList(_groupViewService.ViewAll(), "Id", "GroupName");
                    ViewBag.Groups = groups;
                    return RedirectToAction("Edit",new {@id= product.Id }); 
                }
                
                return RedirectToAction("Index");
            }
            else
            {
                SelectList groups = new SelectList(_groupViewService.ViewAll(), "Id", "GroupName");
                ViewBag.Groups = groups;
                return View(product);
            }
          
        }

        
        public ViewResult Create()
        {
            SelectList groups = new SelectList(_groupViewService.ViewAll(), "Id", "GroupName");
            ViewBag.Groups = groups;
            return View("Edit");
        }

        [HttpPost]
        public ActionResult Delete(int Id)
        {
            ProductViewModel foundProduct = _productViewService.ViewAll().FirstOrDefault(c => c.Id == Id);

            if (foundProduct != null)
            {

                TempData["message"] = string.Format("Product   was deleted");
                _productModifyService.Delete(Id);
                
                return RedirectToAction("Index");
            }
            else
            {
                TempData["message"] = string.Format("Product was not found");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public string Upload(HttpPostedFileBase upload, int? Id)
        {
            if (upload != null)
            {
                
                string fileName = System.IO.Path.GetFileName(upload.FileName);
               
                upload.SaveAs(Server.MapPath("~/Pictures/" + fileName));
            }
            return string.Empty;
        }
    }

}


   