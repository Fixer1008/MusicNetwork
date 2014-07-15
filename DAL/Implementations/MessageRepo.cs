using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using NetworkDatabase;

namespace DAL.Implementations
{
    class MessageRepo : EntityRepository<Message>, IMessageRepo
    {
        public MessageRepo(NetworkContext networkContext)
            : base(networkContext)
        {
            
        }
    }
}
