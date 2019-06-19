
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
            string connectionString = "Data Source=WS-LV-CP2365\\SQL2005; Initial Catalog=MyShop; Persist Security Info=true;Max Pool Size=50000;Pooling=True; User ID=sa; Password=Sywengony2005";
            container.RegisterType<IObjectFactory, InternalDefaultObjectFactory>(
             new InjectionConstructor(new AdoNetRepositoryFactory(connectionString)));
            container.RegisterType<IRepositoryFactory, AdoNetRepositoryFactory>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}