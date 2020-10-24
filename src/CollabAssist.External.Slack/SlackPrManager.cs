using System;
using System.Collections.Generic;
using System.Text;
using CollabAssist.External.Slack.Client;

namespace CollabAssist.External.Slack
{
    public class SlackPrManager : ISlackPrManager
    {
        private readonly ISlackClient _client;

        public SlackPrManager(ISlackClient client)
        {
            _client = client;
        }
    }
}
