using log4net;
using System.Windows;

namespace wLightBoxRewritten.Views;

public partial class MainWindow : Window
{
    private readonly ILog _log;

    public MainWindow(ILog log)
    {
        InitializeComponent();
        log.Info("Main Window initialized");
        _log = log;
    }
}
