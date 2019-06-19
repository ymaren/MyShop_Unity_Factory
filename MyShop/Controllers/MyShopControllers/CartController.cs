
using Core.Common.Dal;
using MyShop.Models;
using Store.Logic.Entity;
using Store.Logic.ProductStore.Infustructure;
using Store.Logic.ProductStore.Models.ModifyModels;
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
    public class CartController : Controller
    {
        Cart cart;
        UserViewModel currentUser;
        private readonly IProductViewService _productViewService;
        private readonly IOrderTypeViewService _orderTypeViewService;
        private readonly IOrderViewService _orderViewService;
        private readonly IOrderModifyService _orderModifyService;
        private readonly IUserViewService _userViewService;
        private readonly IUserModifyService _userModifyService;
        public CartController(IObjectFactory objectFactory)
        {

            _productViewService = objectFactory.Create<IProductViewService>();
            _orderTypeViewService = objectFactory.Create<IOrderTypeViewService>();
            _orderModifyService = objectFactory.Create<IOrderModifyService>();
            _orderViewService = objectFactory.Create<IOrderViewService>();
            _userViewService = objectFactory.Create<IUserViewService>();
            _userModifyService = objectFactory.Create<IUserModifyService>();
            cart = new Cart();
            currentUser = new UserViewModel();



        }

        public RedirectToRouteResult AddToCart(Cart cart, int? Id, string returnUrl)
        {

            ProductViewModel prod = _productViewService.ViewAll().FirstOrDefault(p => p.Id == Id);

            if (prod != null)
            {
                cart.AddItem(prod, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int? Id, string returnUrl)
        {
            ProductViewModel prod = _productViewService.ViewAll().FirstOrDefault(p => p.Id == Id);

            if (prod != null)
            {
                cart.RemoveLine(prod);
            }
            return RedirectToAction("Index", new { returnUrl });
        }


        public ViewResult Index(Cart cart, string returnUrl)
        {

            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl

            });
        }

        public ActionResult IndexSearch(int? Id)
        {
            ViewBag.OrderTypes = new SelectList(_orderTypeViewService.ViewAll(), "Id", "OrderTypeName");

            var selected_orders = _orderViewService.ViewAll().Where(u => u.User.UserEmail == HttpContext.User.Identity.Name).
               Where(u => Id == null || u.OrderTypeId == Id).OrderBy(o => o.OrderDate).ThenBy(n => n.OrderNumber);


            return PartialView(selected_orders);
        }

        public ActionResult IndexSubSearchFilter(int? OrderTypeId)
        {

            ViewBag.OrderTypes = new SelectList(_orderTypeViewService.ViewAll(), "Id", "OrderTypeName");
            var selected_orders = _orderViewService.ViewAll().Where(u => u.User.UserEmail == HttpContext.User.Identity.Name).
               Where(u => OrderTypeId == null || u.OrderTypeId == OrderTypeId).OrderBy(o => o.OrderDate).ThenBy(n => n.OrderNumber);
            return PartialView(selected_orders);
        }



        public PartialViewResult _LoginPartial(Cart cart)
        {
            return PartialView(cart);
        }

        public ViewResult Checkout(Cart cart, UserViewModel user)
        {

            UserViewModel addOrChangeUser = null;
            OrderModifyModel newOrder = null;
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                addOrChangeUser = _userViewService.ViewAll().FirstOrDefault(c => c.UserEmail == HttpContext.User.Identity.Name);
                newOrder = CreateOrderForUser(addOrChangeUser.Id, cart);
            }
            else
            {
                if (ModelState.IsValid)
                {
                    // check if user exist
                    addOrChangeUser = _userViewService.ViewAll().FirstOrDefault(c => c.UserEmail == user.UserEmail);
                    if (addOrChangeUser != null)
                    {
                        //if user found update adress
                        _userModifyService.Update
                            (addOrChangeUser.Id,
                             new UserModifyModel(user.UserEmail, user.UserPassword,user.UserName, user.UserCountry, user.UserAddress, user.UserRoleId)
                            );

                    }
                    else
                    {
                        // if user not exist add new user
                        user.UserRoleId = 3;
                        _userModifyService.Add(new UserModifyModel(user.UserEmail, user.UserPassword, user.UserName, user.UserCountry, user.UserAddress, user.UserRoleId));
                        addOrChangeUser = _userViewService.ViewAll().FirstOrDefault(c => c.UserEmail == user.UserEmail);
                    }
                    newOrder = CreateOrderForUser(addOrChangeUser.Id, cart);

                }

            }
            if (HttpContext.User.Identity.IsAuthenticated || ModelState.IsValid)
            {


                _orderModifyService.Add(newOrder);
                //addOrChangeUser
                newOrder.User = new UserModifyModel(addOrChangeUser.UserEmail, addOrChangeUser.UserPassword, addOrChangeUser.UserName, addOrChangeUser.UserCountry, addOrChangeUser.UserAddress, addOrChangeUser.UserRoleId) ?? new UserModifyModel();
                cart.Clear();
            }


            return View(newOrder);
        }

        private OrderModifyModel CreateOrderForUser(int user_id, Cart cart)
        {

            OrderModifyModel newOrder = new OrderModifyModel
                  (DateTime.Now.Date,
                   GenerateOrderNumber(DateTime.Now.Date),
                   user_id
                   , 1, cart.Lines.Sum(s => s.Quantity * s.Product.Price));

            newOrder.OrderDetail = cart.Lines.Select(line => new OrderDetailModifyModel(
                      line.Product.Id,
                      line.Quantity,
                      line.Product.Price,
                      line.Quantity * line.Product.Price)).ToList();

            return newOrder;

        }


        private string GenerateOrderNumber(DateTime date)
        {
            int countOrderToday = _orderViewService.ViewAll().Where(d => d.OrderDate == date.Date).Count() + 1;
            return DateTime.Now.ToString("ddMMyyyy") + "_" + countOrderToday.ToString();
        }
    }
}