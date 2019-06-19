using MyShop.Models.MyShopModels;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MyShop.Models.interfaces.Repositories
{
    public class EFRepositoryProduct : IGenericRepository<Product, int>
    {
        private DBContext _db;

        public EFRepositoryProduct()
        {
            _db = new DBContext();
        }

        public IEnumerable<Product> GetAll()
        {
            return _db.Products;
        }

        public Product GetSingle(int key)
        {
            return _db.Products.Find(key);
        }

        public Product Add(Product entity)
        {
          return _db.Products.Add(entity);           
        }

        public bool Update(Product entity)
        {
            if (_db.Products.AsNoTracking().FirstOrDefault(c => c.Id == entity.Id) != null)
            {
                _db.Entry(entity).State = EntityState.Modified;
                return true;
            }
            return false;
        }
        public bool Delete(int key)
        {
            if (_db.Products.Remove(_db.Products.Find(key)) != null)
                return true;
            return false;
        }
        public bool Delete(Product entity)
        {
            if (_db.Products.Remove(entity) != null)
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