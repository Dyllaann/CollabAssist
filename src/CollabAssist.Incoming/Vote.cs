using System;
using System.Collections.Generic;
using System.Text;

namespace CollabAssist.Incoming
{
    public enum Vote
    {
        Unknown,
        Rejected,
        Waiting,
        ApprovedWithSuggestions,
        Approved
    }
}
