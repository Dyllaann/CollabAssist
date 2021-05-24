using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Serilog;

namespace CollabAssist.API.Handlers
{
    public class UnhandeledExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public UnhandeledExceptionMiddleware(RequestDelegate next, ILogger logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next.Invoke(httpContext);
            }
            catch(Exception ex)
            {
                _logger.Fatal(ex, "Unhandled Exception");
                httpContext.Response.Body = null;
                httpContext.Response.StatusCode = 500;
            }

        }
    }
}
