namespace Core.Dal.AdoNet.Repositories
{
    using Core.Dal.AdoNet.Exceptions;
    using Store.Logic.Entity;
    using System;
    using System.Data;

    internal class ProductGroupRepository : BaseRepository<ProductGroup>
    {
        const string IdField = "Id";
        const string GroupNameFild = "GroupName";
        const string GroupDescriptionFiled = "GroupDescription";
        const string ProductCategoryidFiled = "ProductCategoryid";

        public ProductGroupRepository(IDbConnection connection)
            : base(connection, "dbo.ProductGroups")
        {
            BindViewMapper(IdField, (o, e) => e.Id = Convert.ToInt32(o));
            BindViewMapper(GroupNameFild, (o, e) => e.GroupName = o.ToString());
            BindViewMapper(GroupDescriptionFiled, (o, e) => e.GroupDescription = o.ToString());
            BindViewMapper(ProductCategoryidFiled, (o, e) => e.ProductCategoryid = Convert.ToInt32(o));
        }

        public override bool Add(ProductGroup entity)
        {
            var command = Connection.CreateCommand();
            command.CommandText = $"insert into {BaseTable} " +
                $"({GroupNameFild}, {GroupDescriptionFiled},{ProductCategoryidFiled}) values " +
                $"( '{entity.GroupName}', '{entity.GroupDescription}','{entity.ProductCategoryid}' ) ";
            try
            {
                return command.ExecuteNonQuery() == 1;
            }
            catch (Exception e)
            {
                throw new DalExecutionException("", e);
            }
        }

        public override bool Update(ProductGroup entity)
        {
            var command = Connection.CreateCommand();
            command.CommandText = $"update {BaseTable} " +
                $"set " +
                $"{GroupNameFild}='{entity.GroupName}', " +
                $"{GroupDescriptionFiled}='{entity.GroupDescription}'," +
                $"{ProductCategoryidFiled}={entity.ProductCategoryid}"+
                $" where Id={entity.Id}";
            try
            {
                return command.ExecuteNonQuery() == 1;
            }
            catch (Exception e)
            {
                throw new DalExecutionException("", e);
            }
        }
    }
}
