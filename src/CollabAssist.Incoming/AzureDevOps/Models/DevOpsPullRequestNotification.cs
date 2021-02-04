using System;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using CollabAssist.Output.AzureDevOps.Models;
using Newtonsoft.Json;

namespace CollabAssist.Incoming.AzureDevOps.Models
{
    public class DevOpsPullRequestNotification
    {
        [JsonProperty("subscriptionId")] public Guid SubscriptionId { get; set; }

        [JsonProperty("notificationId")] public int NotificationId { get; set; }

        [JsonProperty("id")] public Guid Id { get; set; }

        [JsonProperty("eventType")] public string EventType { get; set; }

        [JsonProperty("message")] public Message Message { get; set; }

        [JsonProperty("detailedMessage")] public Message DetailedMessage { get; set; }

        [JsonProperty("resource")] public Resource Resource { get; set; }

        [JsonProperty("createdDate")] public DateTime CreatedDate { get; set; }


        public bool IsValid()
        {
            return SubscriptionId != Guid.Empty
                   && Id != Guid.Empty
                   && NotificationId != 0
                   && EventType != null
                   && Resource != null;
        }

        public PullRequest To()
        {
            var pr = new PullRequest
            {
                Id = Id.ToString(),
                Title = Resource.Title,
                CreatedDate = CreatedDate,
                Url = DevOpsUtils.FormatPrUrl(this),
                AuthorName = Resource.CreatedBy.DisplayName,
                AuthorEmail = Resource.CreatedBy.UniqueName,

                ProjectName = Resource.Repository.Project.Name,
                RepositoryName = Resource.Repository.Name
            };

            if (Resource.Reviewers != null)
            {
                foreach (var devopsReviewer in Resource.Reviewers)
                {
                    var reviewer = new Reviewer(devopsReviewer.DisplayName);
                    switch (devopsReviewer.Vote)
                    {
                        case ReviewerVote.Rejected:
                            reviewer.Vote = Vote.Rejected;
                            break;
                        case ReviewerVote.WaitingForAuthor:
                            reviewer.Vote = Vote.Waiting;
                            break;
                        case ReviewerVote.ApprovedWithSuggestions:
                            reviewer.Vote = Vote.ApprovedWithSuggestions;
                            break;
                        case ReviewerVote.Approved:
                            reviewer.Vote = Vote.Approved;
                            break;
                        default:
                            reviewer.Vote = Vote.Unknown;
                            break;
                    }
                    pr.Reviewers.Add(reviewer);
                }
            }

            if (Resource.Status == "completed")
            {
                pr.Status = PullRequestStatus.Completed;
            }
            else if (Resource.Status == "abandoned")
            {
                pr.Status = PullRequestStatus.Abandoned;
            }
            else if (Resource.Status == "active" && pr.Reviewers.Count == 0)
            {
                pr.Status = PullRequestStatus.New;
            }
            else if (Resource.Status == "active" && pr.Reviewers.Count > 0)
            {
                if (pr.Reviewers.TrueForAll(r => r.Vote == Vote.Approved || r.Vote == Vote.ApprovedWithSuggestions))
                {
                    pr.Status = PullRequestStatus.ApprovedUncompleted;
                }
                else if(pr.Reviewers.Count(r => r.Vote == Vote.Rejected) > 0)
                {
                    pr.Status = PullRequestStatus.Rejected;
                }
                else
                {
                    pr.Status = PullRequestStatus.WaitingApproval;
                }
            }
            else
            {
                pr.Status = PullRequestStatus.Unknown;
            }

            return pr;
        }

    }
}