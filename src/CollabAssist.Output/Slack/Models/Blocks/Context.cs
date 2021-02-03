using System.Collections.Generic;
using CollabAssist.Output.Slack.Models.Blocks.Elements;
using Newtonsoft.Json;

namespace CollabAssist.Output.Slack.Models.Blocks
{
    internal class Context : IBlock
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
