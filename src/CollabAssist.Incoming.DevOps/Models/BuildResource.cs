using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace CollabAssist.Incoming.DevOps.Models
{
    public class BuildResource
    {
        [JsonProperty("uri")]
        public string Uri { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("buildNumber")]
        public string BuildNumber { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("startTime")]
        public DateTime StartTime { get; set; }

        [JsonProperty("finishTime")]
        public DateTime FinishTime { get; set; }

        [JsonProperty("reason")]
        public string Reason { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }





    }
}
