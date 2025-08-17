using System.Net;
using System.Text.Json;

namespace product.Api.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception");

                var problem = new
                {
                    type = "https://httpstatuses.io/500",
                    title = "Beklenmeyen bir hata oluştu.",
                    status = (int)HttpStatusCode.InternalServerError,
                    detail = context.Request.Path.HasValue
                        ? $"İstek: {context.Request.Method} {context.Request.Path}"
                        : null,
                    traceId = context.TraceIdentifier
                };

                context.Response.ContentType = "application/problem+json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync(JsonSerializer.Serialize(problem));
            }
        }
    }

    public static class ExceptionHandlingExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionHandling(this IApplicationBuilder app)
            => app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}
