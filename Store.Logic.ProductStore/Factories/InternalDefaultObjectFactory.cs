namespace Store.Logic.ProductStore.Factories
{
    using Core.Common.Dal;
    using Infustructure;
    using Service.impl;
    using Service.ModifyServices;
    using Service.ViewServices;
    using System;
    using System.Collections.Generic;

    public class InternalDefaultObjectFactory : IObjectFactory
    {
        private readonly IRepositoryFactory _sourceFactory;

        public InternalDefaultObjectFactory(IRepositoryFactory sourceFactory)
        {
            _sourceFactory = sourceFactory;
        }
        

        private readonly static Dictionary<Type, Func<IRepositoryFactory, IObject>> _supportedObjects = new Dictionary<Type, Func<IRepositoryFactory, IObject>>
        {
            [typeof(IProductViewService)] = (factory) => new ProductServiceImpl(factory),
            [typeof(IProductModifyService)] = (factory) => new ProductServiceImpl(factory),
            [typeof(IProductCategoryViewService)] = (factory) => new ProductCategoryServiceImpl(factory),
            [typeof(IProductCategoryModifyService)] = (factory) => new ProductCategoryServiceImpl(factory),
            [typeof(IProductGroupViewService)] = (factory) => new ProductGroupServiceImpl(factory),
            [typeof(IProductGroupModifyService)] = (factory) => new ProductGroupServiceImpl(factory),
            [typeof(IUserRoleViewService)] = (factory) => new UserRoleServiceImpl(factory),
            [typeof(IUserRoleModifyService)] = (factory) => new UserRoleServiceImpl(factory),
            [typeof(ICredentionalViewService)] = (factory) => new CredentialServiceImpl(factory),
            [typeof(IOrderTypeViewService)] = (factory) => new OrderTypeServiceImpl(factory),
            [typeof(IOrderTypeModifyService)] = (factory) => new OrderTypeServiceImpl(factory),

            [typeof(IUserViewService)] = (factory) => new UserServiceImpl(factory),
            [typeof(IUserModifyService)] = (factory) => new UserServiceImpl(factory),

            [typeof(IOrderViewService)] = (factory) => new OrderServiceImpl(factory),
            [typeof(IOrderModifyService)] = (factory) => new OrderServiceImpl(factory)

        };

        public TObject Create<TObject>() where TObject : IObject
        {
            Func<IRepositoryFactory, IObject> delegateFactory = null;
            if (_supportedObjects.TryGetValue(typeof(TObject), out delegateFactory))
            {
                return (TObject)delegateFactory(_sourceFactory);
            }

            throw new System.NotImplementedException();
        }
    }
}
