namespace DevPlatform.Core.Configuration.Settings.Common
{
    public class OpenWeatherSettings : ISettings
    {
        public bool EnabledLogging { get; set; }
        public string ApiUrl { get; set; }
        public string ApiKey { get; set; }
    }
}
