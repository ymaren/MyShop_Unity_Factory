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

    internal class OrderDetailServiceImpl : IOrderDetailViewService, IOrderDetailModifyService
    {
        private readonly IRepositoryFactory _sourceFactory;

        public OrderDetailServiceImpl(IRepositoryFactory sourceFactory)
        {
            _sourceFactory = sourceFactory;
        }
       

        //public IEnumerable<OrderDetailViewModel> ViewAll()
        //{
        //    using (var repository = _sourceFactory.CreateRepository<OrderDetail, int>())
        //    {

        //        //var list = repository.GetAll().
        //        //    Select(c => new OrderTypeViewModel(c.OrderTypeId, c.OrderTypeName));
        //        //return list;
        //    }
        //}
       

        //public OrderDetailViewModel ViewSingle(int id)
        //{
        //    //using (var repository = _sourceFactory.CreateRepository<OrderDetail, int>())
        //    //{
        //    //    var orderType = repository.GetSingle(id);
                
        //    //    return  new OrderTypeViewModel(orderType.OrderTypeId, orderType.OrderTypeName);
        //    //}
        //}


        public bool Add(OrderDetailModifyModel entity)
        {
            using (var repository = _sourceFactory.CreateRepository<OrderDetail, int>())
            {
                return repository.Add(new OrderDetail
                {

                    ProductId = entity.ProductId,
                    OrderQTY = entity.OrderQTY,
                    ProductPrice = entity.ProductPrice,
                    ProductSum = entity.ProductSum,
                    OrderId = entity.OrderId

            });

            };
        }

        public bool Update(int id, OrderDetailModifyModel entity)
        {
            using (var repository = _sourceFactory.CreateRepository<OrderDetail, int>())
            {
                var orderDet = repository.GetSingle(id);
                if (orderDet == null)
                    throw new NotFoundException();

                orderDet.ProductId = entity.ProductId;
                orderDet.OrderQTY = entity.OrderQTY;
                orderDet.ProductPrice = entity.ProductPrice;
                orderDet.ProductSum = entity.ProductSum;
                orderDet.OrderId = entity.OrderId;
                return repository.Update(orderDet);
            }
        }

        public bool Delete(int key)
        {
            using (var repository = _sourceFactory.CreateRepository<OrderDetail, int>())
            {
                return repository.Delete(key);
            }
        }

        public OrderDetailViewModel ViewSingle(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<OrderDetailViewModel> ViewAll()
        {
            throw new System.NotImplementedException();
        }
    }
}
