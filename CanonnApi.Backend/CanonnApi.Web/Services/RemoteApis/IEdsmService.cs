using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CanonnApi.Web.DatabaseModels;

namespace CanonnApi.Web.Services.RemoteApis
{
	public interface IEdsmService
	{
		Task<IEnumerable<(DatabaseModels.System System, bool Updated, string ErrorMessage)>> FetchSystemIds(IEnumerable<DatabaseModels.System> systems);
		Task<IEnumerable<(Body Body, Boolean Updated, string ErrorMessage)>> FetchBodyIds(IEnumerable<Body> bodies);
	}
}
