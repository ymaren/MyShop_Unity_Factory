using System.Collections.Generic;

namespace Store.Logic.Entity
{
    public class User
    {
        public int Id { get; set; }
       
        public string UserEmail { get; set; }
       
        public string UserPassword { get; set; }
        
        public string UserName { get; set; }
       
        public string UserCountry { get; set; }
        
        public string UserAddress { get; set; }
        public int UserRoleId { get; set; }
        
        public List<Credential> Credential { get; set; }

        public User()
        {
            Credential = new List<Credential> { };
        }
    }
}