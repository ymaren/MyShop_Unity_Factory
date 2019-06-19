using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Models.interfaces.Repositories
{
    public interface IGenericRepository<TEntity, TKey> : IDisposable
         where TEntity : class
    {
        TEntity GetSingle(TKey key);

        IEnumerable<TEntity> GetAll();

        TEntity Add(TEntity entity);

        bool Update(TEntity entity);

        bool Delete(TKey key);
        bool Delete(TEntity entity);
        void Save();
    }
}
