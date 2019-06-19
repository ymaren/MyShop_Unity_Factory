
using Store.Logic.Entity;
using Store.Logic.ProductStore.Infustructure;
using Store.Logic.ProductStore.Service.ViewServices;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace Store.Logic.ProductStore.Models.ViewModels
{
    public class OrderDetailViewModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public  ProductViewModel Product { get; set; }
        public int OrderQTY { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal ProductSum { get; set; }
        public int OrderId { get; set; }
        public OrderViewModel Order { get; set; }
        public OrderDetailViewModel(int id, int productid, int orderQTY, decimal productPrice, decimal ProductSum,int OrderId)
        {
            this.Id = id;
            this.ProductId = productid;
            this.OrderQTY = orderQTY;
            this.ProductPrice = productPrice;
            this.ProductSum = ProductSum;
            this.OrderId = OrderId;
        }
        public OrderDetailViewModel()
        {

        }
    }
}

