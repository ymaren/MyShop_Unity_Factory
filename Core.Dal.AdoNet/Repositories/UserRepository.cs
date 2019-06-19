namespace Core.Dal.AdoNet.Repositories
{
    using Core.Dal.AdoNet.Exceptions;
    using Store.Logic.Entity;
    using System;
    using System.Data;

    internal class UserRepository : BaseRepository<User>
    {
        const string IdField = "Id";
        const string UserEmailFild = "UserEmail";
        const string UserPasswordFild = "UserPassword";
        const string UserNameFild = "UserName";
        const string UserCountryFild = "UserCountry";
        const string UserAddressFild = "UserAddress";
        const string UserRoleIdFild = "UserRoleId";

        public UserRepository(IDbConnection connection)
            : base(connection, "dbo.Users")
        {
            BindViewMapper(IdField, (o, e) => e.Id = Convert.ToInt32(o));
            BindViewMapper(UserEmailFild, (o, e) => e.UserEmail = o.ToString());
            BindViewMapper(UserPasswordFild, (o, e) => e.UserPassword = o.ToString());
            BindViewMapper(UserNameFild, (o, e) => e.UserName = o.ToString());
            BindViewMapper(UserCountryFild, (o, e) => e.UserCountry = o.ToString());
            BindViewMapper(UserAddressFild, (o, e) => e.UserAddress = o.ToString());
            BindViewMapper(UserRoleIdFild, (o, e) => e.UserRoleId = Convert.ToInt32(o));
        }

        public override User GetSingle(int key)
        {
        
            var command = Connection.CreateCommand();
            command.CommandText = $"{BaseQuery} where Id ={key};" +
                $"SELECT cred.*   FROM [MyShop].[dbo].[UserRoleCredentials] cr   join [dbo].[Credentials] cred on cr.Credential_Id= cred.Id  where cr.UserRole_Id={key}";

            try
            {
                using (var reader = command.ExecuteReader())
                {
                    User newUser = null;
                    if (reader.Read())
                    {
                        newUser =Map(reader);    
                        
                    }
                    if (reader.NextResult())
                    {
                        while (reader.Read())
                        {
                            newUser.Credential.Add(
                                new Credential(
                               int.Parse(reader["Id"].ToString()),
                               reader["NameCredential"].ToString(),
                               reader["FullNameCredential"].ToString(),
                               int.Parse(reader["ParentCredentialid"].ToString()),
                               int.Parse(reader["Order"].ToString()),
                               reader["URL"].ToString()
                               )
                             );
                        }
                    }

                    return newUser;
                }
            }
            catch (Exception e)
            {
                throw new DalExecutionException("", e);
            }
        
    }

        public override bool Add(User entity)
        {
            var command = Connection.CreateCommand();
            command.CommandText = $"insert into {BaseTable} " +
                $"({UserEmailFild},{UserPasswordFild},{UserNameFild},{UserCountryFild},{UserAddressFild},{UserRoleIdFild} ) values " +
                $"('{entity.UserEmail}', '{entity.UserPassword}', '{entity.UserName}','{entity.UserCountry}','{entity.UserAddress}','{entity.UserRoleId}')";
            try
            {
                
                return command.ExecuteNonQuery() == 1;
            }
            catch (Exception e)
            {
                throw new DalExecutionException("", e);
            }
        }

        public  User AddUser(User entity)
        {
            var command = Connection.CreateCommand();
            command.CommandText = $"insert into {BaseTable} " +
                $"({UserEmailFild},{UserPasswordFild},{UserNameFild},{UserCountryFild},{UserAddressFild},{UserRoleIdFild} ) values " +
                $"('{entity.UserEmail}', '{entity.UserPassword}', '{entity.UserName}','{entity.UserCountry}','{entity.UserAddress}','{entity.UserRoleId}'); " +
                $"SELECT SCOPE_IDENTITY()";
            try
            {
                entity.Id = Convert.ToInt16(command.ExecuteScalar());
                return entity;
                //command.ExecuteNonQuery() == 1;
            }
            catch (Exception e)
            {
                throw new DalExecutionException("", e);
            }
        }


        public override bool Update(User entity)
        {
            var command = Connection.CreateCommand();
            command.CommandText = $"update {BaseTable} " +
                $"set " +
                $"{UserEmailFild}='{entity.UserEmail}', " +
                $"{UserPasswordFild}='{entity.UserPassword}', " +
                $"{UserNameFild}='{entity.UserName}', " +
                $"{UserCountryFild}='{entity.UserCountry}', " +
                $"{UserAddressFild}='{entity.UserAddress}', " +
                $"{UserRoleIdFild}='{entity.UserRoleId}' " +
                $" where Id={entity.Id}";
            try
            {
                return command.ExecuteNonQuery() == 1;
            }
            catch (Exception e)
            {
                throw new DalExecutionException("", e);
            }
        }
    }
}
