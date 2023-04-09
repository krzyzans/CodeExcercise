using System;
using System.Net;
using System.Threading.Tasks;
using CodeExcercise.Common.Exceptions;
using CodeExcercise.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CodeExcercise.Common.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ErrorHandlingMiddleware> logger;

        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
        {
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                ErrorResponse errorResponse = HandleError(ex, context);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = errorResponse.StatusCode;
                await context.Response.WriteAsync(errorResponse.ToString());
            }
        }

        private ErrorResponse HandleError(Exception ex, HttpContext context)
        {
            switch (ex)
            {
                case ValidationErrorException e:
                    return new ErrorResponse(e.Message, (int)HttpStatusCode.BadRequest);
                case CustomerNotExistsException e:
                    return new ErrorResponse(e.Message, (int)HttpStatusCode.BadRequest);
                default:
                    logger.LogError(context.TraceIdentifier, ex);
                    return new ErrorResponse("Internal server error.", (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
