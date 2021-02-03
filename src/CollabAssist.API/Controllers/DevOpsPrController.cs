using CollabAssist.Incoming.AzureDevOps.Models;
using CollabAssist.Output;
using CollabAssist.Output.Slack;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CollabAssist.API.Controllers
{
    [Route("api/devops/pr")]
    public class DevOpsPrController : ControllerBase
    {
        private readonly IOutputHandler _outputHandler;

        public DevOpsPrController(IOutputHandler outputHandler)
        {
            _outputHandler = outputHandler;
        }

        [HttpPost]
        [Route("new")]
        public IActionResult NewPr([FromBody] DevOpsPullRequestNotification devopspr)
        {
            if (!devopspr.IsValidNewPr())
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            var pr = devopspr.To();
            _outputHandler.NotifyNewPullRequest(pr);

            return new OkResult();
        }

        [HttpPost]
        [Route("update")]
        public IActionResult UpdatedPr([FromBody] DevOpsPullRequestNotification pr)
        {
            if (!pr.IsValidNewPr())
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return new OkResult();
        }
    }
}
