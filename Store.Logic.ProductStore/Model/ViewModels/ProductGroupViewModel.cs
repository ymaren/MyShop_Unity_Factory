using Store.Logic.Entity;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace Store.Logic.ProductStore.Models.ViewModels
{
   
    public class ProductGroupViewModel
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public string GroupDescription { get; set; }
        public int ProductCategoryid { get; set; }
        public  ProductCategoryViewModel ProductCategory { get; set; }
        //public ICollection<Product> Products;

        public ProductGroupViewModel()
        {

        }
        public ProductGroupViewModel(ProductGroup productGroup)
        {
            this.Id = productGroup.Id;
            this.GroupName = productGroup.GroupName;
            this.GroupDescription = productGroup.GroupDescription;
            this.ProductCategoryid = productGroup.ProductCategoryid;
        }

        public ProductGroupViewModel(int Id, string groupName, string groupDescription, int productCategoryid)
        {
            this.Id = Id;
            this.GroupName = GroupName;
            this.GroupDescription = groupDescription;
            this.ProductCategoryid = productCategoryid;
        }
    }
}
