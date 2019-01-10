using Autofac;
using LiveShow.Service.Impl;
using LiveShow.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace LiveShow.Service.IOC
{
    public class ServiceModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RoleSvc>().As<IRoleSvc>().InstancePerLifetimeScope();
            builder.RegisterType<UserSvc>().As<IUserSvc>().InstancePerLifetimeScope();
            builder.RegisterType<ShowRoomSvc>().As<IShowRoomSvc>().InstancePerLifetimeScope();
            builder.RegisterType<ShowRoomViewerSvc>().As<IShowRoomViewerSvc>().InstancePerLifetimeScope();
        }
    }
}
