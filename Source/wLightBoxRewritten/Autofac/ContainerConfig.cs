using Autofac;
using System.Reflection;
using wLightBoxRewritten.Views;

namespace wLightBoxRewritten.Autofac;

public static class ContainerConfig
{
    public static IContainer Configure()
    {
        var builder = new ContainerBuilder();

        builder.RegisterType<MainWindow>()
            .AsSelf()
            .SingleInstance();
        builder.Register(CreateLogger)
            .InstancePerLifetimeScope();

        return builder.Build();
    }

    private static log4net.ILog CreateLogger(IComponentContext context)
    {
        return log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType);
    }
}
