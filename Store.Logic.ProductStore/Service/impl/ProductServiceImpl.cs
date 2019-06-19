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

    internal class ProductServiceImpl : IProductViewService, IProductModifyService
    {
        private readonly IRepositoryFactory _sourceFactory;

        public ProductServiceImpl(IRepositoryFactory sourceFactory)
        {
            _sourceFactory = sourceFactory;
        }


        public IEnumerable<ProductViewModel> ViewAll()
        {
            using (var repProduct = _sourceFactory.CreateRepository<Product, int>())
            
            {
                var list = repProduct.GetAll().
                    Select(c => new ProductViewModel
                    {
                        Id = c.Id,
                        ProductCode = c.ProductCode,
                        Name = c.Name,
                        Price = c.Price,
                        Description = c.Description,
                        ProductGroupId = c.ProductGroupId,
                        ProductGroup = new ProductGroupViewModel(_sourceFactory.CreateRepository<ProductGroup, int>().GetSingle(c.ProductGroupId))
                    });
                return list;
            }
        }

        public ProductViewModel ViewSingle(int id)
        {
            using(var repProduct = _sourceFactory.CreateRepository<Product, int>())
            using (var repGroup = _sourceFactory.CreateRepository<ProductGroup, int>())
            {
                var product = repProduct.GetSingle(id);
                var group = repGroup.GetSingle(product.ProductGroupId);
                return new ProductViewModel
                {
                    Id = product.Id,
                    ProductCode = product.ProductCode,
                    Name = product.Name,
                    Price=product.Price,
                    Description = product.Description,
                    ProductGroupId = product.ProductGroupId,
                    ProductGroup = new ProductGroupViewModel(repGroup.GetSingle(product.ProductGroupId))
                };
            }
        }


        public bool Add(ProductModifyModel entity)
        {
            using (var repository = _sourceFactory.CreateRepository<Product, int>())
            {
                return repository.Add(new Product
                {
                    
                    ProductCode = entity.ProductCode,
                    Name = entity.Name,
                    Price = entity.Price,
                    Description = entity.Description,
                    ProductGroupId = entity.ProductGroupId,
                });

            };
        }

        public bool Update(int id, ProductModifyModel entity)
        {
            using (var repository = _sourceFactory.CreateRepository<Product, int>())
            {
                var product = repository.GetSingle(id);
                if (product == null)
                    throw new NotFoundException();
                product.ProductCode = entity.ProductCode;
                product.Name = entity.Name;
                product.Price = entity.Price;
                product.Description = entity.Description;
                product.ProductGroupId = entity.ProductGroupId;

                return repository.Update(product);
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
