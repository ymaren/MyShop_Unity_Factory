using Store.Logic.Entity;
using Store.Logic.ProductStore.Models.ViewModels;

namespace Store.Logic.ProductStore.Models.ModifyModels
{
    public class UserRoleModifyModel
    {
        public string UserRoleName { get; set; }


        public UserRoleModifyModel( string userRoleName)
        {
            
            this.UserRoleName = userRoleName;
            
        }

    }
}