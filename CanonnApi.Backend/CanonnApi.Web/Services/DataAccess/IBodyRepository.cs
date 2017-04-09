using System.Collections.Generic;
using System.Threading.Tasks;
using CanonnApi.Web.DatabaseModels;

namespace CanonnApi.Web.Services.DataAccess
{
	public interface IBodyRepository : IBaseDataRepository<Body>
	{
		Task<IEnumerable<Body>> GetAllWithSystems();
	}
}
