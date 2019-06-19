using Store.Logic.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace Store.Logic.ProductStore.Models.ViewModels
{
   
    public class OrderViewModel
    {

        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderNumber { get; set; }

        public decimal OrderAmount { get; set; }
        public int UserId { get; set; }
        public  UserViewModel User { get; set; }
        public int OrderTypeId { get; set; }
        public  OrderTypeViewModel OrderType { get; set; }
        public virtual List<OrderDetailViewModel> OrderDetail { get; set; }

        public OrderViewModel(int Id,DateTime OrderDate, string orderNumber,  decimal orderAmount, int ordertoUser, UserViewModel userViewModel,int OrderTypeid, OrderTypeViewModel orderType, List<OrderDetailViewModel> orderDetail )
        {
            this.Id = Id;
            this.OrderDate = OrderDate;
            this.OrderNumber = orderNumber;
            this.UserId = ordertoUser;
            this.User = userViewModel;
            this.OrderTypeId = OrderTypeid;
            this.OrderType = orderType;
            this.OrderAmount = orderAmount;
            this.OrderDetail=orderDetail;
        }
        public OrderViewModel(int Id, DateTime OrderDate, string orderNumber, decimal orderAmount, int ordertoUser, UserViewModel userViewModel, int OrderTypeid, OrderTypeViewModel orderType)
        {
            this.Id = Id;
            this.OrderDate = OrderDate;
            this.OrderNumber = orderNumber;
            this.UserId = ordertoUser;
            this.User = userViewModel;
            this.OrderTypeId = OrderTypeid;
            this.OrderType = orderType;
            this.OrderAmount = orderAmount;
            
        }


        public OrderViewModel(DateTime OrderDate, string orderNumber)
        {

            this.OrderDate = OrderDate;
            this.OrderNumber = orderNumber;
            //OrderDetail = new List<OrderDetail>();
        }

        public OrderViewModel()
        {
            OrderDetail = new List<OrderDetailViewModel>();


        }
    }
}
