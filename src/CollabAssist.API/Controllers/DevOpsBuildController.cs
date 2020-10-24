using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CollabAssist.API.Controllers
{
    [Route("devops/build")]
    public class DevOpsBuildController : ControllerBase
    {
        [HttpPost]
        [Route("new")]
        public IActionResult Index()
        {
            return new OkResult();
        }
    }
}
