using System;
using System.Collections.Generic;
using System.Text;

namespace CollabAssist.Incoming
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
