using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using DAL.Implementations;
using DAL.Interfaces;

namespace MusicSocialNetwork.Modules
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
        }
    }
}