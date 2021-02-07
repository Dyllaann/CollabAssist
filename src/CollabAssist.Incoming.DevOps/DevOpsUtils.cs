using System.Text.RegularExpressions;
using CollabAssist.Incoming.DevOps.Models;

namespace CollabAssist.Incoming.DevOps
{
    public static class DevOpsUtils
    {
        private const string OrganizationRegex = @"(https:\/\/dev.azure.com/)(.*?)(\/.*)";

        public static string FormatPrUrl(DevOpsPullRequestNotification pr)
        {
            var projectUrl = pr.PullRequestResource.Repository.Project.Url;
            var regex = Regex.Match(projectUrl, OrganizationRegex);
            if (regex.Success)
            {
                var org = regex.Groups[2];
                var project = pr.PullRequestResource.Repository.Project.Name;
                var repository = pr.PullRequestResource.Repository.Name;
                var id = pr.PullRequestResource.PullRequestId;

                return $"https://dev.azure.com/{org}/{project}/_git/{repository}/pullrequest/{id}";
            }

            return null;
        }

        public static string FormatPrUrl(string baseUrl, string project, string repository, string id)
        {
            return $"{baseUrl}/{project}/_git/{repository}/pullrequest/{id}";
        }
    }
}
