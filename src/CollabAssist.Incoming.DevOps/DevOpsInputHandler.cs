using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CollabAssist.Incoming.DevOps.Client;
using CollabAssist.Incoming.Models;

namespace CollabAssist.Incoming.DevOps
{
    public class DevOpsInputHandler : IInputHandler
    {
        private readonly IDevOpsClient _devopsclient;

        public DevOpsInputHandler(IDevOpsClient devopsclient)
        {
            _devopsclient = devopsclient;
        }

        public async Task<string> GetIdentifier(PullRequest pr, string key)
        {
            return await _devopsclient.GetPullRequestMetaData(pr, key).ConfigureAwait(false);
        }


        public async Task<bool> StoreIdentifier(PullRequest pr, string key, string identifier)
        {
            return await _devopsclient.StorePullRequestMetadata(pr, key, identifier);
        }

        public async Task<Build> LinkBuildWithPr(Build build)
        {
            build = await _devopsclient.LinkBuildWithPr(build).ConfigureAwait(false);
            if (build.PullRequest != null)
            {
                build = await _devopsclient.FillPullRequestMetadataFromUrl(build);
            }

            return build;
        }
    }
}
