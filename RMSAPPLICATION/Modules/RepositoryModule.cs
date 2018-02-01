using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Builder;
using Autofac.Features.Scanning;
using RMSREPO.Generic;


namespace RMSAPPLICATION.Modules
{
    public class RepositoryModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            getdata(builder);
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerDependency();
        }
        private IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> getdata(ContainerBuilder builder)
        {
            return builder.RegisterAssemblyTypes(Assembly.Load("RMSREPO")).Where(t => t.IsClass && t.Name.EndsWith("Repository"))
       .As(t => t.GetInterfaces().Single(i => i.Name.EndsWith(t.Name)));

        }
    }
}