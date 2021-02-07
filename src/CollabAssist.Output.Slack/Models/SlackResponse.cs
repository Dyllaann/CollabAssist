using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace CollabAssist.Output.Slack.Models
{
    public class SlackResponse
    {
        [JsonProperty("ok")]
        public bool Ok { get; set; }
        [JsonProperty("ts")]
        public string Timestamp { get; set; }
        [JsonProperty("error")]
        public string Error { get; set; }
    }
}
