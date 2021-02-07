using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CollabAssist.Output.Slack.Client.Exceptions;
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

        public async Task<SlackResponse> PostMessage(SlackPayload payload)
        {
            var serialized = payload.Serialize();
            var httpContent = new StringContent(payload.Serialize(), Encoding.UTF8, "application/json");
            var request = _httpClient.PostAsync("/api/chat.postMessage", httpContent);
            var response = await request.ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                var responseBody = JsonConvert.DeserializeObject<SlackResponse>(body);
                if (responseBody.Ok)
                {
                    return responseBody;
                }
                throw new SlackClientException(responseBody.Error);
            }

            return null;
        }

        public async Task<SlackResponse> UpdateMessage(SlackPayload payload)
        {
            var httpContent = new StringContent(payload.Serialize(), Encoding.UTF8, "application/json");
            var request = _httpClient.PostAsync("/api/chat.update", httpContent);
            var response = await request.ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                var responseBody = JsonConvert.DeserializeObject<SlackResponse>(body);
                if (responseBody.Ok)
                {
                    return responseBody;
                }
                throw new SlackClientException(responseBody.Error);
            }

            return null;
        }

        public Task<SlackResponse> PostMessageAsThread(string channel, string timestamp)
        {
            throw new NotImplementedException();
        }

        public async Task<SlackResponse> DeleteMessage(string channel, string timestamp)
        {
            var httpContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("channel", channel),
                new KeyValuePair<string, string>("ts", timestamp)
            });
            var request = _httpClient.PostAsync("/api/chat.delete", httpContent);
            var response = await request.ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                var responseBody = JsonConvert.DeserializeObject<SlackUserResponse>(body);
                if (responseBody.Ok)
                {
                    return responseBody;
                }
                throw new SlackClientException(responseBody.Error);
            }
            return null;
        }

        public async Task<SlackUserResponse> GetUserByEmail(string email)
        {
            var httpContent = new FormUrlEncodedContent(new []
            {
                new KeyValuePair<string, string>("email", email)
            });
            var request = _httpClient.PostAsync("/api/users.lookupByEmail", httpContent);
            var response = await request.ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                var responseBody = JsonConvert.DeserializeObject<SlackUserResponse>(body);
                if (responseBody.Ok)
                {
                    return responseBody;
                }
                throw new SlackClientException(responseBody.Error);
            }

            return null;
        }
    }
}
