using Store.Logic.ProductStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyShop.Models
{
    public class UserCredViewModel
    {
        public UserViewModel user { get; set; }
        public List <CredentionalViewModel> allCredential { get; set; }
        public int[] SelectedCredential { get; set; }
        public UserCredViewModel(UserViewModel user, List<CredentionalViewModel> allCredential)
        {
            this.user = user;
            this.allCredential = allCredential;
        }
        public UserCredViewModel()
        {
            allCredential = new List<CredentionalViewModel> { };
        }
    }
}