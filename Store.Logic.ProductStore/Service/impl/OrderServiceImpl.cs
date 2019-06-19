namespace Store.Logic.ProductStore.Service.impl
{
    using Core.Common.Dal;    
    using Models.ModifyModels;
    using Models.ViewModels;
    using Service.ModifyServices;
    using Service.ViewServices;
    using Store.Logic.Entity;
    using Store.Logic.ProductStore.Exceptions;
    using Store.Logic.ProductStore.Models.ModifyModels.Store.Logic.ProductStore.Models.ModifyModels;
    using System.Collections.Generic;
    using System.Linq;

    internal class OrderServiceImpl : IOrderViewService, IOrderModifyService
    {
        private readonly IRepositoryFactory _sourceFactory;

        public OrderServiceImpl(IRepositoryFactory sourceFactory)
        {
            _sourceFactory = sourceFactory;
        }
       

        public IEnumerable<OrderViewModel> ViewAll()
        {
            using (var repository = _sourceFactory.CreateRepository<Order, int>())
            {

                var list = repository.GetAll().
                    Select(c => new OrderViewModel(c.Id, c.OrderDate,c.OrderNumber,c.OrderAmount,
                    c.UserId,
                    new UserViewModel(_sourceFactory.CreateRepository<User, int>().GetSingle(c.UserId)),
                    c.OrderTypeId,
                    new OrderTypeViewModel(_sourceFactory.CreateRepository<OrderType, int>().GetSingle(c.OrderTypeId))
                    ));
                return list;
            }
        }
       

        public OrderViewModel ViewSingle(int id)
        {
            using (var repository = _sourceFactory.CreateRepository<Order, int>())
            {
                var order = repository.GetSingle(id);

                return new OrderViewModel(order.Id, order.OrderDate, order.OrderNumber, order.OrderAmount, order.UserId,
                    new UserViewModel(_sourceFactory.CreateRepository<User, int>().GetSingle(order.UserId)),
                    order.OrderTypeId,
                    new OrderTypeViewModel(_sourceFactory.CreateRepository<OrderType, int>().GetSingle(order.OrderTypeId)),
                    (order.OrderDetail.Select(c=> new OrderDetailViewModel(c.Id,c.ProductId,c.OrderQTY, c.ProductPrice, c.ProductSum,c.OrderId)).ToList())
                    );
            }
        }


        public bool Add(OrderModifyModel entity)
        {
            using (var repository = _sourceFactory.CreateRepository<Order, int>())
            {
                return repository.Add(new Order
                {
                    OrderDate= entity.OrderDate,
                    OrderNumber= entity.OrderNumber,
                    OrderAmount=entity.OrderAmount,
                    OrderTypeId=entity.OrderTypeId,
                    UserId=entity.UserId

                });

            };
        }

        public bool Update(int id, OrderModifyModel entity)
        {
            using (var repository = _sourceFactory.CreateRepository<Order, int>())
            {
                var order = repository.GetSingle(id);
                if (order == null)
                    throw new NotFoundException();

                order.OrderDate = entity.OrderDate;
                order.OrderNumber = entity.OrderNumber;
                order.OrderAmount = entity.OrderAmount;
                order.OrderTypeId = entity.OrderTypeId;
                order.UserId = entity.UserId;

                return repository.Update(order);
            }
        }

        public bool Delete(int key)
        {
            using (var repository = _sourceFactory.CreateRepository<Order, int>())
            {
                return repository.Delete(key);
            }
        }

       
    }
}
