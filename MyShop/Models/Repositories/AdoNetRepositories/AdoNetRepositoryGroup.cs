using MyShop.Models.interfaces.Repositories;
using MyShop.Models.MyShopModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using MyShop.Models.Repositories.AdoNetRepositories.Exceptions;

namespace MyShop.Models.Repositories.AdoNetRepositories
{
    public class AdoNetRepositoryGroups : BaseRepository<ProductGroup>
    {

        public AdoNetRepositoryGroups()
            : base("dbo.ProductGroups")
        {

        }     

        public override IEnumerable<ProductGroup> GetAll()
        {
            try
            {
                string query = "SELECT gr.*, cat.Id as[CategId],cat.*  FROM [dbo].[ProductGroups] gr left join  ProductCategories cat on gr.ProductCategoryid = cat.Id";
                var listGroups = Connection.Query<ProductGroup, ProductCategory, ProductGroup>(
                     query, (group, category) => { group.ProductCategory = category; return group; }, splitOn: "CategId");
                return listGroups;

            }
            catch (Exception ex)
            {
                throw new DalExecutionException("", ex);
            }
            
        }

        public override ProductGroup GetSingle(int key)
        {
            try
            {
                string query = string.Format("SELECT gr.*, cat.Id as[CategId],cat.*  FROM [dbo].[ProductGroups] gr left join  ProductCategories cat on gr.ProductCategoryid = cat.Id where gr.Id={0}", key);
                var listGroups = Connection.Query<ProductGroup, ProductCategory, ProductGroup>(
                     query, (group, category) => { group.ProductCategory = category; return group; }, splitOn: "CategId");
                return listGroups.FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw new DalExecutionException("", ex);
            }
        }

        public override ProductGroup Add(ProductGroup entity)
        {
            var addGroup = Connection.Execute("Insert into ProductGroups (GroupName, GroupDescription ,ProductCategoryid) values (@GroupName, @GroupDescription,@ProductCategoryid )", new { GroupName = entity.GroupName, GroupDescription = entity.GroupDescription, ProductCategoryid = entity.ProductCategoryid });
            return entity;
        }
        public override bool Update(ProductGroup entity)
        {
            var updateCategory = Connection.Execute("Update ProductGroups set GroupName = @GroupName, GroupDescription = @GroupDescription, ProductCategoryid = @ProductCategoryid Where Id = @Id ", new { GroupName = entity.GroupName, GroupDescription = entity.GroupDescription, ProductCategoryid = entity.ProductCategoryid, Id = entity.Id });
            return true;
        }
    }
}