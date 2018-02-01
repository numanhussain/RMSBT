using System.Linq;
using System.Reflection;
using Autofac;
using RMSSERVICES.Generic;
namespace RMSAPPLICATION.Modules
{
    public class ServiceModule :Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.Load("RMSSERVICES")).Where(t => t.IsClass && t.Name.EndsWith("Service"))
            .As(t => t.GetInterfaces().Single(i => i.Name.EndsWith(t.Name)));
            builder.RegisterGeneric(typeof(EntityService<>)).As(typeof(IEntityService<>)).InstancePerDependency();

        }
    }
}