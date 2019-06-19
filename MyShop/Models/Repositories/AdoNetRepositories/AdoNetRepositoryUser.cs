using Dapper;
using MyShop.Models.MyShopModels;
using MyShop.Models.Repositories.AdoNetRepositories.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MyShop.Models.Repositories.AdoNetRepositories
{
    public class AdoNetRepositoryUser: BaseRepository<User>
    {
        public AdoNetRepositoryUser()
            : base("dbo.Users")
        {           

        }

        public override IEnumerable<User> GetAll()
        {
            try
            {
                string query = "SELECT users.*, userRole.Id as[RoleId], userRole.Id , userRole.UserRoleName, cred.Id as [CredId],cred.Id, cred.NameCredential,cred.FullNameCredential, cred.ParentCredentialId, cred.[Order], cred.Url FROM[dbo].[Users] users join [dbo].[UserRoles] userRole on users.UserRoleId= userRole.Id left join[dbo].[UserCredentials] userCred on users.Id=userCred.User_Id left  join[dbo].[Credentials] cred on userCred.Credential_Id= cred.Id";
                

                var listUserInclideCredential = Connection.Query<User, UserRole, Credential, User>(
                    query, (user,userRole, credential) => { user.UserRole = userRole; user.Credential.Add(credential); return user; }, splitOn: "RoleId,CredId")                
                 .GroupBy(o => o.Id).Select(c =>
                {
                    
                    var user = c.First();

                    var credentials = c.Select(p => p.Credential.Single()).ToList();
                    user.UserRole = c.Select(p => p.UserRole).FirstOrDefault();
                    user.Credential = credentials.Any(g=>g.NameCredential!=null)? credentials.ToList(): new List<Credential>() { };
                   return user;
                });
                return listUserInclideCredential;

            }
            catch (Exception ex)
            {
                throw new DalExecutionException("", ex);
            }

        }

        public override User GetSingle(int key)
        {
            try
            {
                
                string query = string.Format("SELECT users.*, cred.Id as [CredId],cred.Id, cred.NameCredential,cred.FullNameCredential, cred.ParentCredentialId, cred.[Order], cred.Url FROM[dbo].[Users] users left join[dbo].[UserCredentials] userCred on users.Id = userCred.User_Id left join[dbo].[Credentials] cred on userCred.Credential_Id= cred.Id where  users.Id={0} ", key);
                var listUserInclideCredential = Connection.Query<User, Credential, User>(
                query, (user, credential) => { user.Credential.Add(credential); return user; }, splitOn: "CredId")                
                 .GroupBy(o => o.Id).Select(c =>
                  {

                      var user = c.First();

                      var credentials = c.Select(p => p.Credential.Single()).ToList();
                      user.Credential = credentials.Any(g => g.NameCredential != null) ? credentials.ToList() : new List<Credential>() { };
                      return user;
                  });
                return listUserInclideCredential.FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw new DalExecutionException("", ex);
            }

        }

        public override User Add(User entity)
        {
            var addNewUser = Connection.Execute("Insert into Users (UserEmail,UserPassword,UserName,UserCountry,UserAddress, UserRoleId) values (@UserEmail,@UserPassword,@UserName,@UserCountry,@UserAddress, @UserRoleId)", 
                new { UserEmail = entity.UserEmail,UserPassword = entity.UserPassword, UserName = entity.UserName, UserCountry = entity.UserCountry, UserAddress = entity.UserAddress, UserRoleId = entity.UserRoleId });

            //entity.Credential.ToList().ForEach(c =>
            //Connection.Execute("Insert into UserCredentials (User_Id ,Credential_Id)" +
            //    "values (@User_Id,  @Credential_Id)", new { User_Id = entity.Id, Credential_Id = c.Id }));


            return entity;
        }        

        public override bool Update(User entity)
        {
            var updateUser = Connection.Execute("Update Users set UserEmail = @UserEmail, UserPassword = @UserPassword," +
                "UserName = @UserName," +
                "UserCountry = @UserCountry," +
                "UserAddress = @UserAddress" +
                " Where Id = @Id ", 
                new { UserEmail = entity.UserEmail, UserPassword = entity.UserPassword, UserName = entity.UserName, UserCountry = entity.UserCountry, UserAddress = entity.UserAddress, id =entity.Id });
            User currentUserDB=  this.GetSingle(entity.Id);
            var difCredAdd = entity.Credential.Where(c => !currentUserDB.Credential.Any(cred => cred.Id == c.Id));
            var difCredDeleted = currentUserDB.Credential.Where(c => !entity.Credential.Any(cred => cred.Id == c.Id));

            difCredAdd.ToList().ForEach(c =>
            Connection.Execute("Insert into UserCredentials (User_Id ,Credential_Id) " +
                "values (@User_Id,  @Credential_Id)", new { User_Id = currentUserDB.Id, Credential_Id = c.Id}));

            difCredDeleted.ToList().ForEach(c =>
            Connection.Execute("delete UserCredentials where User_Id=@User_Id and Credential_Id= @Credential_Id", new { User_Id = currentUserDB.Id, Credential_Id = c.Id }));

            return true;
        }
    }
}