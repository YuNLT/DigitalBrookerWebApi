using DigitalBrooker.Domain.Exception;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace DigitalBrookerWebApi.Handlers
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;
        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            //1.get the exception details from the method bellow. 2.log the error. 3.return the response
            var (statusCode, message) = GetExceptionDetails(exception); //1
            _logger.LogError(exception, message);                       //2
            httpContext.Response.StatusCode = (int)statusCode;          //3
                   
            //4.then return the response to the json format
            await httpContext.Response.WriteAsJsonAsync(message, cancellationToken);

            return true;
        }

        //Helper method to get the exception details based on the provided exception
        private (HttpStatusCode statusCode, string message) GetExceptionDetails(Exception exception)
        {
            return exception switch
            {
                LoginFailException => (HttpStatusCode.Unauthorized, exception.Message),
                UserAlreadyExistException => (HttpStatusCode.Conflict, exception.Message),
                RegistrationfailException => (HttpStatusCode.BadRequest, exception.Message),
                RefreshTokenException => (HttpStatusCode.Unauthorized, exception.Message),
                ForgetPasswordFailException => (HttpStatusCode.BadRequest, exception.Message),
                _ => (HttpStatusCode.InternalServerError, $"An unexpected error occurred: {exception.Message}")
            };
        }
    }
}
