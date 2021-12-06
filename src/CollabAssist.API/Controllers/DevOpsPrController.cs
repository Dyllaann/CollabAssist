using System.Threading.Tasks;
using CollabAssist.Incoming.DevOps.Models;
using CollabAssist.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace CollabAssist.API.Controllers
{
    [Authorize]
    [Route("api/devops/pr")]
    public class DevOpsPrController : ControllerBase
    {
        private readonly PullRequestService _prService;
        private readonly SettingsConfiguration _settingsConfiguration;

        public DevOpsPrController(PullRequestService prService, SettingsConfiguration settings)
        {
            _prService = prService;
            _settingsConfiguration = settings;
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

            return BadRequest();
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

            return BadRequest();
        }
    }
}
