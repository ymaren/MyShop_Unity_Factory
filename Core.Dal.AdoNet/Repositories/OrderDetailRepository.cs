namespace Core.Dal.AdoNet.Repositories
{
    using Core.Dal.AdoNet.Exceptions;
    using Store.Logic.Entity;
    using System;
    using System.Data;

    internal class OrderDetailRepository : BaseRepository<OrderDetail>
    {
        const string IdField = "Id";
        const string ProductIdFild = "ProductId";
        const string OrderQTYFild = "OrderQTY";
        const string ProductPriceFild = "ProductPrice";
        const string ProductSumFild = "ProductSum";
        const string OrderIdFild = "OrderId";
        

        public OrderDetailRepository(IDbConnection connection)
            : base(connection, "dbo.OrderDetails")
        {
            BindViewMapper(IdField, (o, e) => e.Id = Convert.ToInt32(o));
            BindViewMapper(ProductIdFild, (o, e) => e.ProductId = Convert.ToInt32(o));
            BindViewMapper(OrderQTYFild, (o, e) => e.OrderQTY = Convert.ToInt32(o));
            BindViewMapper(ProductPriceFild, (o, e) => e.ProductPrice = Convert.ToDecimal(o));
            BindViewMapper(ProductSumFild, (o, e) => e.ProductSum = Convert.ToDecimal(o));
            BindViewMapper(OrderIdFild, (o, e) => e.OrderId = Convert.ToInt32(o));
        }

    //    public override OrderDetail GetSingle(int key)
    //    {
        
    //        var command = Connection.CreateCommand();
    //        command.CommandText = $"{BaseQuery} where Id ={key};" +
    //            $"SELECT cred.*   FROM [MyShop].[dbo].[UserRoleCredentials] cr   join [dbo].[Credentials] cred on cr.Credential_Id= cred.Id  where cr.UserRole_Id={key}";

    //        try
    //        {
    //            using (var reader = command.ExecuteReader())
    //            {
    //                User newUser = null;
    //                if (reader.Read())
    //                {
    //                    newUser =Map(reader);
                        
    //                }
    //                if (reader.NextResult())
    //                {
    //                    while (reader.Read())
    //                    {
    //                        newUser.Credential.Add(
    //                            new Credential(
    //                           int.Parse(reader["Id"].ToString()),
    //                           reader["NameCredential"].ToString(),
    //                           reader["FullNameCredential"].ToString(),
    //                           int.Parse(reader["ParentCredentialid"].ToString()),
    //                           int.Parse(reader["Order"].ToString()),
    //                           reader["URL"].ToString()
    //                           )
    //                         );
    //                    }
    //                }

    //                return newUser;
    //            }
    //        }
    //        catch (Exception e)
    //        {
    //            throw new DalExecutionException("", e);
    //        }
        
    //}

        public override bool Add(OrderDetail entity)
        {
            var command = Connection.CreateCommand();
            command.CommandText = $"insert into {BaseTable} " +
                $"({ProductIdFild},{OrderQTYFild},{ProductPriceFild},{ProductSumFild},{OrderIdFild}) values " +
                $"('{entity.ProductId}', '{entity.OrderQTY}', '{entity.ProductPrice}','{entity.ProductSum}','{entity.OrderId}') ";
            try
            {
                return command.ExecuteNonQuery() == 1;
            }
            catch (Exception e)
            {
                throw new DalExecutionException("", e);
            }
        }

        public override bool Update(OrderDetail entity)
        {
            var command = Connection.CreateCommand();
            command.CommandText = $"update {BaseTable} " +
                $"set " +
                $"{ProductIdFild}='{entity.OrderId}' " +
                $"{OrderQTYFild}='{entity.OrderQTY}' " +
                $"{ProductPriceFild}='{entity.ProductPrice}' " +
                $"{ProductSumFild}='{entity.ProductSum}' " +
                $"{OrderIdFild}='{entity.OrderId}' " +
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
