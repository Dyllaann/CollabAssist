using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CollabAssist.Configuration;
using CollabAssist.Output.Slack.Models;
using Newtonsoft.Json;

namespace CollabAssist.Output.Slack.Client
{
    public class SlackClient : ISlackClient
    {
        private readonly HttpClient _httpClient;

        public SlackClient(SlackConfiguration configuration, HttpClient httpclient)
        {
            _httpClient = httpclient;

            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {configuration.OAuthToken}");
        }

        public async Task<SlackResponse> SendPayload(SlackPayload payload)
        {
            var httpContent = payload.Serialize();
            var request = _httpClient.PostAsync("/api/chat.postMessage", new StringContent(httpContent, Encoding.UTF8, "application/json"));
            var response = await request.ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                var responseBody = JsonConvert.DeserializeObject<SlackResponse>(body);
                return responseBody;
            }

            return null;
        }
    }
}
