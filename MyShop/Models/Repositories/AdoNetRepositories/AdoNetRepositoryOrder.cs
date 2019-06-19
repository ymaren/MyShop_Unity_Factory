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
    public class AdoNetRepositoryOrder : BaseRepository<Order>
    {
        public AdoNetRepositoryOrder()
            : base("dbo.Orders")
        {

        }

        public override IEnumerable<Order> GetAll()
        {
            try
            {
                string query = "SELECT ordH.Id,ordH.OrderDate, ordH.OrderNumber, ordH.OrderAmount,ordH.OrderTypeId,ordH.UserId,  ordH.OrderTypeId as [Ordertype_Id], orderType.Id,orderType.OrderTypeName, ordH.UserId as [User_Id], users.* FROM [MyShop].[dbo].[Orders] ordH JOIN [dbo].[Users] users on ordH.UserId= users.Id JOIN [dbo].[OrderTypes] orderType  on ordH.OrderTypeId= orderType.Id";
                

                var listOrderIncludeOrderTypeUser = Connection.Query<Order, OrderType, User, Order>(
                    query, (order, ordertype, user) => { order.OrderType = ordertype; order.User = user; return order; }, splitOn: "Ordertype_Id,User_Id");
                 //.GroupBy(o => o.Id).Select(c =>
                 //{

                 //    var userRole = c.First();

                 //    var credentials = c.Select(p => p.Credential.Single()).ToList();
                 //    userRole.Credential = credentials.Any(g => g.NameCredential != null) ? credentials.ToList() : new List<Credential>() { };
                 //    return userRole;
                 //});
                return listOrderIncludeOrderTypeUser;

            }
            catch (Exception ex)
            {
                throw new DalExecutionException("", ex);
            }

        }

        public override Order GetSingle(int key)
        {
            try
            {
                string query = string.Format("SELECT ordH.Id,ordH.OrderDate, ordH.OrderNumber, ordH.OrderAmount,ordH.UserId, ordH.OrderTypeId,orderType.Id AS [SplitOrderTypeId],orderType.Id,orderType.OrderTypeName,users.Id as [SplitUserId],users.*, orderDetails.Id as [OrderDetId], orderDetails.* , prod.Id as [Prod_id],prod.*FROM [Orders] ordH join [dbo].[OrderDetails] orderDetails on ordH.Id= orderDetails.OrderId JOIN [dbo].[Products] prod on orderDetails.ProductId= prod.Id JOIN [dbo].[OrderTypes] orderType  on ordH.OrderTypeId= orderType.Id JOIN [dbo].[Users] users on ordH.UserId= users.Id where ordH.Id={0} ", key);
                var listOrderIncludeOrderTypeUser = Connection.Query<Order, OrderType, User, OrderDetail, Product, Order>(
                    query, (order, ordertype,user, orderDetail, product) => { order.OrderType = ordertype;order.User=user; order.OrderDetail.Add(orderDetail); orderDetail.Product = product; return order; }, splitOn: "SplitOrderTypeId,SplitUserId,OrderDetId,Prod_id")
                  .GroupBy(o => o.Id).Select(c =>
                {

                    var order = c.First();
                    //var ordertype = c.Select(p => p.OrderType);
                    //var user = c.Select(p => p.User);
                   
                    var orderDetail= c.Select(p => p.OrderDetail.Single()).ToList();
                    order.OrderDetail = orderDetail.ToList();
                    return order;
                });
                return listOrderIncludeOrderTypeUser.FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw new DalExecutionException("", ex);
            }

        }


        public override Order Add(Order entity)
        {
            var addCategory = Connection.Execute("Insert into Orders (OrderDate,OrderNumber, OrderAmount,OrderTypeId,UserId) values (@OrderDate,@OrderNumber,@OrderAmount,@OrderTypeId,@UserId)",
               new { OrderDate = entity.OrderDate, OrderNumber = entity.OrderNumber, OrderAmount = entity.OrderAmount, OrderTypeId = entity.OrderTypeId, UserId = entity.UserId });
            return entity;
        }

        public override bool Update(Order entity)
        {
            var updateroduct = Connection.Execute("Update Orders set OrderDate = @OrderDate,OrderNumber = @OrderNumber,OrderAmount = @OrderAmount," +
                "OrderTypeId = @OrderTypeId, UserId = @UserId  Where Id = @Id ",
               new { OrderDate = entity.OrderDate, OrderNumber = entity.OrderNumber, OrderAmount = entity.OrderAmount, OrderTypeId = entity.OrderTypeId, UserId = entity.UserId, Id = entity.Id });
            return true;
        }
    }
}