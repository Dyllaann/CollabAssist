using Newtonsoft.Json;

namespace CollabAssist.External.Slack.Models.Blocks
{
    public class Divider : IBlock
    {
        [JsonProperty("type")]
        public string Type { get; set; } = "divider";

        [JsonProperty("block_id")]
        public string BlockId { get; set; }
    }
}
