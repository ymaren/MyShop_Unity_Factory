using Store.Logic.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace Store.Logic.ProductStore.Models.ViewModels
{

    public class UserViewModel
    {
        public int Id { get; set; }
        [Required]
        public string UserEmail { get; set; }
        [DataType(DataType.Password)]
        public string UserPassword { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string UserCountry { get; set; }
        [Required]
        public string UserAddress { get; set; }
        public int UserRoleId { get; set; }
        public UserRoleViewModel UserRole { get; set; }
        public List<CredentionalViewModel> Credential { get; set; }



        public UserViewModel(User u)
        {
            this.Id = u.Id;
            this.UserEmail = u.UserEmail;
            this.UserPassword = u.UserPassword;
            this.UserName = u.UserName;
            this.UserCountry = u.UserCountry;
            this.UserAddress = u.UserAddress;
            this.UserRoleId = u.UserRoleId;
            
        }
        public UserViewModel()
        {
            Credential = new List<CredentionalViewModel> { };
        }

       
    }
}
