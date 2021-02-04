using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CollabAssist.Incoming;
using CollabAssist.Output;

namespace CollabAssist.Services
{
    public class PullRequestService
    {
        private readonly IOutputHandler _outputHandler;

        public PullRequestService(IOutputHandler outputHandler)
        {
            _outputHandler = outputHandler;
        }


        public async Task<bool> HandleNewPullRequest(PullRequest pr)
        {
            var identifier = await _outputHandler.NotifyNewPullRequest(pr).ConfigureAwait(false);
            if (identifier == null)
            {
                return false;
            }

            //TODO: Handle storing the identifier in the input.
            //Can be used for retrieving the same message for editing purposes.
            return true;
        }

        public async Task<bool> HandleUpdatedPullRequest(PullRequest pr)
        {
            //TODO: Handle retrieving of the identifier that has been stored in a new pr
            //This is needed because we want to update the same message
            var identifier = string.Empty;

            if (pr.Status == PullRequestStatus.Abandoned)
            {
                return await _outputHandler.HandleAbandonedPullRequest(pr, identifier).ConfigureAwait(false);
            }
            return await _outputHandler.HandleUpdatedPullRequest(pr, identifier).ConfigureAwait(false);
        }
    }
}
