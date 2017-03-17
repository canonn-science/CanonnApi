using System.Collections.Generic;
using System.Threading.Tasks;
using RuinsApi.DatabaseModels;

namespace RuinsApi.Services.DataAccess
{
	public interface IObeliskRepository : IBaseDataRepository<Obelisk>
	{
		Task<List<Obelisk>> Search(int ruintypeId, int obeliskgroupId);
	}
}
