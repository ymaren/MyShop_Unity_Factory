using Store.Logic.ProductStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyShop.Models
{
    public class IndexProductViewModel
    {
        public IEnumerable<ProductViewModel> Products { get; set; }
        public PageInfo PageInfo { get; set; }
        public ProductCategoryViewModel CurrentCategory { get; set; }
        public ProductGroupViewModel CurrentGroup { get; set; }
        public MyShop.Controllers.Order? CurrentOrder { get; set; }
    }
}