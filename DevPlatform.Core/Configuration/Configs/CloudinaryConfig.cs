namespace DevPlatform.Core.Configuration.Configs
{
    /// <summary>
    /// Cloudinary configs
    /// </summary>
    public partial class CloudinaryConfig : IConfig
    {
        /// <summary>
        /// Gets or sets cloudName
        /// </summary>
        public string CloudName { get; set; } = "dkkr1ddp3";

        /// <summary>
        /// Gets or sets cloudinary api key
        /// </summary>
        public string ApiKey { get; set; } = "696298912576665";

        /// <summary>
        /// Gets or sets cloudinary api secret key
        /// </summary>
        public string ApiSecret { get; set; } = "tTaoSSSneUgf7gDk5SzNQSnn3VA";
    }
}
