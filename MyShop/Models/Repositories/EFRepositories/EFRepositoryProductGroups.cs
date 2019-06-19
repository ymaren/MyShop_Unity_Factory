using MyShop.Models.MyShopModels;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MyShop.Models.interfaces.Repositories
{
    public class EFRepositoryProductGroups : IGenericRepository<ProductGroup, int>
    {
        private DBContext _db;

        public EFRepositoryProductGroups()
        {
            _db = new DBContext();
        }

        public IEnumerable<ProductGroup> GetAll()
        {
            //_db.Configuration.AutoDetectChangesEnabled = false;
            //_db.Configuration.ProxyCreationEnabled = false;
            return _db.ProductGroups;
        }

        public ProductGroup GetSingle(int key)
        {
            return _db.ProductGroups.Find(key);
        }

        public ProductGroup Add(ProductGroup entity)
        {
            return _db.ProductGroups.Add(entity);
              
        }

        public bool Update(ProductGroup entity)
        {
            if (_db.ProductGroups.AsNoTracking().FirstOrDefault(c => c.Id == entity.Id) != null)
            {
                _db.Entry(entity).State = EntityState.Modified;
                return true;
            }
            return false;
        }
        public bool Delete(int key)
        {
            if (_db.ProductGroups.Remove(_db.ProductGroups.Find(key)) != null)
                return true;
            return false;
        }
        public bool Delete(ProductGroup entity)
        {
            if (_db.ProductGroups.Remove(entity) != null)
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