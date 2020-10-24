using System;
using System.Collections.Generic;
using System.Text;
using CollabAssist.Configuration;

namespace CollabAssist.External.Slack.Client
{
    public class SlackClient : ISlackClient
    {
        private readonly SlackConfiguration _configuration;

        public SlackClient(SlackConfiguration configuration)
        {
            _configuration = configuration;
        }


    }
}
