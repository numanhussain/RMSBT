using System.Data.Entity;
using Autofac;
using RMSREPO.Generic;
using RMSCORE.EF;

namespace RMSAPPLICATION.Modules
{
    public class EFModule :Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new RepositoryModule());

            builder.RegisterType(typeof(BTRMSEntities)).As(typeof(DbContext)).InstancePerRequest();
            builder.RegisterType(typeof(UnitOfWork)).As(typeof(IUnitOfWork)).InstancePerRequest();

        }
    }
}