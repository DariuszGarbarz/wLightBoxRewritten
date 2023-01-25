using Autofac;
using System;
using System.Net.Http;
using System.Reflection;
using wLightBoxRewritten.Api;
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

        builder.Register(CreateHttpClient)
            .InstancePerLifetimeScope();

        builder.RegisterType<WLightBoxSettings>()
            .As<IWLightBoxSettings>()
            .InstancePerLifetimeScope();

        builder.RegisterType<ApiClient>()
            .As<IApiClient>()
            .InstancePerLifetimeScope();

        builder.RegisterType<ApiHttpClient>()
            .As<IApiHttpClient>()
            .InstancePerLifetimeScope();

        return builder.Build();
    }

    private static log4net.ILog CreateLogger(IComponentContext context)
    {
        return log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType);
    }

    private static HttpClient CreateHttpClient(IComponentContext context)
    {
        var config = context.Resolve<IWLightBoxSettings>();
        var log = context.Resolve<log4net.ILog>();
        var address = config.BaseAddress;

        if (address == null)
        {
            log.Warn("Base address cannot be a null, setting up default address");
            address = "192.168.0.1";
        }

        if (!address.EndsWith("/"))
            address += "/";


        if (!address.StartsWith("http://") && !address.StartsWith("https://"))
        {
            address = "http://" + address;
        }

        var httpClient = new HttpClient()
        {
            BaseAddress = new Uri(address, UriKind.Absolute),
        };

        return httpClient;
    }
}
