using Core.Common.Dal;
using Store.Logic.ProductStore.Infustructure;
using Store.Logic.ProductStore.Models.ModifyModels;
using Store.Logic.ProductStore.Models.ViewModels;
using Store.Logic.ProductStore.Service.ModifyServices;
using Store.Logic.ProductStore.Service.ViewServices;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;


namespace StoreWeb.Controllers
{
    [Authorize(Roles = "docOrders")]
    public class OrderController : Controller
    {
        private readonly IUserViewService _userViewService;
        private readonly IOrderViewService _orderViewService;
        private readonly IOrderModifyService _orderModifyService;
        private readonly IOrderTypeViewService _orderTypeViewService;
        public OrderController(IObjectFactory objectFactory)
        {


            _userViewService = objectFactory.Create<IUserViewService>();
            _orderModifyService = objectFactory.Create<IOrderModifyService>();
            _orderViewService = objectFactory.Create<IOrderViewService>();
            _orderTypeViewService = objectFactory.Create<IOrderTypeViewService>();
        }

        public ViewResult Index()
        {
            ViewBag.OrderTypes = new SelectList(_orderTypeViewService.ViewAll(), "Id", "OrderTypeName");
            ViewBag.Users = new SelectList(_userViewService.ViewAll(), "Id", "UserName");
            return View(_orderViewService.ViewAll());
        }

        public ActionResult IndexSearch(DateTime? StartDate, DateTime? FinishDate, int? orderToUser, int? OrderTypeId)
        {

            var selected_orders = _orderViewService.ViewAll().Where(u => orderToUser == null || u.UserId == orderToUser).
                Where(u => OrderTypeId == null || u.OrderTypeId == OrderTypeId).Where
                (d => StartDate == null || d.OrderDate >= StartDate).Where(
                f => FinishDate == null || f.OrderDate <= FinishDate).OrderBy(o => o.OrderDate).ThenBy(n => n.OrderNumber);
            return PartialView(selected_orders);
        }



        [HttpGet]
        public ViewResult Edit(int? Id)
        {
            //bool load=  DbContext.Configuration.LazyLoadingEnabled
            ViewBag.OrderTypes = new SelectList(_orderTypeViewService.ViewAll(), "Id", "OrderTypeName");
            ViewBag.Users = new SelectList(_userViewService.ViewAll(), "Id", "UserName");
            OrderViewModel order = _orderViewService.ViewSingle(int.Parse(Id.ToString())); //Orders.Include(c => c.OrderType).Include(c=>c.OrderDetail.Select(p=>p.Product)).FirstOrDefault(c => c.Id == Id);


            return View(order ?? new OrderViewModel(DateTime.Now, GenerateOrderNumber(DateTime.Now)));
        }

        private string GenerateOrderNumber(DateTime date)
        {
            int countOrderToday = _orderViewService.ViewAll().Where(d => d.OrderDate == date.Date).Count() + 1;
            return DateTime.Now.ToString("ddMMyyyy") + "_" + countOrderToday.ToString();
        }

        [HttpPost]
        public ActionResult Edit(OrderViewModel order)
        {

            if (ModelState.IsValid)
            {
                OrderViewModel foundOrder = _orderViewService.ViewSingle(order.Id);
                if (foundOrder != null)
                {

                    _orderModifyService.Update(order.Id, new OrderModifyModel(order.OrderDate, order.OrderNumber,order.UserId,order.OrderTypeId, order.OrderAmount));
                    TempData["message"] = string.Format("Order \"{0}\"uploaded", order.OrderNumber);

                }
                else
                {
                    _orderModifyService.Add(new OrderModifyModel(order.OrderDate, order.OrderNumber, order.UserId, order.OrderTypeId, order.OrderAmount));
                    TempData["message"] = string.Format("Order\"{0}\"added", order.OrderNumber);
                }

                return RedirectToAction("Index");

            }
            else
            {
                return View(order);
            }

        }
        [HttpPost]
        public ViewResult Create()
        {
            return View("Edit", new OrderViewModel());
        }

        [HttpPost]
        public ActionResult Delete(int Id)
        {
            //Order foundOrder= _order.GetAll().FirstOrDefault(c => c.Id == Id);

            //if (foundOrder != null)
            //{
            //    TempData["message"] = string.Format("Order was deleted");
            //    _order.Delete(foundOrder);

            //    return RedirectToAction("Index");
            //}
            //else
            //{
            //    TempData["message"] = string.Format("User was not found");
            //}

            return RedirectToAction("Index");
        }


        public ViewResult CreateOrderFromCart()
        {
            return View("Edit", new OrderViewModel());
        }
    }

}


