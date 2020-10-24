using System.Collections.Generic;
using CollabAssist.External.Slack.Models.Blocks.Elements;
using Newtonsoft.Json;

namespace CollabAssist.External.Slack.Models.Blocks
{
    public class Context : IBlock
    {
        [JsonProperty("type")]
        public string Type = "context";

        [JsonProperty("elements")]
        public List<IElement> Elements { get; set; }

        public Context(List<IElement> elements)
        {
            Elements = elements;
        }
    }
}
