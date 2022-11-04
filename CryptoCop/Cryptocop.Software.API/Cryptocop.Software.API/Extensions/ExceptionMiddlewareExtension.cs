using Cryptocop.Software.API.Models.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using System.Net;

namespace Cryptocop.Software.API.Extensions
{
    public static class MiddleWareExtension
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(error =>
            {
                error.Run(async context =>
                {
                    var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var exception = exceptionHandlerFeature?.Error;
                    var statusCode = (int)HttpStatusCode.InternalServerError;


                    if (exception is ResourceNotFoundException)
                    {
                        statusCode = (int)HttpStatusCode.NotFound;
                    }
                    else if (exception is ArgumentOutOfRangeException)
                    {
                        statusCode = (int)HttpStatusCode.BadRequest;
                    }


                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = statusCode;
                    Console.WriteLine(exception?.StackTrace);
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                    {
                        StatusCode = statusCode,
                        Message = exception?.Message,
                        StackTrace = exception?.StackTrace?.ToString() //For development only, remove before release.
                    }));
                });
            });
        }
    }
}