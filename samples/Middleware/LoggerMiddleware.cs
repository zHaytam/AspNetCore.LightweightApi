using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Middleware
{
    public class LoggerMiddleware : IMiddleware
    {
        private readonly ILogger<LoggerMiddleware> _logger;

        public LoggerMiddleware(ILogger<LoggerMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            _logger.LogInformation($"Endpoint called: {UriHelper.GetDisplayUrl(context.Request)}");
            await next(context);
        }
    }
}
