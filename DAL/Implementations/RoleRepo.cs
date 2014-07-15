using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using NetworkDatabase;

namespace DAL.Implementations
{
    internal class RoleRepo :  EntityRepository<Role>, IRoleRepo
    {
        public RoleRepo(NetworkContext networkContext)
            : base(networkContext)
        {
        }
    }
}
