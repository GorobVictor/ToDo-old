using Core.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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
            catch (FriendlyException ex)
            {
                context.Response.StatusCode = (int)ex.HttpCode;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(new { message = ex.Message, code = ex.Code, field = ex.Field }));
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
