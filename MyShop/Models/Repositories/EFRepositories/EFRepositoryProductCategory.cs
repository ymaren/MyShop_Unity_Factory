using MyShop.Models.MyShopModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MyShop.Models.interfaces.Repositories
{
    public class EFRepositoryProductCategory : IGenericRepository<ProductCategory, int>
    {
        private DBContext _db;

        public EFRepositoryProductCategory ()
        {
            _db = new DBContext();
        }       

        public IEnumerable<ProductCategory> GetAll()
        {
          return  _db.ProductCategories;
        }

        public ProductCategory GetSingle(int key)
        {
            return _db.ProductCategories.Find(key);
        }

        public ProductCategory Add(ProductCategory entity)
        {

            return _db.ProductCategories.Add(entity);
            
        }

        public bool Update(ProductCategory entity)
        {
            if (_db.ProductCategories.AsNoTracking().FirstOrDefault(c => c.Id == entity.Id) != null)
            {
                 _db.Entry(entity).State = EntityState.Modified;
                return true;
            }
            return false;
        }
        public bool Delete(int key)
        {
            if (_db.ProductCategories.Remove(_db.ProductCategories.Find(key)) != null)
                return true;
            return false;
        }

        public bool Delete(ProductCategory entity)
        {
            if (_db.ProductCategories.Remove(entity) != null)
                return true;
            return false;
        }

        private bool disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Save()
        {
            _db.SaveChanges();
        }

    }
}