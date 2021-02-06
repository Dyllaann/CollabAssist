using System;
using Newtonsoft.Json;

namespace CollabAssist.Incoming.DevOps.Models
{
    public class CreatedBy
    {
        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("links")]
        public Links Links { get; set; }

        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("uniqueName")]
        public string UniqueName { get; set; }

        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }

        [JsonProperty("descriptor")]
        public string Descriptor { get; set; }
    }

    public class Links
    {
        [JsonProperty("avatar")]
        public Avatar Avatar { get; set; }
    }

    public class Avatar
    {
        [JsonProperty("href")]
        public string Href { get; set; }
    }
}
