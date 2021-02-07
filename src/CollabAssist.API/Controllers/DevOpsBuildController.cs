using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CollabAssist.Incoming.DevOps.Models;
using CollabAssist.Services;
using Microsoft.AspNetCore.Mvc;

namespace CollabAssist.API.Controllers
{
    [Route("api/devops/build")]
    public class DevOpsBuildController : ControllerBase
    {
        private readonly BuildService _buildService;

        public DevOpsBuildController(BuildService buildService)
        {
            _buildService = buildService;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> NewBuild([FromBody] DevOpsBuildNotification devopsBuild)
        {
            var build = devopsBuild.To();
            await _buildService.HandleBuild(build);

            return new OkResult();
        }
    }
}
