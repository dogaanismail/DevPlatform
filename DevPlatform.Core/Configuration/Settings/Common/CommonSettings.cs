namespace DevPlatform.Core.Configuration.Settings.Common
{
    public class CommonSettings: ISettings
    {
        public bool UseDefaultIpAddressForLocal { get; set; }
        public string DefaultIpAddress { get; set; }
    }
}
