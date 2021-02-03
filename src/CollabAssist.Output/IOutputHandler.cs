using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CollabAssist.Incoming;

namespace CollabAssist.Output
{
    public interface IOutputHandler
    {
        Task<string> NotifyNewPullRequest(PullRequest pr);
    }
}
