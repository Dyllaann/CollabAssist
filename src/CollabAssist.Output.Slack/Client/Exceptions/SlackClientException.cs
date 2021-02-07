using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json.Serialization;

namespace CollabAssist.Output.Slack.Client.Exceptions
{
    public class SlackClientException : Exception
    {
        public SlackClientException(string message) : base(message)
        {
        }
    }
}
