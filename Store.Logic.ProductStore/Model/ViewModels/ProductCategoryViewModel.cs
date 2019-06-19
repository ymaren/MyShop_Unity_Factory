using Store.Logic.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace Store.Logic.ProductStore.Models.ViewModels
{
    //[Bind(Exclude = "Id")]
    public class ProductCategoryViewModel
    {
        public int Id { get;  set; }
        
        public string CategoryName { get;  set; }

        public string CategoryDescription { get;  set; }
        public List<ProductGroupViewModel> ProductGroups { get;  set; }
    public ProductCategoryViewModel (int Id, string Name, string Description)
        {
            this.Id = Id;
            this.CategoryName = Name;
            this.CategoryDescription = Description;
            
        }
        public ProductCategoryViewModel(ProductCategory productCategory)
        {
            this.Id = productCategory.Id;
            this.CategoryName = productCategory.Name;
            this.CategoryDescription = productCategory.Description;
        }

        public ProductCategoryViewModel()
        {
            ProductGroups = new List<ProductGroupViewModel>() { };
        }

    }
}
