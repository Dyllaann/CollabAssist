using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CollabAssist.API.Handlers
{
    public class UnhandeledExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly SettingsConfiguration _settings;

        public UnhandeledExceptionMiddleware(RequestDelegate next, ILogger logger, SettingsConfiguration settings)
        {
            _next = next;
            _logger = logger;
            _settings = settings;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next.Invoke(httpContext);

                if (_settings.AlwaysReturnOk && httpContext.Response.StatusCode != 200 && httpContext.Response.StatusCode != 401)
                {
                    httpContext.Response.StatusCode = 200;
                }
            }
            catch(Exception ex)
            {
                _logger.Fatal(ex, "Unhandled Exception");
                httpContext.Response.Body = null;
                httpContext.Response.StatusCode = _settings.AlwaysReturnOk ? 200 : 500;
            }

        }
    }
}
