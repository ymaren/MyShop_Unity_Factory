
using Store.Logic.Entity;
using Store.Logic.ProductStore.Infustructure;
using Store.Logic.ProductStore.Service.ViewServices;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace Store.Logic.ProductStore.Models.ViewModels
{
    public class OrderTypeViewModel
    {
        public int Id { get; set; }
        public string OrderTypeName { get; set; }


        public OrderTypeViewModel(int orderTypeId, string orderTypeName)
        {
            this.Id = orderTypeId;
            this.OrderTypeName = orderTypeName;


        }

        public OrderTypeViewModel(OrderType orderType)
        {
            this.Id = orderType.OrderTypeId;
            this.OrderTypeName = orderType.OrderTypeName;
        }
        public OrderTypeViewModel()
        {

        }
    }
}

