using MyShop.Models.MyShopModels;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MyShop.Models.interfaces.Repositories
{
    public class EFRepositoryUserRole : IGenericRepository<UserRole, int>
    {
        private DBContext _db;


        public EFRepositoryUserRole()
        {
            _db = new DBContext();
        }

        public IEnumerable<UserRole> GetAll()
        {
            return _db.UserRoles;
        }

        public UserRole GetSingle(int key)
        {
            return _db.UserRoles.Find(key);
        }

        public UserRole Add(UserRole entity)
        {
            entity.Credential.ForEach(c => _db.Entry(c).State = EntityState.Unchanged);
            return _db.UserRoles.Add(entity);
              
        }

        public bool Update( UserRole entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            //foreach (var cred in entity.Credential)
            //{
            //    string a = _db.Entry(cred).State.ToString();
            //}

            entity.Credential.ForEach(c =>  _db.Entry(c).State= EntityState.Unchanged);
            _db.SaveChanges();
            return true;
        }










            //var dbEntry = _db.Entry(entity);
            //UserRole userRole = _db.UserRoles.FirstOrDefault(c => c.Id == entity.Id);
            //if (userRole != null)
            //{
            //    //_db.Credentials.Attach(userRole.Credential);
                
            //    userRole.Credential = entity.Credential;
            //    foreach (var cred in userRole.Credential)
            //    {
            //        string st = _db.Entry(cred).State.ToString();
            //    }
            //    //_db.Entry(userRole).CurrentValues.SetValues(entity);

            //    _db.Entry(entity).State = EntityState.Modified;
            //    Save();
            //    return true;
            
            //return false;
        
        public bool Delete(int key)
        {
            if (_db.UserRoles.Remove(_db.UserRoles.Find(key)) != null)
                return true;
            return false;
        }
        public bool Delete(UserRole entity)
        {
            if (_db.UserRoles.Remove(entity) != null)
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