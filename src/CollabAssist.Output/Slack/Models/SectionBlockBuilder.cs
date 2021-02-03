using System.Collections.Generic;
using System.Net.Mime;
using CollabAssist.Output.Slack.Models.Blocks;

namespace CollabAssist.Output.Slack.Models
{
    internal class SectionBlockBuilder
    {
        private SectionText _text;
        private List<SectionField> _fields;
        private SectionAccessory _accessory;

        public SectionBlockBuilder()
        {
            _fields = new List<SectionField>();
        }

        public SectionBlockBuilder HasText(string text)
        {
            _text = new SectionText(text);
            return this;
        }

        public SectionBlockBuilder HasField(string text)
        {
            _fields.Add(new SectionField(text));
            return this;
        }

        public SectionBlockBuilder HasField(string title, string data)
        {
            _fields.Add(new SectionField($"*{title}*\n {data}"));
            return this;
        }

        public SectionBlockBuilder HasAccessory(string alt, string url)
        {
            _accessory = new SectionAccessory(url, alt);
            return this;
        }

        public Section Build()
        {
            return new Section(_text, _accessory, _fields);
        }
    }
}
