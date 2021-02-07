using Newtonsoft.Json;

namespace CollabAssist.Output.Slack.Models.Blocks.Elements
{
    internal class Image : IElement
    {
        [JsonProperty("type")]
        public string Type = "image";

        [JsonProperty("image_url")]
        public string Url { get; set; }

        [JsonProperty("alt_text")]
        public string AltText { get; set; }

        public Image(string url, string altText)
        {
            Url = url;
            AltText = altText;
        }
    }
}
