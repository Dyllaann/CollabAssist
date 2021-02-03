using System;
using System.Collections.Generic;
using System.Text;
using CollabAssist.Incoming;
using CollabAssist.Output.Slack.Models;

namespace CollabAssist.Output.Slack
{
    internal static class SlackMessageFormatter
    {
        public static SlackPayload FormatNewPullRequest(PullRequest pr, string slackChannel)
        {
            var payloadBuilder = new SlackPayloadBuilder();
            return payloadBuilder
                .SendsTo(slackChannel)
                .HasPreviewText($"New PR in {pr.RepositoryName} from {pr.AuthorName}")
                .WithDivider()
                .WithContext(cbb =>
                    cbb.HasText($":new: Pull-Request from {pr.AuthorName} in {pr.RepositoryName}"))
                .WithSection(sbb =>
                    sbb.HasText($"<{pr.Url}|{pr.Title}>")
                        .HasField("Author", pr.AuthorName)
                        .HasField("Repository", pr.RepositoryName))
                .WithDivider()
                .Build();
        }
    }
}
