using FluentValidation;

using EventPlanner.Domain.Exceptions;
using EventPlanner.Domain.Exceptions.Interfaces;

using Microsoft.AspNetCore.Diagnostics;

using System.Net;
using System.Text.Json;

namespace EventPlanner.Api.Extensions;

public static class ErrorHandlerExtension
{
	public static void UseErrorHandler(this IApplicationBuilder app)
	{
		_ = app.UseExceptionHandler(appError =>
		{
			appError.Run(async context =>
			{
				var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
				if (contextFeature == null)
				{
					return;
				}

				context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
				context.Response.ContentType = "application/json";

				context.Response.StatusCode = contextFeature.Error switch
				{
					OperationCanceledException => (int)HttpStatusCode.ServiceUnavailable,
					InvalidUserException => (int)HttpStatusCode.BadRequest,
					InvalidRefreshTokenException => (int)HttpStatusCode.BadRequest,
                    BookedLocationException => (int)HttpStatusCode.BadRequest,
                    NotFoundException => (int)HttpStatusCode.NotFound,
					ValidationException => (int)HttpStatusCode.PreconditionFailed,
					ExpiredRefreshTokenException =>(int)HttpStatusCode.Forbidden,
					_ => (int)HttpStatusCode.InternalServerError
				};

				var errorList = contextFeature.Error switch
				{
					OperationCanceledException => new[]
					{
						new
						{
							errorType = "SimpleError",
							errorCode = "OperationCanceled",
							message = (contextFeature.Error as OperationCanceledException)!.Message,
						} as object
					},
					ISimpleError => new[]
					{
						new
						{
							errorType = "SimpleError",
							errorCode = (contextFeature.Error as ISimpleError)!.ErrorCode,
							message = (contextFeature.Error as ISimpleError)!.Message,
						} as object
					},
					ITemplatedError => new[]
					{
						new
						{
							errorType = "TemplatedError",
							errorCode = (contextFeature.Error as ITemplatedError)!.ErrorCode,
							message = (contextFeature.Error as ITemplatedError)!.Message,
							messageDetails = (contextFeature.Error as ITemplatedError)!.MessageDetails,
						} as object
					},
					ValidationException => (contextFeature.Error as ValidationException)!
						.Errors
						.Select(validationFailure => new
						{
							errorType = "ValidationError",
							errorCode = validationFailure.ErrorCode,
							message = validationFailure.ErrorMessage,
							propertyName = validationFailure.PropertyName,
						} as object
					),
					_ => new[]
					{
						new
						{
							errorType = "Error",
							message = contextFeature.Error.Message,
						} as object
					}.ToList()
				};

				var errorResponse = new
				{
					status = context.Response.StatusCode,
					errors = errorList,
				};

				await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
			});
		});
	}
}