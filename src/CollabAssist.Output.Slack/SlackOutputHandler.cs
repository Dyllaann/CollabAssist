using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CollabAssist.Incoming;
using CollabAssist.Incoming.Models;
using CollabAssist.Output.Slack.Client;
using CollabAssist.Output.Slack.Models;

namespace CollabAssist.Output.Slack
{
    public class SlackOutputHandler : IOutputHandler
    {
        private readonly ISlackClient _client;
        private readonly SlackConfiguration _configuration;

        public SlackOutputHandler(ISlackClient client, SlackConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;
        }


        public async Task<string> NotifyNewPullRequest(PullRequest pr)
        {
            var profilePicture = await GetAuthorProfilePictureUrl(pr).ConfigureAwait(false);
            var payload = profilePicture != null 
                ? SlackMessageFormatter.FormatNewPullRequestWithImage(pr, _configuration.Channel, profilePicture) 
                : SlackMessageFormatter.FormatNewPullRequest(pr, _configuration.Channel);

            var success = await _client.PostMessage(payload);
            if (success.Ok)
            {
                return success.Timestamp;
            }

            return null;
        }

        public async Task<bool> HandleUpdatedPullRequest(PullRequest update, string identifier)
        {
            var profilePicture = await GetAuthorProfilePictureUrl(update).ConfigureAwait(false);
            var payload = profilePicture != null
                ? SlackMessageFormatter.FormatUpdatedPullRequestWithImage(update, _configuration.Channel, identifier, profilePicture)
                : SlackMessageFormatter.FormatUpdatedPullRequest(update, _configuration.Channel, identifier);

            var success = await _client.UpdateMessage(payload).ConfigureAwait(false);
            return success.Ok;
        }

        public async Task<bool> HandleAbandonedPullRequest(PullRequest update, string identifier)
        {
            var success = await _client.DeleteMessage(_configuration.Channel, identifier).ConfigureAwait(false);
            return success.Ok;
        }

        public async Task<bool> NotifyFailedPullRequestBuild(Build build, string identifier)
        {
            var pullRequestOwner = await _client.GetUserByEmail(build.PullRequest.AuthorEmail).ConfigureAwait(false);
            var payload = SlackMessageFormatter.FormatFailedBuild(build, _configuration.Channel, identifier, pullRequestOwner);
            var success = await _client.PostMessage(payload).ConfigureAwait(false);
            return success.Ok;
        }

        private async Task<string> GetAuthorProfilePictureUrl(PullRequest pr)
        {
            var user = await _client.GetUserByEmail(pr.AuthorEmail).ConfigureAwait(false);
            return user?.SlackUser.Profile.Image512;
        }
    }
}
