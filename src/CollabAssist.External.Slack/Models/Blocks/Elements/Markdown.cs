using Newtonsoft.Json;

namespace CollabAssist.External.Slack.Models.Blocks.Elements
{
    public class Markdown : IElement
    {
        [JsonProperty("type")]
        public string Type = "mrkdown";

        [JsonProperty("text")]
        public string Text { get; set; }

        public Markdown(string text)
        {
            Text = text;
        }
    }
}
