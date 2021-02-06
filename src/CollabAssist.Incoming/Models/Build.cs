using System;

namespace CollabAssist.Incoming.Models
{
    public class Build
    {
        public IncomingType IncomingType { get; set; }

        public string Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public BuildStatus Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public string OwnerName { get; set; }
        public string OwnerEmail { get; set; }
    }
}
