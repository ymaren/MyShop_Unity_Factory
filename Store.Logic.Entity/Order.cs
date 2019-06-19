using System;
using System.Collections.Generic;

namespace Store.Logic.Entity
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderNumber { get; set; }

        public decimal OrderAmount { get; set; }
        public int UserId { get; set; }
        public  User User { get; set; }
        public int OrderTypeId { get; set; }
        public  OrderType OrderType { get; set; }
        public virtual List<OrderDetail> OrderDetail { get; set; }

        public Order(DateTime OrderDate, string orderNumber, int ordertoUser, int OrderTypeid, decimal orderAmount)
        {
            this.OrderDate = OrderDate;
            this.OrderNumber = orderNumber;
            this.UserId = ordertoUser;
            this.OrderTypeId = OrderTypeid;
            this.OrderAmount = orderAmount;
        }



        public Order(DateTime OrderDate, string orderNumber)
        {

            this.OrderDate = OrderDate;
            this.OrderNumber = orderNumber;
            //OrderDetail = new List<OrderDetail>();
        }

        public Order()
        {
            OrderDetail = new List<OrderDetail>();


        }
    }
}