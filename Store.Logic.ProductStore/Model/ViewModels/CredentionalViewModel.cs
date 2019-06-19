using Store.Logic.Entity;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace Store.Logic.ProductStore.Models.ViewModels
{
   
    public class CredentionalViewModel
    {
        public int Id { get; set; }
        public string NameCredential { get; set; }
        public string FullNameCredential { get; set; }
        public int ParentCredentialId { get; set; }
        public int Order { get; set; }
        public string Url { get; set; }

        public CredentionalViewModel()
        {

        }
        public CredentionalViewModel(int id, string nameCredential, string fullNameCredential,
            int order, int parentCredentialId, string url
            )
        {
            this.Id = id;
            this.NameCredential = nameCredential;
            this.FullNameCredential = fullNameCredential;
            this.Order = order;
            this.ParentCredentialId = parentCredentialId;
            this.Url = url;
        }
    }
}
