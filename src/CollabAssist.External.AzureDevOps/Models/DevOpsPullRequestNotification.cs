using System;
using Newtonsoft.Json;

namespace CollabAssist.External.AzureDevOps.Models
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

        public bool IsValid()
        {
            return SubscriptionId != Guid.Empty
                   && Id != Guid.Empty
                   && NotificationId != 0
                   && EventType != null
                   && Message != null
                   && Resource != null;
        }
    }
}