namespace WebApi.Middlewares
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Net;
    using System.Threading.Tasks;
    using WebApi.Models;

    public class ExceptionHandlerMiddleware
    {
        private const string JsonContentType = "application/json";
        private readonly RequestDelegate request;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionHandlerMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next.</param>
        public ExceptionHandlerMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            request = next;
            logger = loggerFactory.CreateLogger<ExceptionHandlerMiddleware>();
        }

        /// <summary>
        /// Invokes the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public Task Invoke(HttpContext context) => InvokeAsync(context);

        async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await request(context);
            }
            catch (AppException exception)
            {
                logger.LogError($"Error, status code: {context.Response.StatusCode}. Error exception {exception}");
                await HandleExceptionAsync(context, exception);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            ErrorResponse response = new ErrorResponse()
            {
                Message = exception.Message
            };

            var exceptionType = exception.GetType();

            switch (exception)
            {
                case var _ when exception is ValidationException:
                    response.Status = (int)HttpStatusCode.UnprocessableEntity;
                    break;

                case Exception _ when exceptionType == typeof(UnauthorizedAccessException):
                    response.Status = (int)HttpStatusCode.Unauthorized;
                    break;

                case AppException e when exceptionType == typeof(AppException):
                    response.Status = e.Code;
                    break;

                default:
                    response.Status = (int)HttpStatusCode.InternalServerError;
                    response.Message = "Internal Server Error";
                    break;
            }
            context.Response.StatusCode = response.Status;
            context.Response.ContentType = JsonContentType;

            return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
    }
}