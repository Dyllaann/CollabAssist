using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CollabAssist.Incoming.Models;

namespace CollabAssist.Incoming
{
    public interface IInputHandler
    {
        Task<string> GetIdentifier(PullRequest pr, string key);
        Task<bool> StoreIdentifier(PullRequest pr, string key, string identifier);

        Task<Build> LinkBuildWithPr(Build build);
    }
}
