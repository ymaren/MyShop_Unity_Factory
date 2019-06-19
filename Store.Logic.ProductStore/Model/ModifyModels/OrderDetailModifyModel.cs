using Store.Logic.Entity;

namespace Store.Logic.ProductStore.Models.ModifyModels
{
    namespace Store.Logic.ProductStore.Models.ModifyModels
    {
        public class OrderDetailModifyModel
        {
            public int Id { get; set; }
            public int ProductId { get; set; }
            public ProductModifyModel Product { get; set; }
            public int OrderQTY { get; set; }
            public decimal ProductPrice { get; set; }
            public decimal ProductSum { get; set; }
            public int OrderId { get; set; }
            public OrderModifyModel Order { get; set; }
            public OrderDetailModifyModel(int productid, int orderQTY, decimal productPrice, decimal ProductSum)
            {
                this.ProductId = productid;
                this.OrderQTY = orderQTY;
                this.ProductPrice = productPrice;
                this.ProductSum = ProductSum;
            }
            public OrderDetailModifyModel()
            {

            }
        }
    }
}