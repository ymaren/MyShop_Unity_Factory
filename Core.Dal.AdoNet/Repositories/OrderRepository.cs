namespace Core.Dal.AdoNet.Repositories
{
    using Core.Dal.AdoNet.Exceptions;
    using Store.Logic.Entity;
    using System;
    using System.Data;
    using System.Globalization;

    internal class OrderRepository : BaseRepository<Order>
    {
        const string IdField = "Id";
        const string OrderDateFild = "OrderDate";
        const string OrderNumberFiled = "OrderNumber";
        const string OrderAmountFiled = "OrderAmount";
        const string OrderTypeIdFiled = "OrderTypeId";
        const string UserIdFiled = "UserId";

        public OrderRepository(IDbConnection connection)
            : base(connection, "dbo.Orders")
        {
            BindViewMapper(IdField, (o, e) => e.Id = Convert.ToInt32(o));
            BindViewMapper(OrderDateFild, (o, e) => e.OrderDate = Convert.ToDateTime(o));
            BindViewMapper(OrderNumberFiled, (o, e) => e.OrderNumber = o.ToString());
            BindViewMapper(OrderAmountFiled, (o, e) => e.OrderAmount = Convert.ToDecimal(o));
            BindViewMapper(OrderTypeIdFiled, (o, e) => e.OrderTypeId = Convert.ToInt32(o));
            BindViewMapper(UserIdFiled, (o, e) => e.UserId = Convert.ToInt32(o));
        }

        public override Order GetSingle(int key)
        {

            var command = Connection.CreateCommand();
            command.CommandText = $"{BaseQuery} where Id ={key};" +
                $"select * from  [dbo].[OrderDetails] where  OrderId={key}";

            try
            {
                using (var reader = command.ExecuteReader())
                {
                    Order newOrder = null;
                    if (reader.Read())
                    {
                        newOrder = Map(reader);

                    }
                    if (reader.NextResult())
                    {
                        while (reader.Read())
                        {
                            newOrder.OrderDetail.Add(
                                new OrderDetail(
                               int.Parse(reader["Id"].ToString()),
                               int.Parse(reader["ProductId"].ToString()),
                               int.Parse(reader["OrderQTY"].ToString()),
                               decimal.Parse(reader["ProductPrice"].ToString()),
                               decimal.Parse(reader["ProductSum"].ToString()),
                               int.Parse(reader["OrderId"].ToString())
                               )
                             );
                        }
                    }

                    return newOrder;
                }
            }
            catch (Exception e)
            {
                throw new DalExecutionException("", e);
            }

        }



        public override bool Add(Order entity)
        {
            var command = Connection.CreateCommand();
            command.CommandText = $"insert into {BaseTable} " +
                $"({OrderDateFild}, {OrderNumberFiled},{OrderAmountFiled},{OrderTypeIdFiled},{UserIdFiled} ) values " +
                $"( '{entity.OrderDate.ToString("yyyyMMdd")}', '{entity.OrderNumber}',{entity.OrderAmount.ToString(CultureInfo.InvariantCulture)},'{entity.OrderTypeId}','{entity.UserId}') ";
            try
            {
                return command.ExecuteNonQuery() == 1;
            }
            catch (Exception e)
            {
                throw new DalExecutionException("", e);
            }
        }

        public override bool Update(Order entity)
        {
            var command = Connection.CreateCommand();
            command.CommandText = $"update {BaseTable} " +
                $"set " +
                $"{OrderDateFild}='{entity.OrderDate}', " +
                $"{OrderNumberFiled}='{entity.OrderNumber}'," +
                $"{OrderAmountFiled}='{entity.OrderAmount}'," +
                $"{OrderTypeIdFiled}='{entity.OrderTypeId}'" +
                $"{UserIdFiled}='{entity.UserId}'" +
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
