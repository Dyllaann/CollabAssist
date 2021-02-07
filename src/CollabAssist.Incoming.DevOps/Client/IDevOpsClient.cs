using System.Threading.Tasks;
using CollabAssist.Incoming.Models;

namespace CollabAssist.Incoming.DevOps.Client
{
    public interface IDevOpsClient
    {
        Task<string> GetPullRequestMetaData(PullRequest pr, string key);
        Task<bool> StorePullRequestMetadata(PullRequest pr, string key, string data);

        Task<Build> LinkBuildWithPr(Build build);
        Task<Build> FillPullRequestMetadataFromUrl(Build build);
    }
}
