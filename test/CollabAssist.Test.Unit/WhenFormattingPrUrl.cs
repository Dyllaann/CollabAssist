using System;
using CollabAssist.Incoming.AzureDevOps;
using CollabAssist.Output.Slack;
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

            var formattedUrl = DevOpsUtils.FormatPrUrl(pr);

            formattedUrl
                .Should()
                .Be(url);
        }

        [Fact]
        public void GivenInvalidNotificationShouldThrow()
        {
            var pr = TestUtils.GenerateValidDevOpsPrNotification();
            pr.Resource.Repository.Project = null;

            Action formatAction = () => DevOpsUtils.FormatPrUrl(pr);

            formatAction
                .Should()
                .Throw<Exception>();

        }
    }
}
