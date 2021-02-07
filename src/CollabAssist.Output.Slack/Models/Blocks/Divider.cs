using Newtonsoft.Json;

namespace CollabAssist.Output.Slack.Models.Blocks
{
    internal class Divider : IBlock
    {
        [JsonProperty("type")]
        public string Type { get; set; } = "divider";

        [JsonProperty("block_id")]
        public string BlockId { get; set; }
    }
}
