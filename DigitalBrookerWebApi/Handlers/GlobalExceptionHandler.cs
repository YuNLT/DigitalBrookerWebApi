using DigitalBroker.Application.Exception;
using DigitalBrokker.Infrastructure.Exceptions;
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
            var (statusCode, message) = GetExceptionDetails(exception); 
            _logger.LogError(exception, message);                       
            httpContext.Response.StatusCode = (int)statusCode;  
            await httpContext.Response.WriteAsJsonAsync(message, cancellationToken);

            return true;
        }

        private (HttpStatusCode statusCode, string message) GetExceptionDetails(Exception exception)
        {
            return exception switch
            {
                LoginFailException => (HttpStatusCode.Unauthorized, exception.Message),
                UserAlreadyExistException => (HttpStatusCode.Conflict, exception.Message),
                RegistrationfailException => (HttpStatusCode.BadRequest, exception.Message),
                RefreshTokenException => (HttpStatusCode.Unauthorized, exception.Message),
                ForgetPasswordFailException => (HttpStatusCode.BadRequest, exception.Message),
                EmptyResetPasswordTokenException => (HttpStatusCode.BadRequest, exception.Message),
                MissingFieldException => (HttpStatusCode.BadRequest, exception.Message),
                ResetPasswordTokenException => (HttpStatusCode.Unauthorized, exception.Message),
                UserNotFoundException => (HttpStatusCode.NotFound, exception.Message),
                EmptyUserException => (HttpStatusCode.NotFound, exception.Message),
                PasswordUpdateError => (HttpStatusCode.BadRequest, exception.Message),
                IsActiveException => (HttpStatusCode.Unauthorized, exception.Message),
                PropertyCreationException => (HttpStatusCode.BadRequest, exception.Message),
                _ => (HttpStatusCode.InternalServerError, $"An unexpected error occurred: {exception.Message}")
            };
        }
    }
}
