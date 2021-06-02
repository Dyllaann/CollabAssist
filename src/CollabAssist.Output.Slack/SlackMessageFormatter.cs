using System;
using System.Collections.Generic;
using System.Text;
using CollabAssist.Incoming;
using CollabAssist.Incoming.Models;
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

        public static SlackPayload FormatNewPullRequestWithImage(PullRequest pr, string slackChannel, string profilePictureUrl)
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
                        .HasAccessory($"{pr.AuthorName}'s Profile Picture", profilePictureUrl)
                        .HasField("Author", pr.AuthorName)
                        .HasField("Repository", pr.RepositoryName))
                .WithDivider()
                .Build();
        }

        public static SlackPayload FormatUpdatedPullRequest(PullRequest pr, string slackChannel, string identifier)
        {
            var payloadBuilder = new SlackPayloadBuilder();
            return payloadBuilder
                .SendsTo(slackChannel)
                .UpdatesMessage(identifier)
                .HasPreviewText($"New PR in {pr.RepositoryName} from {pr.AuthorName}")
                .WithDivider()
                .WithContext(cbb =>
                    cbb.HasText($":new: Pull-Request from {pr.AuthorName} in {pr.RepositoryName}"))
                .WithSection(sbb =>
                    sbb.HasText($"<{pr.Url}|{pr.Title}>")
                        .HasField("Author", pr.AuthorName)
                        .HasField("Repository", pr.RepositoryName)
                        .HasField("Reviewers", FormatReviewersField(pr.Reviewers))
                        .HasField("State", pr.Status.ToString()))
                .WithDivider()
                .Build();
        }

        public static SlackPayload FormatUpdatedPullRequestWithImage(PullRequest pr, string slackChannel, string identifier, string profilePictureUrl)
        {
            var payloadBuilder = new SlackPayloadBuilder();
            return payloadBuilder
                .SendsTo(slackChannel)
                .UpdatesMessage(identifier)
                .HasPreviewText($"New PR in {pr.RepositoryName} from {pr.AuthorName}")
                .WithDivider()
                .WithContext(cbb =>
                    cbb.HasText($"Pull-Request from {pr.AuthorName} in {pr.RepositoryName}"))
                .WithSection(sbb =>
                    sbb.HasText($"<{pr.Url}|{pr.Title}>")
                        .HasAccessory($"{pr.AuthorName}'s Profile Picture", profilePictureUrl)
                        .HasField("Author", pr.AuthorName)
                        .HasField("Repository", pr.RepositoryName)
                        .HasField("Reviewers", FormatReviewersField(pr.Reviewers))
                        .HasField("State", pr.Status.ToString()))
                .WithDivider()
                .Build();
        }

        public static SlackPayload FormatFailedBuild(Build build, string slackChannel, string updateTs, SlackUserResponse pullRequestOwner)
        {
            var payloadBuilder = new SlackPayloadBuilder();
            var builder = payloadBuilder
                .SendsTo(slackChannel)
                .PostsAsThreadIn(updateTs)
                .LinksNames(true)
                .HasPreviewText($"Build of your PR failed.");

            if (pullRequestOwner != null)
            {
                builder.WithSection(s =>
                    s.HasText($"Hey <@{pullRequestOwner.SlackUser.Id}>, the build of this PR failed."));
            }
            else
            {
                builder.WithSection(s =>
                    s.HasText($"I was unable to find who to tag for this PullRequest, but the build failed."));
            }

            return builder.Build();
        }

        private static string FormatReviewersField(List<Reviewer> reviewers)
        {
            var fieldData = "";
            foreach (var reviewer in reviewers)
            {
                fieldData += $"{reviewer.Name}: {GetEmojiForVote(reviewer.Vote)}\n ";
            }

            return fieldData;
        }

        private static string GetEmojiForVote(Vote vote)
        {
            switch (vote)
            {
                case Vote.Rejected:
                    return ":x:";
                case Vote.Waiting:
                    return ":hourglass:";
                case Vote.ApprovedWithSuggestions:
                    return ":ballot_box_with_check:";
                case Vote.Approved:
                    return ":white_check_mark:";
                case Vote.Unknown:
                    return ":question:";
                default:
                    return ":question:";
            }
        }
    }
}
