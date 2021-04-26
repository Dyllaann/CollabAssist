using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CollabAssist.API.Controllers
{
    [Route("api/health")]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        [Route("status")]
        public IActionResult GetStatus()
        {
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
