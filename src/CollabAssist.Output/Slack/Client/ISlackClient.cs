using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CollabAssist.Incoming;
using CollabAssist.Output.Slack.Models;

namespace CollabAssist.Output.Slack.Client
{
    public interface ISlackClient
    {
        Task<SlackResponse> SendPayload(SlackPayload payload);
    }
}
