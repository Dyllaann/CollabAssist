using System;
using System.Collections.Generic;
using CollabAssist.Output.Slack.Models.Blocks;

namespace CollabAssist.Output.Slack.Models
{
    internal class BlocksBuilder
    {
        private List<IBlock> _blocks;

        public BlocksBuilder()
        {
            _blocks = new List<IBlock>();
        }

        public BlocksBuilder AddDivider()
        {
            _blocks.Add(new Divider());
            return this;
        }

        public BlocksBuilder AddContext(Action<ContextBlockBuilder> contextBuilderActions)
        {
            var contextBuilder = new ContextBlockBuilder();
            contextBuilderActions(contextBuilder);
            _blocks.Add(contextBuilder.Build());
            return this;
        }

        public BlocksBuilder AddSection(Action<SectionBlockBuilder> sectionBuilderActions)
        {
            var contextBuilder = new SectionBlockBuilder();
            sectionBuilderActions(contextBuilder);
            _blocks.Add(contextBuilder.Build());
            return this;
        }

        public List<IBlock> Build()
        {
            return _blocks;
        }
    }
}
