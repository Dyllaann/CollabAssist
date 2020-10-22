using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CollabAssist.API.Controllers
{
    [Route("build")]
    public class BuildController : ControllerBase
    {

        public BuildController()
        {
        }

        [HttpGet]
        public IActionResult Get()
        {
            return new OkResult();
        }
    }
}
