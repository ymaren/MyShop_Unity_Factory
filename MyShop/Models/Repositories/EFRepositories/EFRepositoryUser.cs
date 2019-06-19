using MyShop.Models.MyShopModels;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MyShop.Models.interfaces.Repositories
{
    public class EFRepositoryUser : IGenericRepository<User, int>
    {
        private DBContext _db;

        public EFRepositoryUser()
        {
            _db = new DBContext();
        }

        public IEnumerable<User> GetAll()
        {
            return _db.Users;
        }

        public User GetSingle(int key)
        {
            return _db.Users.Find(key);
        }

        public User Add(User entity)
        {
            return _db.Users.Add(entity);
              
        }

        public bool Update(User entity)
        {
            //User foundUser = _db.Users.Include(c=>c.Credential).FirstOrDefault(c => c.Id == entity.Id);
            //if (foundUser != null)
            //{
            //_db.Users.Attach(entity);
            // foundUser.Credential.AddRange(entity.Credential);
            //foundUser.UserName = entity.UserName;
            //foundUser.UserPassword = entity.UserPassword;
            //foundUser.UserRoleId = entity.UserRoleId;
            //foundUser.UserAddress = entity.UserAddress;


            _db.Entry(entity).State = EntityState.Modified;
            //_db.Entry(foundUser).CurrentValues.SetValues(entity);
                //_db.Entry(foundUser.Credential).Collection.Add(entity.Credential);
                //Save();
            //    return true;
            //}
            return false;
        }
        public bool Delete(int key)
        {
            if (_db.Users.Remove(_db.Users.Find(key)) != null)
                return true;
            return false;
        }
        public bool Delete(User entity)
        {
            if (_db.Users.Remove(entity) != null)
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