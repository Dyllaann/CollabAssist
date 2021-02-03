using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CollabAssist.Incoming;
using CollabAssist.Output.Slack.Client;

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
            var payload = SlackMessageFormatter.FormatNewPullRequest(pr, _configuration.Channel);
            var success = await _client.SendPayload(payload);
            if (success.Ok)
            {
                return success.Timestamp;
            }

            return null;
        }
    }
}
