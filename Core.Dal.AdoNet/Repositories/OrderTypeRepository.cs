namespace Core.Dal.AdoNet.Repositories
{
    using Core.Dal.AdoNet.Exceptions;
    using Store.Logic.Entity;
    using System;
    using System.Data;

    internal class OrderTypeRepository : BaseRepository<OrderType>
    {
        const string IdField = "Id";
        const string OrderTypeNameFiled = "OrderTypeName";
        

        public OrderTypeRepository(IDbConnection connection)
            : base(connection, "dbo.OrderTypes")
        {
            BindViewMapper(IdField, (o, e) => e.OrderTypeId = Convert.ToInt32(o));
            BindViewMapper(OrderTypeNameFiled, (o, e) => e.OrderTypeName = o.ToString());
           
        }

        public override bool Add(OrderType entity)
        {
            var command = Connection.CreateCommand();
            command.CommandText = $"insert into {BaseTable} " +
                $"( {OrderTypeNameFiled}) values " +
                $"( '{entity.OrderTypeName}') ";
            try
            {
                return command.ExecuteNonQuery() == 1;
            }
            catch (Exception e)
            {
                throw new DalExecutionException("", e);
            }
        }

        public override bool Update(OrderType entity)
        {
            var command = Connection.CreateCommand();
            command.CommandText = $"update {BaseTable} " +
                $"set " +
                $"{OrderTypeNameFiled}='{entity.OrderTypeName}'  where Id={entity.OrderTypeId}";
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
