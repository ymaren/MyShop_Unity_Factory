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

    internal class CredentialServiceImpl : ICredentionalViewService
    {
        private readonly IRepositoryFactory _sourceFactory;

        public CredentialServiceImpl(IRepositoryFactory sourceFactory)
        {
            _sourceFactory = sourceFactory;
        }

        public IEnumerable<CredentionalViewModel> ViewAll()
        {
            
            using (var repCredential = _sourceFactory.CreateRepository<Credential, int>())
            {
                var list = repCredential.GetAll().
                    Select(c => new CredentionalViewModel(c.Id, c.NameCredential, c.FullNameCredential, c.Order, c.ParentCredentialId,c.Url));
                return list;
            }
        }

        public CredentionalViewModel ViewSingle(int id)
        {
            using (var repCredential = _sourceFactory.CreateRepository<Credential, int>())

            {
                var cred = repCredential.GetSingle(id);

                return new CredentionalViewModel
                {
                    Id = cred.Id,
                    NameCredential= cred.NameCredential,
                    FullNameCredential = cred.FullNameCredential,
                    Order = cred.Order,
                    ParentCredentialId = cred.ParentCredentialId,
                    Url = cred.Url
                };
            }
        }

    }
 }


