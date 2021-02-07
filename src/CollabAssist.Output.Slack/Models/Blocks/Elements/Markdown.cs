using Newtonsoft.Json;

namespace CollabAssist.Output.Slack.Models.Blocks.Elements
{
    internal class Markdown : IElement
    {
        [JsonProperty("type")]
        public string Type = "mrkdwn";

        [JsonProperty("text")]
        public string Text { get; set; }

        public Markdown(string text)
        {
            Text = text;
        }
    }
}
