using System;
using Newtonsoft.Json;

namespace CollabAssist.Incoming.DevOps.Models
{
    public class Repository
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("project")]
        public Project Project { get; set; }

        [JsonProperty("size")]
        public int Size { get; set; }

        [JsonProperty("remoteUrl")]
        public string RemoteUrl { get; set; }

        [JsonProperty("sshUrl")]
        public string SshUrl { get; set; }

        [JsonProperty("webUrl")]
        public string WebUrl { get; set; }
    }
}
