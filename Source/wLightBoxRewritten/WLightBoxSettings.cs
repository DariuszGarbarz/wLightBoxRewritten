using System.Configuration;

namespace wLightBoxRewritten;

public interface IWLightBoxSettings
{
    string? BaseAddress { get; }
    TimeSpan RefreshRate { get; }
}

public class WLightBoxSettings : IWLightBoxSettings
{
    public string? BaseAddress
    {
        get
        {
            return ConfigurationManager.AppSettings["BaseAddress"];
        }
    }
    public TimeSpan RefreshRate
    {
        get
        {
            return TimeSpan.Parse(ConfigurationManager.AppSettings["RefreshRate"]!);
        }
    }
}