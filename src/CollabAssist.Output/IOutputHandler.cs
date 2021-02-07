using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CollabAssist.Incoming;
using CollabAssist.Incoming.Models;

namespace CollabAssist.Output
{
    public interface IOutputHandler
    {
        Task<string> NotifyNewPullRequest(PullRequest pr);
        Task<bool> HandleUpdatedPullRequest(PullRequest update, string identifier);
        Task<bool> HandleAbandonedPullRequest(PullRequest update, string identifier);

        Task<bool> NotifyFailedPullRequestBuild(Build build, string identifier);
    }
}
