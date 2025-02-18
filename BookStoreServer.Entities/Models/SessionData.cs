using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStoreServer.Entities;

namespace BookStoreServer.Api.Entities.Models
{
    public class SessionData
    {
        public int UserIdentity { get; set; }
        public Language Language { get; set; }

        //public bool IsAdmin => Role == Roles.Admin; 
        //public Roles Role { get; set; }

        public int RoleId { get; set; }
        public UserType UserType { get; set; }
        public long CustomerNum { get; set; }
        public bool RunMockTransaction { get; set; }

        public override string ToString()
        {
            return $"userIdentity={UserIdentity}";
        }
    }

}
