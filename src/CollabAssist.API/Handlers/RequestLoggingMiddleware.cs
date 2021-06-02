using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Serilog;

namespace CollabAssist.API.Handlers
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Path.Value != "/api/health/status")
            {
                _logger.Information("Request start {url}", httpContext.Request.GetEncodedUrl());
            }

            await _next.Invoke(httpContext);

            if (httpContext.Request.Path.Value != "/api/health/status")
            {
                _logger.Information("Request end {url}: {statuscode}", httpContext.Request.GetEncodedUrl(), httpContext.Response.StatusCode);
            }
        }
    }
}
