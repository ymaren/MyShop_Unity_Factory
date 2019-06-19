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
    public class AdoNetRepositoryProductCategory : BaseRepository<ProductCategory>
    {
        public AdoNetRepositoryProductCategory()
            : base( "dbo.ProductCategories")
        {           

        }

        public override IEnumerable<ProductCategory> GetAll()
        {
            try
            {
                string query = "SELECT cat.*, gr.Id as [GroupId], gr.GroupName, GR.GroupDescription FROM ProductCategories cat left join [dbo].[ProductGroups]  gr on cat.Id=gr.ProductCategoryid";
                dynamic test = Connection.Query<dynamic>(query);

                var listCategoryInclideGroups = Connection.Query<ProductCategory, ProductGroup, ProductCategory>(
                    query, (category, group) => { category.ProductGroups.Add(group); return category; }, splitOn: "GroupId")                
                 .GroupBy(o => o.Id).Select(c =>
                {
                    
                    var category = c.First();

                    var groups = c.Select(p => p.ProductGroups.Single()).ToList();
                    category.ProductGroups = groups.Any(G=>G.GroupName!=null)? groups.ToList(): new List<ProductGroup>() { };
                   return category;
                });
                return listCategoryInclideGroups;

            }
            catch (Exception ex)
            {
                throw new DalExecutionException("", ex);
            }

        }


        public override ProductCategory Add(ProductCategory entity)
        {
            var addCategory = Connection.Execute("Insert into ProductCategories (CategoryName, CategoryDescription) values (@CategoryName, @CategoryDescription)", new { CategoryName = entity.CategoryName, CategoryDescription = entity.CategoryDescription });
            return entity;
        }        

        public override bool Update(ProductCategory entity)
        {
            var updateCategory = Connection.Execute("Update ProductCategories set CategoryName = @CategoryName, CategoryDescription = @CategoryDescription Where Id = @Id ", new { CategoryName = entity.CategoryName, CategoryDescription = entity.CategoryDescription,id=entity.Id });
            return true;
        }
    }
}