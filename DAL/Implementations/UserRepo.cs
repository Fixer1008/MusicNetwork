using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL.Interfaces;
using NetworkDatabase;

namespace DAL.Implementations
{
    internal class UserRepo: EntityRepository<User>, IUserRepo
    {
        public UserRepo(NetworkContext networkContext)
            : base(networkContext)
        {
            
        }
    }
}