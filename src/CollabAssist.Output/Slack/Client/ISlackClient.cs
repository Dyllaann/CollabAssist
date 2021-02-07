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
        Task<SlackResponse> PostMessage(SlackPayload payload);
        Task<SlackResponse> UpdateMessage(SlackPayload payload);
        Task<SlackResponse> PostMessageAsThread(string channel, string timestamp);
        Task<SlackResponse> DeleteMessage(string channel, string timestamp);
        Task<SlackUserResponse> GetUserByEmail(string email);

    }
}
