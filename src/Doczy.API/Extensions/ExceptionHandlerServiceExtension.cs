using Doczy.Business.DTOs.Common;
using Doczy.Business.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace Doczy.API.Extensions
{
    public static class ExceptionHandlerServiceExtension
    {
        public static IApplicationBuilder AddExceptionHandler(this IApplicationBuilder application)
        {
            application.UseExceptionHandler(error =>
            {
                error.Run(async context =>
                {
                    var feature = context.Features.Get<IExceptionHandlerFeature>();
                    HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
                    string message = feature.Error.Message;

                    if (feature?.Error is IBaseException)
                    {
                        var exception = (IBaseException)feature.Error;
                        statusCode = exception.StatusCode;
                        message = exception.ErrorMessage;
                    }

                    var response = new ResponseDto(statusCode, message);

                    context.Response.StatusCode = (int)statusCode;
                    await context.Response.WriteAsJsonAsync(response);
                    await context.Response.CompleteAsync();
                });



            });

            return application;
        }
    }
}
