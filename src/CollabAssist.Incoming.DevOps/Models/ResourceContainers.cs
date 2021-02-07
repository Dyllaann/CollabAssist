using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace CollabAssist.Incoming.DevOps.Models
{
    public class ResourceContainers
    {
        [JsonProperty("collection")]
        public ResourceContainer Collection { get; set; }

        [JsonProperty("account")]
        public ResourceContainer Account { get; set; }

        [JsonProperty("project")]
        public ResourceContainer Project { get; set; }
    }

    public class ResourceContainer
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("baseUrl")]
        public string BaseUrl { get; set; }
    }
}
