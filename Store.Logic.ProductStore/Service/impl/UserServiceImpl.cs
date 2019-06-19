namespace Store.Logic.ProductStore.Service.impl
{
    using Core.Common.Dal;
    using Models.ModifyModels;
    using Models.ViewModels;
    using Service.ModifyServices;
    using Service.ViewServices;
    using Store.Logic.Entity;
    using Store.Logic.ProductStore.Exceptions;
    using System.Collections.Generic;
    using System.Linq;

    internal class UserServiceImpl : IUserViewService, IUserModifyService
    {
        private readonly IRepositoryFactory _sourceFactory;

        public UserServiceImpl(IRepositoryFactory sourceFactory)
        {
            _sourceFactory = sourceFactory;
        }


        public IEnumerable<UserViewModel> ViewAll()
        {
            using (var repUser = _sourceFactory.CreateRepository<User, int>())
            {
                var list = repUser.GetAll().
                    Select(c => new UserViewModel
                    {
                        Id = c.Id,
                        UserEmail = c.UserEmail,
                        UserPassword = c.UserPassword,
                        UserName = c.UserName,
                        UserCountry = c.UserCountry,
                        UserAddress = c.UserAddress,
                        UserRole = new UserRoleViewModel(_sourceFactory.CreateRepository<UserRole, int>().GetSingle(c.UserRoleId))
                    });
                return list;
            }
        }

        public UserViewModel ViewSingle(int id)
        {
            using (var repUser = _sourceFactory.CreateRepository<User, int>())
            {
                var user = repUser.GetSingle(id);

                return new UserViewModel
                {
                    Id = user.Id,
                    UserEmail = user.UserEmail,
                    UserPassword = user.UserPassword,
                    UserName = user.UserName,
                    UserCountry = user.UserCountry,
                    UserAddress = user.UserAddress,
                    Credential = user.Credential.Select(c =>
                                       new CredentionalViewModel(c.Id, c.NameCredential, c.FullNameCredential, c.Order, c.ParentCredentialId, c.Url)).ToList()
                };
            }
        }


        public bool Add(UserModifyModel entity)
        {
            using (var repository = _sourceFactory.CreateRepository<User, int>())
            {
                return repository.Add(
                new User
                {

                    UserEmail = entity.UserEmail,
                    UserPassword = entity.UserPassword,
                    UserName = entity.UserName,
                    UserCountry = entity.UserCountry,
                    UserAddress = entity.UserAddress,
                    UserRoleId = entity.UserRoleId
                }
                    );
            };
        }

        
        public bool Update(int id, UserModifyModel entity)
        {
            using (var repository = _sourceFactory.CreateRepository<User, int>())
            {
                var user = repository.GetSingle(id);
                if (user == null)
                    throw new NotFoundException();
                user.UserEmail = entity.UserEmail;
                user.UserPassword = entity.UserPassword;
                user.UserName = entity.UserName;
                user.UserCountry = entity.UserCountry;
                user.UserAddress = entity.UserAddress;

                return repository.Update(user);
            }
        }

        public bool Delete(int key)
        {
            using (var repository = _sourceFactory.CreateRepository<User, int>())
            {
                return repository.Delete(key);
            }
        }

        
    }
}


