using System;
using System.Collections.Generic;

namespace Store.Logic.Entity
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public  Product Product { get; set; }
        public int OrderQTY { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal ProductSum { get; set; }
        public int OrderId { get; set; }
        public OrderDetail(int id, int productid, int orderQTY, decimal productPrice, decimal ProductSum, int OrderId)
        {
            this.Id = id;
            this.ProductId = productid;
            this.OrderQTY = orderQTY;
            this.ProductPrice = productPrice;
            this.ProductSum = ProductSum;
            this.OrderId = OrderId;
        }
        public OrderDetail()
        {

        }
    }
}