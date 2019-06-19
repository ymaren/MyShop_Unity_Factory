namespace Store.Logic.ProductStore.Models.ModifyModels
{
    public class ProductGroupModifyModel
    {
        public string GroupName { get; set; }
        public string GroupDescription { get; set; }
        public int ProductCategoryid { get; set; }
        public ProductGroupModifyModel ProductCategory { get; set; }

        public ProductGroupModifyModel( string groupName, string groupDescription, int productCategoryid)
        {
            
            this.GroupName = groupName;
            this.GroupDescription = groupDescription;
            this.ProductCategoryid = productCategoryid;
        }

    }
}