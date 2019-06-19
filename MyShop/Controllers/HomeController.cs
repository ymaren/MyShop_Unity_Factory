using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using MyShop.Models;
using Microsoft.AspNet.Identity;

using Core.Common.Dal;
using Store.Logic.ProductStore.Service.ViewServices;
using Store.Logic.ProductStore.Infustructure;
using Store.Logic.ProductStore.Models.ViewModels;

namespace MyShop.Controllers
{
    public enum Order
    {
        
        not_sort ,
        first_cheap,
        first_expensive
    };

    public class HomeController : Controller
    {

        private readonly IUserViewService _userViewService;
        private readonly IProductViewService _productViewService;
        private readonly IProductGroupViewService _groupViewService;
        private readonly IProductCategoryViewService _categoryViewService;
        private int pageSize = 8;
        public HomeController(IObjectFactory objectFactory)
        {
            _userViewService = objectFactory.Create<IUserViewService>();
            _productViewService = objectFactory.Create<IProductViewService>();
            _groupViewService = objectFactory.Create<IProductGroupViewService>();
            _categoryViewService = objectFactory.Create<IProductCategoryViewService>();
           
        }
            
        public ActionResult Index(int? category, int? group, Order sort= Order.not_sort, int page = 1)
        {
            IEnumerable<ProductViewModel> products = _productViewService.ViewAll().ToList().Where(c => category == null || c.ProductGroup.ProductCategoryid == category).
                Where(g => group == null || g.ProductGroupId == group);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = products.Count() };
            switch (sort)
            {
                case Order.not_sort:
                    products = products.OrderBy(C => C.Id).Skip((page - 1) * pageSize).Take(pageSize);
                break;
                case Order.first_expensive:
                    products = products.OrderByDescending(C => C.Price).Skip((page - 1) * pageSize).Take(pageSize);
                break;
                case Order.first_cheap:
                    products = products.OrderBy(C => C.Price).Skip((page - 1) * pageSize).Take(pageSize);
                break;
               


            }
            

            IndexProductViewModel ivm = new IndexProductViewModel
            {
                PageInfo = pageInfo,
                Products = products,
                CurrentCategory = _categoryViewService.ViewAll().Where(c => c.Id == category).FirstOrDefault(),
                CurrentGroup = _groupViewService.ViewAll().Where(g => g.Id == group).FirstOrDefault(),
                CurrentOrder = sort
            };


            return View(ivm);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Menu()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                string email = User.Identity.GetUserName();
                UserViewModel u = _userViewService.ViewAll().ToList().FirstOrDefault(c => c.UserEmail == email);
                UserViewModel user = _userViewService.ViewSingle(u.Id);
                return PartialView(user.Credential);
            }
            return RedirectToAction("Login", "Account");
            
        }
        public PartialViewResult VerticalMenu()
        {
          return PartialView(_categoryViewService.ViewAll().ToList());
        }

    }
}