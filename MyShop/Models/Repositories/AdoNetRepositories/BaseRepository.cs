using Dapper;
using MyShop.Models.interfaces.Repositories;
using MyShop.Models.Repositories.AdoNetRepositories.Exceptions;
using MyShop.Models.Repositories.AdoNetRepositories.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace MyShop.Models.Repositories.AdoNetRepositories
{
    public abstract class BaseRepository<TEntity> : IGenericRepository<TEntity, int>
         where TEntity : class, new()
    {
        string connectionString = WebConfigurationManager.ConnectionStrings["DefaultAdoNet"].ConnectionString;
        private readonly IDbConnection _connection;
        private readonly List<IPropertyMapper<TEntity>> _mappers = new List<IPropertyMapper<TEntity>>();

        protected string BaseTable { get; }

        protected string BaseQuery => $"select * from {BaseTable}";


        protected IDbConnection Connection
        {
            get
            {
                if (_connection.State != ConnectionState.Open)
                    _connection.Open();

                return _connection;
            }
        }

        public BaseRepository(string tableName)
        {
            _connection = new SqlConnection(connectionString);
            BaseTable = tableName;
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
           
            try
            {               
                    var obj = _connection.Query<TEntity>(BaseQuery);
                    return obj;
                
            }
            catch (Exception ex)
            {
                throw new DalExecutionException("", ex);
            }
        }

        public virtual TEntity GetSingle(int key)
        {
           

            try
            {
                var obj = _connection.Query<TEntity>($"{BaseQuery} where Id ={key}").FirstOrDefault();
                return obj;
            }
            catch (Exception e)
            {
                throw new DalExecutionException("", e);
            }
        }

        public bool Delete(int key)
        {
            var command = Connection.CreateCommand();
            command.CommandText = $"delete {BaseTable} where Id={key}";

            try
            {
                return command.ExecuteNonQuery() == 1;
            }
            catch (Exception e)
            {
                throw new DalExecutionException("", e);
            }
        }

        public bool Delete(TEntity entity)
        {
            var command = Connection.CreateCommand();
            command.CommandText = $"delete {BaseTable} where Id={4}";

            try
            {
                return command.ExecuteNonQuery() == 1;
            }
            catch (Exception e)
            {
                throw new DalExecutionException("", e);
            }
        }



        public void Dispose()
        {
            _connection?.Dispose();
        }

        //public abstract bool Add(TEntity entity);

        public abstract bool Update(TEntity entity);

       
                

        public void Save()
        {
            return;
        }

        public abstract TEntity Add(TEntity entity);
    }
}