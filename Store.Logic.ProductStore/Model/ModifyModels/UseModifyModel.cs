using Store.Logic.Entity;
using Store.Logic.ProductStore.Models.ViewModels;
using System.Collections.Generic;

namespace Store.Logic.ProductStore.Models.ModifyModels
{
    public class UserModifyModel
    {
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public string UserName { get; set; }
        public string UserCountry { get; set; }
        public string UserAddress { get; set; }
        public int UserRoleId { get; set; }
        public List<CredentionalViewModel> Credential { get; set; }

        public UserModifyModel()
        {
            Credential = new List<CredentionalViewModel> { };
        }

        public UserModifyModel(string userEmail, string userPassword, string userName, string userCountry, string userAddress, int userRoleId)
        {
            this.UserEmail = userEmail;
            this.UserPassword = userPassword;
            this.UserName = userName;
            this.UserCountry = userCountry;
            this.UserAddress = userAddress;
            this.UserRoleId = userRoleId;
           
        }

    }
}