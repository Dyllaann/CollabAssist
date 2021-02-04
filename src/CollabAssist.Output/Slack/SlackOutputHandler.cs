using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CollabAssist.Incoming;
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

        public async Task<bool> HandleUpdatedPullRequest(PullRequest update, string inputHandlerId = null)
        {
            var profilePicture = await GetAuthorProfilePictureUrl(update).ConfigureAwait(false);
            var payload = profilePicture != null
                ? SlackMessageFormatter.FormatUpdatedPullRequestWithImage(update, _configuration.Channel, profilePicture)
                : SlackMessageFormatter.FormatUpdatedPullRequest(update, _configuration.Channel);

            var success = await _client.UpdateMessage(payload);
            return success.Ok;
        }

        public async Task<bool> HandleAbandonedPullRequest(PullRequest update, string inputHandlerId = null)
        {
            var success = await _client.DeleteMessage(_configuration.Channel, inputHandlerId).ConfigureAwait(false);
            return success.Ok;
        }

        private async Task<string> GetAuthorProfilePictureUrl(PullRequest pr)
        {
            var user = await _client.GetUserByEmail(pr.AuthorEmail).ConfigureAwait(false);
            return user?.SlackUser.Profile.Image512;
        }
    }
}
