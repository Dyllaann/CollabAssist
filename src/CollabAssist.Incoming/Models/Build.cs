using System;

namespace CollabAssist.Incoming.Models
{
    public class Build
    {
        public string Id { get; set; }
        public string Project { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public BuildStatus Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public string OwnerName { get; set; }
        public string OwnerEmail { get; set; }

        public bool HasPullRequestLinked { get; set; }
        public string PullRequestUrl { get; set; }
    }
}
