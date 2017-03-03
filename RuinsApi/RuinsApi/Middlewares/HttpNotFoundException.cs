using System.Net;

namespace RuinsApi.Middlewares
{
	public class HttpNotFoundException: HttpException
	{
		public HttpNotFoundException()
			: base(HttpStatusCode.NotFound, "Not found")
		{ }
	}
}
