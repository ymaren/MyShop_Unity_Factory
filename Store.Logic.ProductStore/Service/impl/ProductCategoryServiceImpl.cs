﻿namespace Store.Logic.ProductStore.Service.impl
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

    internal class ProductCategoryServiceImpl : IProductCategoryViewService, IProductCategoryModifyService
    {
        private readonly IRepositoryFactory _sourceFactory;

        public ProductCategoryServiceImpl(IRepositoryFactory sourceFactory)
        {
            _sourceFactory = sourceFactory;
        }
        

        public IEnumerable<ProductCategoryViewModel> ViewAll()
        {
            using (var repository = _sourceFactory.CreateRepository<ProductCategory, int>())
            {
                var allGroups = _sourceFactory.CreateRepository<ProductGroup, int>().GetAll().
                    Select(c => new ProductGroupViewModel
                    {
                        Id = c.Id,
                        GroupName = c.GroupName,
                        GroupDescription = c.GroupDescription,
                        ProductCategoryid = c.ProductCategoryid

                    }).ToList();
                var list = repository.GetAll().
                    Select(c => new ProductCategoryViewModel
                    {
                        Id = c.Id,
                        CategoryName = c.Name,
                        CategoryDescription = c.Description,
                        ProductGroups = allGroups.Where(g => g.ProductCategoryid == c.Id).ToList()

                    }
                    );
                return list;
            }
        }

        public ProductCategoryViewModel ViewSingle(int id)
        {
            using (var repository = _sourceFactory.CreateRepository<ProductCategory, int>())
            {
                var category = repository.GetSingle(id);

                return new ProductCategoryViewModel
                {
                    Id = category.Id,
                    CategoryName = category.Name,
                    CategoryDescription = category.Description
                };
            }
        }


        public bool Add(ProductCategoryModifyModel entity)
        {
            using (var repository = _sourceFactory.CreateRepository<ProductCategory, int>())
            {
                return repository.Add(new ProductCategory
                {
                    Name = entity.Name,
                    Description = entity.Description                    
                });

            };
        }

        public bool Update(int id, ProductCategoryModifyModel entity)
        {
            using (var repository = _sourceFactory.CreateRepository<ProductCategory, int>())
            {
                var category = repository.GetSingle(id);
                if (category == null)
                    throw new NotFoundException();
                //Here logic of updateing existed entity
                category.Name = entity.Name;
                category.Description = entity.Description;

                return repository.Update(category);
            }
        }

        public bool Delete(int key)
        {
            using (var repository = _sourceFactory.CreateRepository<ProductCategory, int>())
            {
                return repository.Delete(key);
            }
        }


       

        public bool Delete(int key, out string reason)
        {
            throw new System.NotImplementedException();
        }

        

        

       
    }
}
