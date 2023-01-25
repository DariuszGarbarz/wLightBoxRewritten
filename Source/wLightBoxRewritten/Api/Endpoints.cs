namespace wLightBoxRewritten.Api;

public static class Endpoints
{
    public static class General
    {
        public static readonly string DeviceInfoUri = "/info";
        public static readonly string DeviceUptimeUri = "/api/device/uptime";
        public static readonly string FirmwareUpdateUri = "/api/ota/update";
    }

    public static class Network
    {
        public static readonly string NetworkInfoUri = "/api/device/network";
        public static readonly string SetAccessPointUri = "/api/device/set";
        public static readonly string WifiScanUri = "/api/wifi/scan";
        public static readonly string WifiConnectUri = "/api/wifi/connect";
        public static readonly string WifiDisconnectUri = "/api/wifi/disconnect";
    }

    public static class ControlAndState
    {
        public static readonly string StateOfLightingUri = "/api/rgbw/state";
        public static readonly string SetStateOfLightingUri = "/api/rgbw/set";
        public static readonly string ExtendedStateOfLightingUri = "/api/rgbw/extended/state";
        public static readonly string SetExtendedStateOfLightingUri = "/api/rgbw/extended/set";
    }

    public static class Settings
    {
        public static readonly string SettingStateUri = "/api/settings/state";
        public static readonly string SetSettingsUri = "/api/settings/set";
    }
}
