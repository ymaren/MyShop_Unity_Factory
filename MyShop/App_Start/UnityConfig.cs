
using Core.Common.Dal;
using Core.Dal.AdoNet;
using MyShop.Providers;
using Store.Logic.ProductStore.Factories;
using Store.Logic.ProductStore.Infustructure;
//using Store.Logic.ProductStore.Factories;
//using Store.Logic.ProductStore.Infustructure;
using System.Web.Mvc;
using Unity;
using Unity.Injection;
using Unity.Mvc5;

namespace MyShop
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            //container.RegisterType<IGenericRepository<ProductCategory, int>, EFRepositoryProductCategory>();
            //container.RegisterType<IGenericRepository<ProductGroup, int>, EFRepositoryProductGroups>();
            //container.RegisterType<IGenericRepository<Product, int>, EFRepositoryProduct>();
            //container.RegisterType<IGenericRepository<User, int>, EFRepositoryUser>();
            //container.RegisterType<IGenericRepository<UserRole, int>, EFRepositoryUserRole>();
            //container.RegisterType<IGenericRepository<Credential, int>, EFRepositoryCredential>();
            //container.RegisterType<IGenericRepository<Order, int>, EFRepositoryOrder>();
            //container.RegisterType<IGenericRepository<OrderType, int>, EFRepositoryOrderType>();

            //container.RegisterType<IObjectFactory, InternalDefaultObjectFactory>();
            //container.RegisterType<IRepositoryFactory, AdoNetRepositoryFactory>();
            string connectionString = "Data Source=WS-LV-CP2365\\SQL2005; Initial Catalog=MyShop; Persist Security Info=true;Max Pool Size=50000;Pooling=True; User ID=sa; Password=Sywengony2005";
            container.RegisterType<IObjectFactory, InternalDefaultObjectFactory>(
             new InjectionConstructor(new AdoNetRepositoryFactory(connectionString)));
            container.RegisterType<IRepositoryFactory, AdoNetRepositoryFactory>();
            
            //IObjectFactory drv = container.Resolve<IObjectFactory>();

            //container.RegisterType<IGenericRepository<ProductCategory, int>, AdoNetRepositoryProductCategory>();
            //container.RegisterType<IGenericRepository<ProductGroup, int>, AdoNetRepositoryGroups>();
            //container.RegisterType<IGenericRepository<Product, int>, AdoNetRepositoryProduct>();
            //container.RegisterType<IGenericRepository<UserRole, int>, AdoNetRepositoryUserRole>();
            //container.RegisterType<IGenericRepository<User, int>, AdoNetRepositoryUser>();
            //container.RegisterType<IGenericRepository<OrderType, int>, AdoNetRepositoryOrderType>();
            //container.RegisterType<IGenericRepository<Order, int>, AdoNetRepositoryOrder>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}