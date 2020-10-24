using CollabAssist.External.AzureDevOps.Models;
using CollabAssist.External.Slack;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CollabAssist.API.Controllers
{
    [Route("devops/pr")]
    public class DevOpsPrController : ControllerBase
    {
        private readonly ISlackPrManager _slackPrManager;

        public DevOpsPrController(ISlackPrManager slackPrManager)
        {
            _slackPrManager = slackPrManager;
        }

        [HttpPost]
        [Route("new")]
        public IActionResult NewPr([FromBody] PullRequestNotification pr)
        {
            if (!pr.IsValid())
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }


            return new OkResult();
        }

        [HttpPost]
        [Route("update")]
        public IActionResult UpdatedPr([FromBody] PullRequestNotification pr)
        {
            if (!pr.IsValid())
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return new OkResult();
        }
    }
}
