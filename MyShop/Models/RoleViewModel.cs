using Store.Logic.ProductStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyShop.Models
{
    public class RoleViewModel
    {
        public UserRoleViewModel userRole { get; set; }
        public ICollection <CredentionalViewModel> allCredential { get; set; }
        public int[] SelectedCredential { get; set; }
        public RoleViewModel(UserRoleViewModel userRole, ICollection<CredentionalViewModel> allCredential)
        {
            this.userRole = userRole;
            this.allCredential = allCredential;
        }
        public RoleViewModel()
        {
            allCredential = new List<CredentionalViewModel>();
        }
        //public FillSelectedCredential()
        //{
        //    DBContext _db = new DBContext();
        //    foreach (int item in role.SelectedCredential)
        //    {
        //        _db.Credentials
        //        role.userRole.Credential.FirstOrDefault(c => c.Id == item).UserRole.Add(role.userRole);
        //        //role.userRole.Credential.Add(_credential.GetAll().FirstOrDefault(c => c.Id == item));
        //    }
        //}
    }
}