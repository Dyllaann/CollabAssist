using System;
using System.Collections.Generic;
using System.Text;
using CollabAssist.Incoming.Slack;
using FluentAssertions;
using Xunit;

namespace CollabAssist.Test.Unit
{
    public class WhenFormattingPrUrl
    {
        [Fact]
        public void GivenValidNotificationShouldFormatPrUrl()
        {
            var pr = TestUtils.GenerateValidDevOpsPrNotification();

            var url = "https://dev.azure.com/collabassistorg/CollabAssistProject/_git/CollabAssist/pullrequest/1337";

            var formattedUrl = SlackUtils.FormatPrUrl(pr);

            formattedUrl
                .Should()
                .Be(url);
        }

        [Fact]
        public void GivenInvalidNotificationShouldThrow()
        {
            var pr = TestUtils.GenerateValidDevOpsPrNotification();
            pr.Resource.Repository.Project = null;

            Action formatAction = () => SlackUtils.FormatPrUrl(pr);

            formatAction
                .Should()
                .Throw<Exception>();

        }
    }
}
