namespace Store.Logic.Entity
{
    public class ProductGroup
    {
        public int Id { get; set; }

        public string GroupName { get; set; }

        public string GroupDescription { get; set; }

        public int ProductCategoryid { get; set; }

        public ProductCategory Category { get; set; }
    }
}