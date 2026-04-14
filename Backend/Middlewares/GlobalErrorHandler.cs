using Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace Backend.Api.Middleware
{
    public sealed class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly IHostEnvironment _env;
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(IHostEnvironment env, ILogger<GlobalExceptionHandler> logger)
        {
            _env = env;
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken ct)
        {
            // always log the full exception
            _logger.LogError(exception, "Unhandled exception: {Message}", exception.Message);

            if (exception is ApiException apiEx)
            {
                await Results.Problem(
                    statusCode: apiEx.StatusCode,
                    detail: apiEx.Message
                ).ExecuteAsync(context);
                return true;
            }

            if (_env.IsDevelopment())
                return false;

            await Results.Problem(
                statusCode: 500,
                detail: "حدث خطأ غير متوقع."
            ).ExecuteAsync(context);
            return true;
        }
    }
}
