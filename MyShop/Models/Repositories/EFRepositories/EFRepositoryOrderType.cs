using MyShop.Models.MyShopModels;
using Store.Logic.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MyShop.Models.interfaces.Repositories
{
    public class EFRepositoryOrderType : IGenericRepository<OrderType, int>
    {
        private DBContext _db;

        public EFRepositoryOrderType()
        {
            _db = new DBContext();
        }

        public IEnumerable<OrderType> GetAll()
        {
            return _db.OrderTypes;
        }

        public OrderType GetSingle(int key)
        {
            return _db.OrderTypes.Find(key);
        }

        public OrderType Add(OrderType entity)
        {
          return _db.OrderTypes.Add(entity);           
        }

        public bool Update(OrderType entity)
        {
           OrderType foundOrder = _db.OrderTypes.FirstOrDefault(c => c.Id == entity.Id);
            if (foundOrder != null)
            {
                _db.Entry(foundOrder).CurrentValues.SetValues(entity);
                 return true;
            }
            return false;
        }
        public bool Delete(int key)
        {
            if (_db.OrderTypes.Remove(_db.OrderTypes.Find(key)) != null)
                return true;
            return false;
        }
        public bool Delete(OrderType entity)
        {
            if (_db.OrderTypes.Remove(entity) != null)
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