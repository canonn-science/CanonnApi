using System.Collections.Generic;
using System.Threading.Tasks;
using CanonnApi.Web.DatabaseModels;

namespace CanonnApi.Web.Services.DataAccess
{
	public interface IRuinSiteRepository: IBaseDataRepository<RuinSite>
	{
		Task<List<ObeliskGroup>> LoadObeliskGroupsForSite(int siteId);
		Task<bool> SaveObeliskGroupsForSite(int siteId, ObeliskGroup[] obeliskGroups);
		Task<List<Obelisk>> LoadActiveObelisksForSite(int siteId);
		Task<bool> SaveActiveObelisksForSite(int siteId, Obelisk[] obelisks);
	}
}
