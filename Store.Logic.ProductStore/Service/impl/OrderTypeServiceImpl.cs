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

    internal class OrderTypeServiceImpl : IOrderTypeViewService, IOrderTypeModifyService
    {
        private readonly IRepositoryFactory _sourceFactory;

        public OrderTypeServiceImpl(IRepositoryFactory sourceFactory)
        {
            _sourceFactory = sourceFactory;
        }
       

        public IEnumerable<OrderTypeViewModel> ViewAll()
        {
            using (var repository = _sourceFactory.CreateRepository<OrderType, int>())
            {

                var list = repository.GetAll().
                    Select(c => new OrderTypeViewModel(c.OrderTypeId, c.OrderTypeName));
                return list;
            }
        }
       

        public OrderTypeViewModel ViewSingle(int id)
        {
            using (var repository = _sourceFactory.CreateRepository<OrderType, int>())
            {
                var orderType = repository.GetSingle(id);
                
                return  new OrderTypeViewModel(orderType.OrderTypeId, orderType.OrderTypeName);
            }
        }


        public bool Add(OrderTypeModifyModel entity)
        {
            using (var repository = _sourceFactory.CreateRepository<OrderType, int>())
            {
                return repository.Add(new OrderType
                {
                    
                    OrderTypeName = entity.OrderTypeName
                    
                });

            };
        }

        public bool Update(int id, OrderTypeModifyModel entity)
        {
            using (var repository = _sourceFactory.CreateRepository<OrderType, int>())
            {
                var orderType = repository.GetSingle(id);
                if (orderType == null)
                    throw new NotFoundException();

                orderType.OrderTypeName = entity.OrderTypeName;

                return repository.Update(orderType);
            }
        }

        public bool Delete(int key)
        {
            using (var repository = _sourceFactory.CreateRepository<OrderType, int>())
            {
                return repository.Delete(key);
            }
        }
    }
}
