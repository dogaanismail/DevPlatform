using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DevPlatform.Core.Configuration.Configs
{
    /// <summary>
    /// Represents the app configs
    /// </summary>
    public partial class AppConfigs
    {
        #region Properties

        /// <summary>
        /// Gets or sets cache configuration parameters
        /// </summary>
        public CacheConfig CacheConfig { get; set; } = new CacheConfig();

        /// <summary>
        /// Gets or sets hosting configuration parameters
        /// </summary>
        public HostingConfig HostingConfig { get; set; } = new HostingConfig();

        /// <summary>
        /// Gets or sets distributed cache configuration parameters
        /// </summary>
        public DistributedCacheConfig DistributedCacheConfig { get; set; } = new DistributedCacheConfig();

        /// <summary>
        /// Gets or sets Azure Blob storage configuration parameters
        /// </summary>
        public AzureBlobConfig AzureBlobConfig { get; set; } = new AzureBlobConfig();

        /// <summary>
        /// Gets or sets common configuration parameters
        /// </summary>
        public CommonConfig CommonConfig { get; set; } = new CommonConfig();

        /// <summary>
        /// Gets or sets cloudinary configuration parameters
        /// </summary>
        public CloudinaryConfig CloudinaryConfig { get; set; } = new CloudinaryConfig();

        /// <summary>
        /// Gets or sets jwt configuration parameters
        /// </summary>
        public JwtConfig JwtConfig { get; set; } = new JwtConfig();

        /// <summary>
        /// Gets or sets additional configuration parameters
        /// </summary>
        [JsonExtensionData]
        public IDictionary<string, JToken> AdditionalData { get; set; }

        #endregion
    }
}
