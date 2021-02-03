using System;
using System.Collections.Generic;
using System.Text;
using CollabAssist.Output.Slack.Models.Blocks;
using CollabAssist.Output.Slack.Models.Blocks.Elements;

namespace CollabAssist.Output.Slack.Models
{
    internal class ContextBlockBuilder
    {
        private List<IElement> _elements;

        public ContextBlockBuilder()
        {
            _elements = new List<IElement>();
        }

        public ContextBlockBuilder HasImage(string url, string alt)
        {
            _elements.Add(new Image(url, alt));
            return this;
        }

        public ContextBlockBuilder HasText(string text)
        {
            _elements.Add(new Markdown(text));
            return this;
        }

        public Context Build()
        {
            return new Context(_elements);
        }
    }
}
