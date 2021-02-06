using System.Threading.Tasks;
using CollabAssist.Incoming.Models;

namespace CollabAssist.Incoming.DevOps.Client
{
    public interface IDevOpsClient
    {
        Task<string> GetPullRequestMetaData(PullRequest pr, string key);
        bool StorePullRequestMetadata(PullRequest pr, string key, string data);
    }
}
