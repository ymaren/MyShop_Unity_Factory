using Dapper;
using MyShop.Models.MyShopModels;
using MyShop.Models.Repositories.AdoNetRepositories.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MyShop.Models.Repositories.AdoNetRepositories
{
    public class AdoNetRepositoryProduct : BaseRepository<Product>
    {
        public AdoNetRepositoryProduct()
            : base("dbo.Products")
        {           

        }

        public override IEnumerable<Product> GetAll()
        {
            try
            {
                string query = "SELECT * FROM Products prod left join [dbo].[ProductGroups]  gr on prod.ProductGroupId=gr.Id";
                var listGroups = Connection.Query<Product, ProductGroup, Product>(
                     query, (prod, group) => { prod.ProductGroup = group; return prod; });
                return listGroups;

            }
            catch (Exception ex)
            {
                throw new DalExecutionException("", ex);
            }

        }


        public override Product Add(Product entity)
        {
            var addCategory = Connection.Execute("Insert into Products (ProductCode, Name,Price,Description,ProductGroupId ) values (@ProductCode, @Name, @Price,@Description,@ProductGroupId)", 
                new {ProductCode = entity.ProductCode, Name = entity.Name, Price = entity.Price, Description = entity.Description, entity.ProductGroupId });
            return entity;
        }        

        public override bool Update(Product entity)
        {
            var updateroduct = Connection.Execute("Update Products set ProductCode = @ProductCode, Name = @Name,Price = @Price,Description = @Description, ProductGroupId = @ProductGroupId  Where Id = @Id ", 
                new { ProductCode = entity.ProductCode, Name = entity.Name, Price = entity.Price, Description = entity.Description, ProductGroupId= entity.ProductGroupId, id = entity.Id });
            return true;
        }
    }
}