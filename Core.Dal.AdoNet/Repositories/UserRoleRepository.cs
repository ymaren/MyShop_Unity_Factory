namespace Core.Dal.AdoNet.Repositories
{
    using Core.Dal.AdoNet.Exceptions;
    using Store.Logic.Entity;
    using System;
    using System.Data;

    internal class UserRoleRepository : BaseRepository<UserRole>
    {
        const string IdField = "Id";
        const string UserRoleNameFild = "UserRoleName";
        

        public UserRoleRepository(IDbConnection connection)
            : base(connection, "dbo.UserRoles")
        {
            BindViewMapper(IdField, (o, e) => e.Id = Convert.ToInt32(o));
            BindViewMapper(UserRoleNameFild, (o, e) => e.UserRoleName = o.ToString());
           
        }

        public override UserRole GetSingle(int key)
        {
        
            var command = Connection.CreateCommand();
            command.CommandText = $"{BaseQuery} where Id ={key};" +
                $"SELECT cred.*   FROM [MyShop].[dbo].[UserRoleCredentials] cr   join [dbo].[Credentials] cred on cr.Credential_Id= cred.Id  where cr.UserRole_Id={key}";

            try
            {
                using (var reader = command.ExecuteReader())
                {
                    UserRole newUserRole = null;
                    if (reader.Read())
                    {
                        newUserRole =Map(reader);    
                        
                    }
                    if (reader.NextResult())
                    {
                        while (reader.Read())
                        {
                            newUserRole.RoleCredential.Add(
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

                    return newUserRole;
                }
            }
            catch (Exception e)
            {
                throw new DalExecutionException("", e);
            }
        
    }

        public override bool Add(UserRole entity)
        {
            var command = Connection.CreateCommand();
            command.CommandText = $"insert into {BaseTable} " +
                $"({UserRoleNameFild}) values " +
                $"('{entity.UserRoleName}') ";
            try
            {
                return command.ExecuteNonQuery() == 1;
            }
            catch (Exception e)
            {
                throw new DalExecutionException("", e);
            }
        }

        public override bool Update(UserRole entity)
        {
            var command = Connection.CreateCommand();
            command.CommandText = $"update {BaseTable} " +
                $"set " +
                $"{UserRoleNameFild}='{entity.UserRoleName}' " +
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
