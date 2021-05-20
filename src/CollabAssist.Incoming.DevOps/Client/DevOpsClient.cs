using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.Build.WebApi;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using Microsoft.TeamFoundation.TestManagement.WebApi;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.OAuthWhitelist;
using Microsoft.VisualStudio.Services.WebApi.Patch;
using Microsoft.VisualStudio.Services.WebApi.Patch.Json;
using Build = CollabAssist.Incoming.Models.Build;
using PullRequest = CollabAssist.Incoming.Models.PullRequest;

namespace CollabAssist.Incoming.DevOps.Client
{
    public class DevOpsClient : IDevOpsClient
    {
        private const string PrRegex = "https://(.*?)/(.*?)/(.*?)/_git/(.*?)/pullrequest/([0-9]*)";
        private readonly DevOpsConfiguration _config;
        private readonly GitHttpClient _gitHttpClient;
        private readonly BuildHttpClient _buildHttpClient;

        public DevOpsClient(DevOpsConfiguration config, GitHttpClient gitHttpClient, BuildHttpClient buildHttpClient)
        {
            _config = config;
            _gitHttpClient = gitHttpClient;
            _buildHttpClient = buildHttpClient;
        }

        public async Task<string> GetPullRequestMetaData(PullRequest pr, string key)
        {
            var properties = await _gitHttpClient.GetPullRequestPropertiesAsync(pr.ProjectName, pr.RepositoryName, int.Parse(pr.Id)).ConfigureAwait(false);
            return (string)properties.FirstOrDefault(p => p.Key == key).Value;
        }

        public async Task<bool> StorePullRequestMetadata(PullRequest pr, string key, string data)
        {
            var patchDocument = new JsonPatchDocument()
            {
                new JsonPatchOperation
                {
                    Operation = Operation.Add,
                    Path = "/" + key,
                    Value = data
                }
            };

            try
            {
                var newProperties = await _gitHttpClient
                    .UpdatePullRequestPropertiesAsync(patchDocument, pr.ProjectName, pr.RepositoryName,
                        int.Parse(pr.Id)).ConfigureAwait(false);
                return newProperties.ContainsKey(key);
            }
            catch (VssException ex) when (ex.Message.Contains("TF401181: The pull request cannot be edited due to its state."))
            {
                return true;
                //TODO error handling: The PR is probably completed or abandoned and can no longer be updated
            }
            catch (VssException ex) when (ex.Message == "VS30063: You are not authorized to access https://dev.azure.com.")
            {
                //TODO error handling: PAT expired or missing scope "Code -> Read & Write"
                throw;
            }

            return false;
        }

        public async Task<Build> LinkBuildWithPr(Build build)
        {
            try
            {
                var devopsbuild = await _buildHttpClient.GetBuildAsync(build.Project, int.Parse(build.Id));

                if (devopsbuild.Reason != BuildReason.PullRequest)
                {
                    return build;
                }

                var project = devopsbuild.Project.Name;
                var repo = devopsbuild.Repository.Name;
                var prId = devopsbuild.TriggerInfo.FirstOrDefault(k => k.Key == "pr.number").Value;
                var url = DevOpsUtils.FormatPrUrl(_config.BaseUrl, project, repo, prId);

                build.PullRequest = new PullRequest
                {
                    Id = prId,
                    ProjectName = project,
                    RepositoryName = repo,
                    Url = url
                };

                return build;
            }
            catch (VssException ex) when (ex.Message == "VS30063: You are not authorized to access https://dev.azure.com.")
            {
                //TODO error handling: PAT expired or missing scope "Build -> Read"
                throw;
            }
        }

        public async Task<Build> FillPullRequestMetadataFromUrl(Build build)
        {
            try
            {
                var pr = await _gitHttpClient.GetPullRequestAsync(build.PullRequest.ProjectName, build.PullRequest.RepositoryName, int.Parse(build.PullRequest.Id))
                    .ConfigureAwait(false);
                build.PullRequest.AuthorName = pr.CreatedBy.DisplayName;
                build.PullRequest.AuthorEmail = pr.CreatedBy.UniqueName;
                build.PullRequest.CreatedDate = pr.CreationDate;
                build.PullRequest.Title = pr.Title;
                return build;
            }
            catch (VssException ex)// when (ex.Message == "VS30063: You are not authorized to access https://dev.azure.com.")
            {
                //TODO error handling: PAT expired or missing scope "Build -> Read"
                return null;
            }
        }
    }
}
