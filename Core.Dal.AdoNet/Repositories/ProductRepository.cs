namespace Core.Dal.AdoNet.Repositories
{
    using Core.Dal.AdoNet.Exceptions;
    using Store.Logic.Entity;
    using System;
    using System.Data;

    internal class ProductRepository : BaseRepository<Product>
    {
        const string IdField = "Id";
        const string ProductCodeFild = "ProductCode";
        const string NameFiled = "Name";
        const string PriceFiled = "Price";
        const string DescriptionFiled = "Description";
        const string ProductGroupIdFiled = "ProductGroupId";

        public ProductRepository(IDbConnection connection)
            : base(connection, "dbo.Products")
        {
            BindViewMapper(IdField, (o, e) => e.Id = Convert.ToInt32(o));
            BindViewMapper(ProductCodeFild, (o, e) => e.ProductCode = o.ToString());
            BindViewMapper(NameFiled, (o, e) => e.Name = o.ToString());
            BindViewMapper(PriceFiled, (o, e) => e.Price = Convert.ToDecimal(o));
            BindViewMapper(DescriptionFiled, (o, e) => e.Description = o.ToString());
            BindViewMapper(ProductGroupIdFiled, (o, e) => e.ProductGroupId = Convert.ToInt32(o));
        }

        public override bool Add(Product entity)
        {
            var command = Connection.CreateCommand();
            command.CommandText = $"insert into {BaseTable} " +
                $"({ProductCodeFild}, {NameFiled},{DescriptionFiled},{ProductGroupIdFiled} ) values " +
                $"( '{entity.ProductCode}', '{entity.Name}','{entity.Description}','{ProductGroupIdFiled}' ) ";
            try
            {
                return command.ExecuteNonQuery() == 1;
            }
            catch (Exception e)
            {
                throw new DalExecutionException("", e);
            }
        }

        public override bool Update(Product entity)
        {
            var command = Connection.CreateCommand();
            command.CommandText = $"update {BaseTable} " +
                $"set " +
                $"{ProductCodeFild}='{entity.ProductCode}', " +
                $"{NameFiled}='{entity.Name}'," +
                $"{DescriptionFiled}='{entity.Description}'," +
                $"{ProductGroupIdFiled}='{entity.ProductGroupId}'" +
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
