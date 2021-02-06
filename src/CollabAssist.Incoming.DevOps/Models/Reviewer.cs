using Newtonsoft.Json;

namespace CollabAssist.Incoming.DevOps.Models
{
    public class Reviewer
    {
        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("vote")]

        public ReviewerVote Vote { get; set; }

    }

    public enum ReviewerVote
    {
        Approved = 10,
        ApprovedWithSuggestions = 5,
        NoVote = 0,
        WaitingForAuthor = -5,
        Rejected = -10
    }
}
