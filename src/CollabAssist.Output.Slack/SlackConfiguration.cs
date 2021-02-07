using System;
using System.Collections.Generic;
using System.Text;

namespace CollabAssist.Output.Slack
{
    public class SlackConfiguration
    {
        public string OAuthToken { get; set; }
        public string Channel { get; set; }
    }
}
