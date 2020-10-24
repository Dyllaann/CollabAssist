using System;
using System.Collections.Generic;
using System.Text;
using CollabAssist.External.Slack.Models;
using CollabAssist.External.Slack.Models.Blocks;
using FluentAssertions;
using Xunit;

namespace CollabAssist.Test.Unit
{
    public class WhenSerializingSlackPayload
    {
        [Fact]
        public void GivenNotGivenPropertiesTheyAreNotIncluded()
        {
            var payload = new SlackPayload()
            {
                Channel = "FOOBAR",
                Blocks = new List<IBlock>()
                {
                    new Section(new SectionText("SectionText"), null, null)
                }
            };

            var serialized = payload.Serialize();

            serialized
                .Should()
                .NotContain("fields");
        }
    }
}
