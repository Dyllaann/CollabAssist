using System;
using CollabAssist.Incoming.Models;
using Newtonsoft.Json;

namespace CollabAssist.Incoming.DevOps.Models
{
    public class DevOpsBuildNotification
    {
        [JsonProperty("subscriptionId")]
        public Guid SubscriptionId { get; set; }

        [JsonProperty("notificationId")]
        public int NotificationId { get; set; }

        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("eventType")]
        public string EventType { get; set; }

        [JsonProperty("resource")]
        public BuildResource Resource { get; set; }

        [JsonProperty("resourceContainers")]
        public ResourceContainers ResourceContainers { get; set; }


        public Build To()
        {
            var build = new Build
            {
                Id = Resource.Id.ToString(),
                Project = ResourceContainers.Project.Id,
                Url = Resource.Url
            };

            switch (Resource.Status)
            {
                case "succeeded":
                    build.Status = BuildStatus.Succeeded;
                    break;
                case "failed":
                    build.Status = BuildStatus.Succeeded;
                    break;
                case "stopped":
                    build.Status = BuildStatus.Cancelled;
                    break;
                default:
                    build.Status = BuildStatus.Unknown;
                    break;
            }

            return build;
        }
    }
}
