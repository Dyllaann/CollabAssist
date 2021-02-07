using System.Threading.Tasks;
using CollabAssist.Incoming.DevOps.Models;
using CollabAssist.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CollabAssist.API.Controllers
{
    [Authorize]
    [Route("api/devops/pr")]
    public class DevOpsPrController : ControllerBase
    {
        private readonly PullRequestService _prService;

        public DevOpsPrController(PullRequestService prService)
        {
            _prService = prService;
        }

        [HttpPost]
        [Route("new")]
        public async Task<IActionResult> NewPr([FromBody] DevOpsPullRequestNotification devopspr)
        {
            if (devopspr.IsValid())
            {
                var pr = devopspr.To();
                if (await _prService.HandleNewPullRequest(pr).ConfigureAwait(false))
                {
                    return new OkResult();
                }
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> UpdatedPr([FromBody] DevOpsPullRequestNotification devopspr)
        {
            if (devopspr.IsValid())
            {
                var pr = devopspr.To();
                if (await _prService.HandleUpdatedPullRequest(pr).ConfigureAwait(false))
                {
                    return new OkResult();
                }
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
