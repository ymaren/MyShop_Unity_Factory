using Store.Logic.Entity;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace Store.Logic.ProductStore.Models.ViewModels
{
   
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string ProductCode { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int ProductGroupId { get; set; }
        public ProductGroupViewModel ProductGroup { get; set; }

        public ProductViewModel()
        {

        }
    
        public ProductViewModel(int Id, string productCode, string name, decimal price, string description,
          int productGroupId
            )
        {
            this.Id = Id;
            this.ProductCode = ProductCode;
            this.Name = name;
            this.Price = price;
            this.Description = description;
            this.ProductGroupId = productGroupId;
        }
    }
}
