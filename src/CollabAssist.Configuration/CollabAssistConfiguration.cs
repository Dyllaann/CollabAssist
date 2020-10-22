using System;
using System.Net.Security;

namespace CollabAssist.Configuration
{
    public class CollabAssistConfiguration
    {
        public Auth Auth { get; set; }
    }

    public class Auth
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
