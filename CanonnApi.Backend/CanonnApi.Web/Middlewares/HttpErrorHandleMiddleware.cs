using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Logging;
using HttpContext = Microsoft.AspNetCore.Http.HttpContext;

namespace CanonnApi.Web.Middlewares
{
	public class HttpErrorHandleMiddleware
	{
		private readonly ILogger _logger;
		private readonly RequestDelegate _next;

		public HttpErrorHandleMiddleware(RequestDelegate next, ILogger<HttpErrorHandleMiddleware> logger)
		{
			_next = next ?? throw new ArgumentNullException(nameof(next));
			_logger = logger;
		}

		public async Task Invoke(HttpContext context)
		{
			try
			{
				await _next.Invoke(context);
			}
			catch (HttpException httpException)
			{
				_logger?.LogInformation("Http error occured: {0}", httpException);

				context.Response.StatusCode = httpException.StatusCode;
				var responseFeature = context.Features.Get<IHttpResponseFeature>();
				responseFeature.ReasonPhrase = httpException.Message;
			}
		}
	}
}
