using System.Collections.Generic;
using CollabAssist.Output.Slack.Models.Blocks;
using Newtonsoft.Json;

namespace CollabAssist.Output.Slack.Models
{
    public class SlackPayload
    {
        [JsonProperty("channel")]
        public string Channel { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("ts")]
        public string Timestamp { get; set; }

        [JsonProperty("thread_ts")]
        public string ThreadTimestamp { get; set; }
        
        [JsonProperty("link_names")]
        public bool LinkNames { get; set; }

        [JsonProperty("blocks")]
        public List<IBlock> Blocks { get; set; }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this, Formatting.None,
                new JsonSerializerSettings // Shouldn't include a not set property
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
        }
    }
}
