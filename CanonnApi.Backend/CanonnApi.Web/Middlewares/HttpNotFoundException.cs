using System.Net;

namespace CanonnApi.Web.Middlewares
{
	public class HttpNotFoundException: HttpException
	{
		public HttpNotFoundException()
			: base(HttpStatusCode.NotFound, "Not found")
		{ }
	}
}
