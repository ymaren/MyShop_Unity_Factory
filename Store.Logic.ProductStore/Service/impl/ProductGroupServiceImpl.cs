namespace Store.Logic.ProductStore.Service.impl
{
    using Core.Common.Dal;
    using Models.ModifyModels;
    using Models.ViewModels;
    using Service.ModifyServices;
    using Service.ViewServices;
    using Store.Logic.Entity;
    using Store.Logic.ProductStore.Exceptions;
    using System.Collections.Generic;
    using System.Linq;

    internal class ProductGroupServiceImpl : IProductGroupViewService, IProductGroupModifyService
    {
        private readonly IRepositoryFactory _sourceFactory;

        public ProductGroupServiceImpl(IRepositoryFactory sourceFactory)
        {
            _sourceFactory = sourceFactory;
        }


        public IEnumerable<ProductGroupViewModel> ViewAll()
        {
            using (var repGroup = _sourceFactory.CreateRepository<ProductGroup, int>())
            
            {
                var list = repGroup.GetAll().
                    Select(c => new ProductGroupViewModel
                    {
                        Id = c.Id,
                        GroupName = c.GroupName,
                        GroupDescription = c.GroupDescription,
                        ProductCategoryid = c.ProductCategoryid,
                        ProductCategory = new ProductCategoryViewModel(_sourceFactory.CreateRepository<ProductCategory, int>().GetSingle(c.ProductCategoryid))
                    });
                return list;
            }
        }

        public ProductGroupViewModel ViewSingle(int id)
        {
            using (var repGroup = _sourceFactory.CreateRepository<ProductGroup, int>())
            
            {
                var group = repGroup.GetSingle(id);
                var repCategory = _sourceFactory.CreateRepository<ProductCategory, int>();
                var category = repCategory.GetSingle(group.ProductCategoryid);
                return new ProductGroupViewModel
                {
                    Id = group.Id,
                    GroupName = group.GroupName,
                    GroupDescription = group.GroupDescription,
                    ProductCategoryid = group.ProductCategoryid,
                    ProductCategory = new ProductCategoryViewModel(category.Id, category.Name, category.Description)
                };
            }
        }


        public bool Add(ProductGroupModifyModel entity)
        {
            using (var repository = _sourceFactory.CreateRepository<ProductGroup, int>())
            {
                return repository.Add(new ProductGroup
                {
                    GroupName = entity.GroupName,
                    GroupDescription = entity.GroupDescription,
                    ProductCategoryid = entity.ProductCategoryid,
                });

            };
        }

        public bool Update(int id, ProductGroupModifyModel entity)
        {
            using (var repository = _sourceFactory.CreateRepository<ProductGroup, int>())
            {
                var group = repository.GetSingle(id);
                if (group == null)
                    throw new NotFoundException();

                group.GroupName = entity.GroupName;
                group.GroupDescription = entity.GroupDescription;
                group.ProductCategoryid = entity.ProductCategoryid;

                return repository.Update(group);
            }
        }

        public bool Delete(int key)
        {
            using (var repository = _sourceFactory.CreateRepository<ProductGroup, int>())
            {
                return repository.Delete(key);
            }
        }


    }
}
