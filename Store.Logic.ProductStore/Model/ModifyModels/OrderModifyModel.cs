using Store.Logic.Entity;
using Store.Logic.ProductStore.Models.ModifyModels.Store.Logic.ProductStore.Models.ModifyModels;
using Store.Logic.ProductStore.Models.ViewModels;
using System;
using System.Collections.Generic;

namespace Store.Logic.ProductStore.Models.ModifyModels
{
    public class OrderModifyModel
    {       
        public DateTime OrderDate { get; set; }
        public string OrderNumber { get; set; }
        public decimal OrderAmount { get; set; }
        public int UserId { get; set; }
        public  UserModifyModel User { get; set; }
        public int OrderTypeId { get; set; }
        public  OrderType OrderType { get; set; }
        public  List<OrderDetailModifyModel> OrderDetail { get; set; }

        public OrderModifyModel(DateTime OrderDate, string orderNumber, int ordertoUser, int OrderTypeid, decimal orderAmount)
        {
            this.OrderDate = OrderDate;
            this.OrderNumber = orderNumber;
            this.UserId = ordertoUser;
            this.OrderTypeId = OrderTypeid;
            this.OrderAmount = orderAmount;
        }



        public OrderModifyModel(DateTime OrderDate, string orderNumber)
        {

            this.OrderDate = OrderDate;
            this.OrderNumber = orderNumber;
            //OrderDetail = new List<OrderDetail>();
        }

        public OrderModifyModel()
        {
            OrderDetail = new List<OrderDetailModifyModel>();


        }

    }
}