using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebToDo.Middleware
{
    public class ErrorMiddleware
    {
        private ILogger<ErrorMiddleware> Logger { get; set; }

        private RequestDelegate Next { get; set; }

        public ErrorMiddleware(ILogger<ErrorMiddleware> logger, RequestDelegate next)
        {
            Logger = logger;
            Next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await Next.Invoke(context);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync($"{ex.Message}\n{ex.StackTrace}");
            }
        }
    }
}
