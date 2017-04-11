using System.Collections.Generic;
using System.Threading.Tasks;
using CanonnApi.Web.DatabaseModels;

namespace CanonnApi.Web.Services.DataAccess
{
	public interface IObeliskRepository : IBaseDataRepository<Obelisk>
	{
		Task<List<Obelisk>> Search(int ruintypeId, int obeliskgroupId);
	}
}
