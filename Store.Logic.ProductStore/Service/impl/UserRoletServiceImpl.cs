namespace Store.Logic.ProductStore.Service.impl
{
    using Core.Common.Dal;
    using Models.ModifyModels;
    using Models.ViewModels;
    using Service.ModifyServices;
    using Service.ViewServices;
    using Store.Logic.Entity;
    using Store.Logic.ProductStore.Exceptions;
    using Store.Logic.ProductStore.Infustructure;
    using System.Collections.Generic;
    using System.Linq;

    internal class UserRoleServiceImpl : IUserRoleViewService, IUserRoleModifyService
    {
        private readonly IRepositoryFactory _sourceFactory;

        public UserRoleServiceImpl(IRepositoryFactory sourceFactory)
        {
            _sourceFactory = sourceFactory;
        }


        public IEnumerable<UserRoleViewModel> ViewAll()
        {
            using (var repUserRole = _sourceFactory.CreateRepository<UserRole, int>())
            using (var repGroup = _sourceFactory.CreateRepository<ProductGroup, int>())
            {
                var list = repUserRole.GetAll().
                    Select(c => new UserRoleViewModel
                    {
                        Id = c.Id,
                        UserRoleName = c.UserRoleName

                    });
                return list;
            }
        }

        public UserRoleViewModel ViewSingle(int id)
        {
            using (var repUserRole = _sourceFactory.CreateRepository<UserRole, int>())

            {
                var userRole = repUserRole.GetSingle(id);

                return new UserRoleViewModel
                {
                    Id = userRole.Id,
                    UserRoleName = userRole.UserRoleName,
                    Credential = userRole.RoleCredential.Select(c=> 
                                       new CredentionalViewModel(c.Id, c.NameCredential, c.FullNameCredential, c.Order,c.ParentCredentialId,c.Url)).ToList()
                };
            }
        }


        public bool Add(UserRoleModifyModel entity)
        {
            using (var repository = _sourceFactory.CreateRepository<UserRole, int>())
            {
                return repository.Add(new UserRole { UserRoleName = entity.UserRoleName }
                    );
            };
        }




        public bool Update(int id, UserRoleModifyModel entity)
            {
                using (var repository = _sourceFactory.CreateRepository<UserRole, int>())
                {
                    var userRole = repository.GetSingle(id);
                    if (userRole == null)
                        throw new NotFoundException();
                    userRole.UserRoleName = entity.UserRoleName;

                    return repository.Update(userRole);
                }
            }

        public bool Delete(int key)
            {
                using (var repository = _sourceFactory.CreateRepository<UserRole, int>())
                {
                    return repository.Delete(key);
                }
            }


        }
 }


