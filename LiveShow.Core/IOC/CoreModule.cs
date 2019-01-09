using Autofac;
using System;
using System.Collections.Generic;
using System.Text;
using LiveShow.Core.Encrypt;

namespace LiveShow.Core.IOC
{
    public class CoreModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EncryptUtil>().As<IEncryptUtil>().InstancePerLifetimeScope();
        }
    }
}
