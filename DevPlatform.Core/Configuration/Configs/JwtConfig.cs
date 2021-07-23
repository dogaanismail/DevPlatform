namespace DevPlatform.Core.Configuration.Configs
{
    /// <summary>
    /// Represents JWT configuration parameters
    /// </summary>
    public partial class JwtConfig : IConfig
    {
        /// <summary>
        /// Gets or sets audience 
        /// </summary>
        public string Audience { get; set; } = "localhost";

        /// <summary>
        /// Gets or sets clock skew time in minutes
        /// </summary>
        public int ClockSkew { get; set; } = 0;

        /// <summary>
        /// Gets or sets issuer
        /// </summary>
        public string Issuer { get; set; } = "devplatformebapi";

        /// <summary>
        /// Gets or sets key
        /// </summary>
        public string Key { get; set; } = "iNivDmHLpUA223sqsfhqGbMRdRj1PVkH";

        /// <summary>
        /// Gets or sets token expiration time in minutes
        /// </summary>
        public int TokenExpirationTime { get; set; } = 1440;

        /// <summary>
        /// Gets or sets signingKey
        /// </summary>
        public bool ValidateIssuerSigningKey { get; set; } = true;

        /// <summary>
        /// Gets or sets ValidateLifetime
        /// </summary>
        public bool ValidateLifetime { get; set; } = true;
    }
}
