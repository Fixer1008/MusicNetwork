using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using NetworkDatabase;

namespace DAL.Implementations
{
    class SongRepo : EntityRepository<Song>, ISongRepo
    {
        public SongRepo(NetworkContext networkContext)
            : base(networkContext)
        {
        }
    }
}
