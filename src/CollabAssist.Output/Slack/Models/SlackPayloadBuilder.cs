using System;
using System.Collections.Generic;
using System.Text;
using CollabAssist.Output.Slack.Models.Blocks;

namespace CollabAssist.Output.Slack.Models
{
    internal class SlackPayloadBuilder
    {
        private string _channel;
        private string _text;
        private string _timestamp;
        private string _threadTimestamp;
        private bool _linksNames;
        private List<IBlock> _blocks;


        public SlackPayloadBuilder()
        {
            _blocks = new List<IBlock>();
        }

        public SlackPayloadBuilder SendsTo(string channelId)
        {
            _channel = channelId;
            return this;
        }

        public SlackPayloadBuilder HasPreviewText(string text)
        {
            _text = text;
            return this;
        }

        public SlackPayloadBuilder UpdatesMessage(string timestamp)
        {
            _timestamp = timestamp;
            return this;
        }

        public SlackPayloadBuilder PostsAsThreadIn(string timestamp)
        {
            _threadTimestamp = timestamp;
            return this;
        }

        public SlackPayloadBuilder LinksNames(bool linksNames)
        {
            _linksNames = linksNames;
            return this;
        }

        public SlackPayloadBuilder WithSection(Action<SectionBlockBuilder> sbb)
        {
            var sectionBlockBuilder = new SectionBlockBuilder();
            sbb(sectionBlockBuilder);
            _blocks.Add(sectionBlockBuilder.Build());
            return this;
        }

        public SlackPayloadBuilder WithContext(Action<ContextBlockBuilder> cbb)
        {
            var contextBlockBuilder = new ContextBlockBuilder();
            cbb(contextBlockBuilder);
            _blocks.Add(contextBlockBuilder.Build());
            return this;
        }

        public SlackPayloadBuilder WithDivider()
        {
            _blocks.Add(new Divider());
            return this;
        }

        public SlackPayload Build()
        {
            return new SlackPayload
            {
                Channel = _channel,
                Text = _text,
                Timestamp = _timestamp,
                ThreadTimestamp = _threadTimestamp,
                LinkNames = _linksNames,
                Blocks = _blocks
            };
        }
    }
}
