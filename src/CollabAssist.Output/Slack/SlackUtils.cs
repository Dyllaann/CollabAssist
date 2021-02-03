using System;
using System.Text.RegularExpressions;
using CollabAssist.Incoming.AzureDevOps.Models;

namespace CollabAssist.Output.Slack
{
    public static class SlackUtils
    {
        private static readonly string _organizationRegex = @"(https:\/\/dev.azure.com/)(.*?)(\/.*)";

        public static string FormatPrUrl(DevOpsPullRequestNotification pr)
        {
            var projectUrl = pr.Resource.Repository.Project.Url;
            var regex = Regex.Match(projectUrl, _organizationRegex);
            if (regex.Success)
            {
                var org = regex.Groups[2];
                var project = pr.Resource.Repository.Project.Name;
                var repository = pr.Resource.Repository.Name;
                var id = pr.Resource.PullRequestId;

                return $"https://dev.azure.com/{org}/{project}/_git/{repository}/pullrequest/{id}";
            }

            throw new Exception("Unable to read organization. Cannot format PR url");
        }
    }
}
