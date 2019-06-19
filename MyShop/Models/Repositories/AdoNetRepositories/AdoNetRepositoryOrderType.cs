using MyShop.Models.interfaces.Repositories;
using MyShop.Models.MyShopModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace MyShop.Models.Repositories.AdoNetRepositories
{
    public class AdoNetRepositoryOrderType : BaseRepository<OrderType>
    {
        public AdoNetRepositoryOrderType()
            : base("dbo.OrderTypes")
        {

        }


        public override OrderType Add(OrderType entity)
        {
            var addCategory = Connection.Execute("Insert into OrderTypes (OrderTypeName ) values (@OrderTypeName)",
               new { OrderTypeName = entity.OrderTypeName});
            return entity;
        }

        public override bool Update(OrderType entity)
        {
            var updateroduct = Connection.Execute("Update OrderTypes set OrderTypeName = @OrderTypeName  Where Id = @Id ",
               new { OrderTypeName = entity.OrderTypeName,  id = entity.Id });
            return true;
        }
    }
}