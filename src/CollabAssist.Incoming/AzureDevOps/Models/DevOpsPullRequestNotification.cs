using System;
using CollabAssist.Output.AzureDevOps.Models;
using Newtonsoft.Json;

namespace CollabAssist.Incoming.AzureDevOps.Models
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
        public Resource Resource { get; set; }

        [JsonProperty("createdDate")]
        public DateTime CreatedDate { get; set; }

        public bool IsValidNewPr()
        {
            return SubscriptionId != Guid.Empty
                   && Id != Guid.Empty
                   && NotificationId != 0
                   && EventType != null
                   && Message != null
                   && Resource != null;
        }

        public PullRequest To()
        {
            return new PullRequest
            {
                Id = Id.ToString(),
                Title = Resource.Title,
                CreatedDate = CreatedDate,
                AuthorName = Resource.CreatedBy.DisplayName,
                AuthorEmail = Resource.CreatedBy.UniqueName,

                ProjectName = Resource.Repository.Project.Name,
                RepositoryName = Resource.Repository.Name
            };
        }
    }
}