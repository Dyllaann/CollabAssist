using System;
using System.Linq;
using CollabAssist.Incoming.Models;
using Newtonsoft.Json;

namespace CollabAssist.Incoming.DevOps.Models
{
    public class DevOpsPullRequestNotification
    {
        [JsonProperty("subscriptionId")]
        public Guid SubscriptionId { get; set; }

        [JsonProperty("notificationId")]
        public int NotificationId { get; set; }

        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("eventType")]
        public string EventType { get; set; }

        [JsonProperty("message")]
        public Message Message { get; set; }

        [JsonProperty("detailedMessage")]
        public Message DetailedMessage { get; set; }

        [JsonProperty("resource")]
        public PullRequestResource PullRequestResource { get; set; }

        [JsonProperty("createdDate")]
        public DateTime CreatedDate { get; set; }

        public bool IsValid()
        {
            return SubscriptionId != Guid.Empty
                   && Id != Guid.Empty
                   && NotificationId != 0
                   && EventType != null
                   && PullRequestResource != null;
        }
        public PullRequest To()
        {
            var pr = new PullRequest
            {
                Id = PullRequestResource.PullRequestId.ToString(),
                Title = PullRequestResource.Title,
                CreatedDate = CreatedDate,
                Url = DevOpsUtils.FormatPrUrl(this),
                AuthorName = PullRequestResource.CreatedBy.DisplayName,
                AuthorEmail = PullRequestResource.CreatedBy.UniqueName,

                ProjectName = PullRequestResource.Repository.Project.Name,
                RepositoryName = PullRequestResource.Repository.Name
            };

            if (PullRequestResource.Reviewers != null)
            {
                foreach (var devopsReviewer in PullRequestResource.Reviewers)
                {
                    var reviewer = new Incoming.Models.Reviewer(devopsReviewer.DisplayName);
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

            if (PullRequestResource.Status == "completed")
            {
                pr.Status = PullRequestStatus.Completed;
            }
            else if (PullRequestResource.Status == "abandoned")
            {
                pr.Status = PullRequestStatus.Abandoned;
            }
            else if (PullRequestResource.Status == "active" && pr.Reviewers.Count == 0)
            {
                pr.Status = PullRequestStatus.New;
            }
            else if (PullRequestResource.Status == "active" && pr.Reviewers.Count > 0)
            {
                if (pr.Reviewers.TrueForAll(r => r.Vote == Vote.Approved || r.Vote == Vote.ApprovedWithSuggestions))
                {
                    pr.Status = PullRequestStatus.ApprovedUncompleted;
                }
                else if (pr.Reviewers.Count(r => r.Vote == Vote.Rejected) > 0)
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