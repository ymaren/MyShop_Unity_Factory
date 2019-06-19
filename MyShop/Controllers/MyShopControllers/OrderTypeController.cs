

using Core.Common.Dal;
using Store.Logic.ProductStore.Infustructure;
using Store.Logic.ProductStore.Models.ModifyModels.Store.Logic.ProductStore.Models.ModifyModels;
using Store.Logic.ProductStore.Models.ViewModels;
using Store.Logic.ProductStore.Service.ModifyServices;
using Store.Logic.ProductStore.Service.ViewServices;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace StoreWeb.Controllers
{
    [Authorize(Roles = "dicOrderType")]
    public class OrderTypeController : Controller
    {
        private readonly IOrderTypeViewService _orderTypeViewService;
        private readonly IOrderTypeModifyService _orderTypeModifyService;
        public OrderTypeController(IObjectFactory objectFactory)
        {
            _orderTypeViewService = objectFactory.Create<IOrderTypeViewService>();
            _orderTypeModifyService = objectFactory.Create<IOrderTypeModifyService>();
        }

        //list
        public ViewResult Index()
        {
            return View(_orderTypeViewService.ViewAll());
        }

        
        [HttpGet]
        public ViewResult Edit(int? Id)
        {
            OrderTypeViewModel orderType = Id != null ? _orderTypeViewService.ViewSingle(Convert.ToInt32(Id.ToString())) : new OrderTypeViewModel();
            return View(orderType);
        }

        [HttpPost]
        public ActionResult Edit(OrderTypeViewModel orderType)
        {
            
            if (ModelState.IsValid)
            {
                
                OrderTypeViewModel foundOrderType = _orderTypeViewService.ViewAll().FirstOrDefault(c => c.Id == orderType.Id);
                if (foundOrderType!=null)
                {
                    _orderTypeModifyService.Update(foundOrderType.Id, new OrderTypeModifyModel(orderType.OrderTypeName));
                    TempData["message"] = string.Format("Order type \"{0}\"uploaded", orderType.OrderTypeName);                    
                }
                else
                {
                    _orderTypeModifyService.Add(new OrderTypeModifyModel(orderType.OrderTypeName));
                    TempData["message"] = string.Format("Order type\"{0}\"added", orderType.OrderTypeName);
                }
                
                return RedirectToAction("Index");
            }
            else
            {
                
                return View(orderType);
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

            OrderTypeViewModel foundOrderType = _orderTypeViewService.ViewAll().FirstOrDefault(c => c.Id == Id);

            if (foundOrderType != null)
            {
                _orderTypeModifyService.Delete(foundOrderType.Id);
                TempData["message"] = string.Format("Order type  was deleted");
                return RedirectToAction("Index");
            }
            else
            {
                TempData["message"] = string.Format("Order type was not found");
            }
            return RedirectToAction("Index");
        }
    }

}


   