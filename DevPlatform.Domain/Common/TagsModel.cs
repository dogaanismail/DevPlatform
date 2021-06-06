using Newtonsoft.Json;

namespace DevPlatform.Domain.Common
{
    public class TagsModel
    {
        [JsonProperty("display")]
        public string Display { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
