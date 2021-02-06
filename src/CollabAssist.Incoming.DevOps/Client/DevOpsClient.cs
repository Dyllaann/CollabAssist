using System;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CollabAssist.Incoming.Models;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using Newtonsoft.Json;

namespace CollabAssist.Incoming.DevOps.Client
{
    public class DevOpsClient : IDevOpsClient
    {
        private const string PrRegex = "https://(.*?)/(.*?)/(.*?)/_git/(.*?)/pullrequest/([0-9]*)";
        private readonly GitHttpClient _gitHttpClient;

        public DevOpsClient(GitHttpClient gitHttpClient)
        {
            _gitHttpClient = gitHttpClient;
        }

        public async Task<string> GetPullRequestMetaData(PullRequest pr, string key)
        {
            
            var match = Regex.Match(pr.Url, PrRegex);
            if (match.Success)
            {
                var projectId = match.Groups[3].Value;
                var repositoryId = match.Groups[4].Value;
                var pullRequestId = int.Parse(match.Groups[5].Value);
                var properties = await _gitHttpClient.GetPullRequestPropertiesAsync(projectId, repositoryId, pullRequestId).ConfigureAwait(false);
                return (string)properties.FirstOrDefault(p => p.Key == key).Value;
            }
            return null;
        }

        public bool StorePullRequestMetadata(PullRequest pr, string key, string data)
        {


            return false;
        }
    }
}
