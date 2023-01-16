using Autofac;
using log4net;
using System;
using System.Threading;
using System.Windows;

namespace wLightBoxRewritten
{
    public partial class App : Application
    {
        public static IContainer? AppContainer;

        private ILog? _log;

        public App()
        {
            AppContainer = ContainerConfig.Configure();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            AppContainer!.BeginLifetimeScope();
            _log = AppContainer.Resolve<ILog>();

            _log.Info("Container started, the app is running");

            var mainWindow = AppContainer.Resolve<MainWindow>();
            mainWindow.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await AppContainer!.DisposeAsync();
            _log!.Info("Container disposed, exiting the app");
            _log!.Info("-----------------------------------");

            base.OnExit(e);
        }
    }
}
