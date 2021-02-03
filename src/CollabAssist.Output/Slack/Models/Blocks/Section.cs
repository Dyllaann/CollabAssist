using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace CollabAssist.Output.Slack.Models.Blocks
{
    internal class Section : IBlock
    {
        [JsonProperty("type")]
        public string Type = "section";

        [JsonProperty("text")]
        public SectionText Text { get; set; }

        [JsonProperty("accessory")]
        public SectionAccessory Accessory { get; set; }

        [JsonProperty("fields")]
        public List<SectionField> Fields { get; set; }

        public Section(SectionText text, SectionAccessory accessory, List<SectionField> fields)
        {
            Text = text;
            Accessory = accessory;
            Fields = fields;
        }
    }

    public class SectionText
    {
        [JsonProperty("type")]
        public string Type = "mrkdwn";

        [JsonProperty("text")]
        public string Text { get; set; }

        public SectionText(string text)
        {
            Text = text;
        }
    }

    public class SectionAccessory
    {
        [JsonProperty("type")]
        public string Type = "image";

        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }

        [JsonProperty("alt_text")]
        public string AltText { get; set; }

        public SectionAccessory(string imageUrl, string altText)
        {
            ImageUrl = imageUrl;
            AltText = altText;
        }
    }

    public class SectionField
    {
        [JsonProperty("type")]
        public string Type = "mrkdwn";

        [JsonProperty("text")]
        public string Text { get; set; }

        public SectionField(string text)
        {
            Text = text;
        }
    }
}
