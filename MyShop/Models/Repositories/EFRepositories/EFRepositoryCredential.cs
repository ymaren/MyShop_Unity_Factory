using MyShop.Models.MyShopModels;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MyShop.Models.interfaces.Repositories
{
    public class EFRepositoryCredential : IGenericRepository<Credential, int>
    {
        private DBContext _db;

        public EFRepositoryCredential()
        {
            _db = new DBContext();
        }

        public IEnumerable<Credential> GetAll()
        {
           Credential cred= _db.Credentials.FirstOrDefault();
            return _db.Credentials;
        }



        public Credential GetSingle(int key)
        {
            return _db.Credentials.Find(key);
        }

        public Credential Add(Credential entity)
        {
            return _db.Credentials.Add(entity);
              
        }

        public bool Update(Credential entity)
        {
            if (_db.Credentials.AsNoTracking().FirstOrDefault(c => c.Id == entity.Id) != null)
            {
                _db.Entry(entity).State = EntityState.Modified;
                return true;
            }
            return false;
        }
        public bool Delete(int key)
        {
            if (_db.Credentials.Remove(_db.Credentials.Find(key)) != null)
                return true;
            return false;
        }
        public bool Delete(Credential entity)
        {
            if (_db.Credentials.Remove(entity) != null)
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