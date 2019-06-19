using MyShop.Models.MyShopModels;
using Store.Logic.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MyShop.Models.interfaces.Repositories
{
    public class EFRepositoryOrder : IGenericRepository<Order, int>
    {
        private DBContext _db;

        public EFRepositoryOrder()
        {
            _db = new DBContext();
        }

        public IEnumerable<Order> GetAll()
        {
            return _db.Orders;
        }

        public Order GetSingle(int key)
        {
            return _db.Orders.Find(key);
        }

        public Order Add(Order entity)
        {
          return _db.Orders.Add(entity);           
        }

        public bool Update(Order entity)
        {
           Order foundOrder = _db.Orders.FirstOrDefault(c => c.Id == entity.Id);
            if (foundOrder != null)
            {
                _db.Entry(foundOrder).CurrentValues.SetValues(entity);
                 return true;
            }
            return false;
        }
        public bool Delete(int key)
        {
            if (_db.Orders.Remove(_db.Orders.Find(key)) != null)
                return true;
            return false;
        }
        public bool Delete(Order entity)
        {
            if (_db.Orders.Remove(entity) != null)
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