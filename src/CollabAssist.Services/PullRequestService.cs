using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CollabAssist.Incoming;
using CollabAssist.Incoming.Models;
using CollabAssist.Output;

namespace CollabAssist.Services
{
    public class PullRequestService
    {
        private readonly IInputHandler _inputHandler;
        private readonly IOutputHandler _outputHandler;

        public PullRequestService(IInputHandler inputHandler, IOutputHandler outputHandler)
        {
            _inputHandler = inputHandler;
            _outputHandler = outputHandler;
        }

        public async Task<bool> HandleNewPullRequest(PullRequest pr)
        {
            var identifier = await _outputHandler.NotifyNewPullRequest(pr).ConfigureAwait(false);
            if (identifier == null)
            {
                return false;
            }

            return await _inputHandler.StoreIdentifier(pr, "CollabAssist.MessageIdentifier", identifier).ConfigureAwait(false);
        }

        public async Task<bool> HandleUpdatedPullRequest(PullRequest pr)
        {
            var identifier = await _inputHandler.GetIdentifier(pr, "CollabAssist.MessageIdentifier").ConfigureAwait(false);
            if (identifier == null)
            {
                return false;
            }

            if (pr.Status == PullRequestStatus.Abandoned)
            {
                return await _outputHandler.HandleAbandonedPullRequest(pr, identifier).ConfigureAwait(false);
            }

            return await _outputHandler.HandleUpdatedPullRequest(pr, identifier).ConfigureAwait(false);
        }
    }
}
