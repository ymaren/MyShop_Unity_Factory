using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Models.MyShopModels
{
   public class Credential
    {
        //[Key]
        public int Id { get; set; }
        public string NameCredential { get; set; }
        public string FullNameCredential { get; set; }
        public int ParentCredentialId { get; set; }
        public int Order { get; set; }
        public string Url { get; set; }
        public  ICollection<UserRole> UserRole { get; set; }
        public  ICollection<User> User { get; set; }
        public Credential()
        {
            UserRole = new List<UserRole>();
        }
    }
}
