using System.Collections.Generic;

namespace Store.Logic.Entity
{
    public class UserRole
    {
        public int Id { get; set; }
        public string UserRoleName { get; set; }
        public List<Credential> RoleCredential { get; set; }

        public UserRole()
        {
            RoleCredential = new List<Credential>() { };
        }
    }
}