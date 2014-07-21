using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using NetworkDatabase;

namespace DAL.Implementations
{
    class ImageRepo : EntityRepository<Image>, IImageRepo
    {
        public ImageRepo(NetworkContext networkContext) : base(networkContext)
        {
        }
    }
}
