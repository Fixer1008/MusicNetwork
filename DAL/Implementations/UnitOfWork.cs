using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL.Interfaces;
using NetworkDatabase;

namespace DAL.Implementations
{
    public class UnitOfWork:IUnitOfWork
    {
        private NetworkContext _networkContext;

        public UnitOfWork()
        {
            _networkContext = new NetworkContext();
        }

        public IRepository<User> UserRepository
        {
            get { return new UserRepo(_networkContext); }
        }
        public IRepository<Role> RoleRepository
        {
            get { return new RoleRepo(_networkContext); }
        }
        public IRepository<Message> MessageRepository
        {
            get { return new MessageRepo(_networkContext); }
        }
        public IRepository<Song> SongRepository
        {
            get { return new SongRepo(_networkContext); }
        }
        public IRepository<Image> ImageRepository
        {
            get {return new ImageRepo(_networkContext);}
        }

        public void Commit()
        {
            _networkContext.SaveChanges();
        }

        public void Dispose()
        {
            _networkContext.Dispose();
        }
    }
}