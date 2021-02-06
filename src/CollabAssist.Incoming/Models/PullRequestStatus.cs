namespace CollabAssist.Incoming.Models
{
    public enum PullRequestStatus
    {
        Unknown,
        New,
        Rejected,
        WaitingApproval,
        ApprovedUncompleted,
        Completed,
        Abandoned
    }
}
