using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CollabAssist.Incoming;
using CollabAssist.Incoming.Models;
using CollabAssist.Output;

namespace CollabAssist.Services
{
    public class BuildService
    {
        private readonly IInputHandler _inputHandler;
        private readonly IOutputHandler _outputHandler;

        public BuildService(IInputHandler inputHandler, IOutputHandler outputHandler)
        {
            _inputHandler = inputHandler;
            _outputHandler = outputHandler;
        }

        public async Task<bool> HandleBuild(Build build)
        {
            if (build.Status == BuildStatus.Failed)
            {
                build = await _inputHandler.LinkBuildWithPr(build).ConfigureAwait(false);
                if (build.PullRequest != null)
                {
                    var identifier = await _inputHandler.GetIdentifier(build.PullRequest, IdentifierKeys.Identifier);
                    return await _outputHandler.NotifyFailedPullRequestBuild(build, identifier).ConfigureAwait(false);
                }

                return false;
            }

            return true;
        }
    }
}
