using Autofac.Core;
using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Channels;
using Infrastructure.Utilities;

namespace Adapter.InjectionModules
{
    public class AutofacPropertityModuleRegister : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(type => type.IsAssignableTo<BackgroundService>())
                .AsImplementedInterfaces().PropertiesAutowired();

            builder.RegisterType<IotMqttTool>().AsSelf().SingleInstance().PropertiesAutowired().AsImplementedInterfaces();
            //builder.RegisterType<IotMqttPushTool>().AsSelf().SingleInstance().PropertiesAutowired().AsImplementedInterfaces();
            //builder.RegisterAssemblyTypes(Assembly.Load(nameof(Service))).PropertiesAutowired().AsSelf().InstancePerDependency();
            
            //builder.Register(context => Channel.CreateUnbounded<MqttApplicationMessageReceivedEventArgs>()).AsSelf().SingleInstance();
            //builder.Register(context => context.Resolve<Channel<MqttApplicationMessageReceivedEventArgs>>().Reader).AsSelf().SingleInstance();
            //builder.Register(context => context.Resolve<Channel<MqttApplicationMessageReceivedEventArgs>>().Writer).AsSelf().SingleInstance();
        }
    }
}
