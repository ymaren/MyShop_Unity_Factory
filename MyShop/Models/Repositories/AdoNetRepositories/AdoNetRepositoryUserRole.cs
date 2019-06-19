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
    public class AdoNetRepositoryUserRole : BaseRepository<UserRole>
    {
        public AdoNetRepositoryUserRole()
            : base("dbo.UserRoles")
        {           

        }

        public override IEnumerable<UserRole> GetAll()
        {
            try
            {
                string query = "SELECT uRole.*, cred.Id as [CredId],cred.Id, cred.NameCredential,cred.FullNameCredential, cred.ParentCredentialId, cred.[Order], cred.Url FROM[dbo].[UserRoles] uRole left join[dbo].[UserRoleCredentials] uRoleCred on uRole.Id=uRoleCred.UserRole_Id left join[dbo].[Credentials] cred on uRoleCred.Credential_Id= cred.Id";
                dynamic test = Connection.Query<dynamic>(query);

                var listUserRoleInclideCredential = Connection.Query<UserRole, Credential, UserRole>(
                    query, (user_role, credential) => { user_role.Credential.Add(credential); return user_role; }, splitOn: "CredId")                
                 .GroupBy(o => o.Id).Select(c =>
                {
                    
                    var userRole = c.First();

                    var credentials = c.Select(p => p.Credential.Single()).ToList();
                    userRole.Credential = credentials.Any(g=>g.NameCredential!=null)? credentials.ToList(): new List<Credential>() { };
                   return userRole;
                });
                return listUserRoleInclideCredential;

            }
            catch (Exception ex)
            {
                throw new DalExecutionException("", ex);
            }

        }

        public override UserRole GetSingle(int key)
        {
            try
            {
                string query = string.Format("SELECT uRole.*, cred.Id as [CredId],cred.Id, cred.NameCredential,cred.FullNameCredential, cred.ParentCredentialId, cred.[Order], cred.Url FROM[dbo].[UserRoles] uRole left join[dbo].[UserRoleCredentials] uRoleCred on uRole.Id=uRoleCred.UserRole_Id left join[dbo].[Credentials] cred on uRoleCred.Credential_Id= cred.Id where uRole.Id={0} ", key);
                 var listUserRoleInclideCredential = Connection.Query<UserRole, Credential, UserRole>(
                    query, (user_role, credential) => { user_role.Credential.Add(credential); return user_role; }, splitOn: "CredId")
                 .GroupBy(o => o.Id).Select(c =>
                 {

                     var userRole = c.First();

                     var credentials = c.Select(p => p.Credential.Single()).ToList();
                     userRole.Credential = credentials.Any(g => g.NameCredential != null) ? credentials.ToList() : new List<Credential>() { };
                     return userRole;
                 });
                return listUserRoleInclideCredential.FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw new DalExecutionException("", ex);
            }

        }

        public override UserRole Add(UserRole entity)
        {
            var addNewUserRole = Connection.Execute("Insert into UserRoles (UserRoleName) values (@UserRoleName)", new { UserRoleName = entity.UserRoleName});
            return entity;
        }        

        public override bool Update(UserRole entity)
        {
            var updateUserRole = Connection.Execute("Update UserRoles set UserRoleName = @UserRoleName Where Id = @Id ", new { UserRoleName = entity.UserRoleName, id=entity.Id });
            UserRole currentRoleinDB=  this.GetSingle(entity.Id);
            var difCredAdd = entity.Credential.Where(c => !currentRoleinDB.Credential.Any(cred => cred.Id == c.Id));
            var difCredDeleted = currentRoleinDB.Credential.Where(c => !entity.Credential.Any(cred => cred.Id == c.Id));

            difCredAdd.ToList().ForEach(c =>
            Connection.Execute("Insert into UserRoleCredentials (UserRole_Id ,Credential_Id) " +
                "values (@UserRole_Id,  @Credential_Id)", new { UserRole_Id = currentRoleinDB.Id, Credential_Id = c.Id}));

            difCredDeleted.ToList().ForEach(c =>
            Connection.Execute("delete UserRoleCredentials where UserRole_Id=@UserRole_Id and Credential_Id= @Credential_Id", new { UserRole_Id = currentRoleinDB.Id, Credential_Id = c.Id }));

            return true;
        }
    }
}