using System.Text.RegularExpressions;
using System.Web;
using CollabAssist.Incoming.DevOps.Models;

namespace CollabAssist.Incoming.DevOps
{
    public static class DevOpsUtils
    {
        private const string OrganizationRegex = @"(https:\/\/dev.azure.com/)(.*?)(\/.*)";

        public static string FormatPrUrl(DevOpsPullRequestNotification pr)
        {
            var projectUrl = pr.Resource.Repository.Project.Url;
            var regex = Regex.Match(projectUrl, OrganizationRegex);
            if (regex.Success)
            {
                var org = HttpUtility.UrlEncode(regex.Groups[2].Value);
                var project = HttpUtility.UrlEncode(pr.Resource.Repository.Project.Name);
                var repository = HttpUtility.UrlEncode(pr.Resource.Repository.Name);
                var id = pr.Resource.PullRequestId;

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
