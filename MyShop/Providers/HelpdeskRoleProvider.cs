using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Data.Entity;
using Store.Logic.ProductStore.Infustructure;
using Store.Logic.ProductStore.Service.ViewServices;
using Store.Logic.ProductStore.Models.ViewModels;

using Store.Logic.ProductStore.Factories;
using Unity.Attributes;
using Core.Dal.AdoNet;

namespace MyShop.Providers
{
    public class HelpdeskRoleProvider : RoleProvider
    {
        string connectionString = "Data Source=WS-LV-CP2365\\SQL2005; Initial Catalog=MyShop; Persist Security Info=true;Max Pool Size=50000;Pooling=True; User ID=sa; Password=Sywengony2005";
        private IObjectFactory _objectFactory;
        //[Dependency("default")]
        //public IObjectFactory objectFactory { get { return _objectFactory; } set { _objectFactory = value; } }
        private readonly IUserViewService _userViewService;

        public HelpdeskRoleProvider()
        {
            _objectFactory = new InternalDefaultObjectFactory(new AdoNetRepositoryFactory(connectionString));
            _userViewService = _objectFactory.Create<IUserViewService>();
        }
        public override string[] GetRolesForUser(string username)
        {
             string[] role = new string[] { };
              try
                {
                    // Get user
                    UserViewModel user = _userViewService.ViewAll().FirstOrDefault(c => c.UserEmail == username);
                    if (user != null)
                    {
                    user = _userViewService.ViewSingle(user.Id);
                        if (user.Credential != null)
                        {
                            role = user.Credential.Select(c => c.NameCredential).ToArray();
                        }
                    }
                }
                catch
                {
                    role = new string[] { };
                }
            
            return role;
        }
        public override bool IsUserInRole(string username, string roleName)
        {
            bool outputResult = false;
            // Находим пользователя
            
                try
                {
                // Получаем пользователя
                UserViewModel user = _userViewService.ViewAll().FirstOrDefault(c => c.UserEmail == username);
                if (user != null)
                    {
                        // получаем роль
                        UserRoleViewModel userRole = user.UserRole;

                        //сравниваем
                        if (userRole != null && userRole.UserRoleName == roleName)
                        {
                            outputResult = true;
                        }
                    }
                }
                catch
                {
                    outputResult = false;
                }
            
            return outputResult;
        }

        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }
        
        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}