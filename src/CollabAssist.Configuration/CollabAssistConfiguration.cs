using System;
using System.Net.Security;

namespace CollabAssist.Configuration
{
    public class CollabAssistConfiguration
    {
        public Auth Auth { get; set; }
        public SlackConfiguration Slack { get; set; }
    }

    public class Auth
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class SlackConfiguration
    {
        public string OAuthToken { get; set; }
        public string Channel { get; set; }
    }
}
