﻿using System;
using Newtonsoft.Json;

namespace CollabAssist.Incoming.DevOps.Models
{
    public class Project
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("revision")]
        public int Revision { get; set; }

        [JsonProperty("visibility")]
        public string Visibility { get; set; }

        [JsonProperty("lastUpdateTime")]
        public DateTime LastUpdateTime { get; set; }
    }
}
