//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.Hosting;
//using Microsoft.Extensions.Logging;
//using System;
//using System.Threading.Tasks;

//namespace FinalProjectMvc.Middlewares
//{
//    public class GlobalExceptionHandlerMiddleware
//    {
//        private readonly RequestDelegate _next;
//        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;
//        private readonly IHostEnvironment _env;

//        public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger, IHostEnvironment env)
//        {
//            _next = next;
//            _logger = logger;
//            _env = env;
//        }

//        public async Task InvokeAsync(HttpContext context)
//        {
//            try
//            {
//                await _next(context);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Exception occurred: {Message}", ex.Message);
//                await HandleExceptionAsync(context, ex);
//            }
//        }

//        private Task HandleExceptionAsync(HttpContext context, Exception exception)
//        {
//            int statusCode = exception switch
//            {
//                ArgumentNullException or BadHttpRequestException => StatusCodes.Status400BadRequest,
//                KeyNotFoundException => StatusCodes.Status404NotFound,
//                UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
//                _ => StatusCodes.Status500InternalServerError
//            };

//            var errorPath = $"/Home/Error?statusCode={statusCode}";

//            context.Response.Redirect(errorPath);

//            return Task.CompletedTask;
//        }
//    }
//}
