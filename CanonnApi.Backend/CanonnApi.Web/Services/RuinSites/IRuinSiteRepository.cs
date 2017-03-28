using System.Collections.Generic;
using System.Threading.Tasks;
using CanonnApi.Web.DatabaseModels;
using CanonnApi.Web.Services.DataAccess;

namespace CanonnApi.Web.Services.RuinSites
{
	public interface IRuinSiteRepository: IBaseDataRepository<RuinSite>
	{
		Task<List<ObeliskGroupWithActiveState>> LoadActiveObeliskGroupsForSite(int siteId);
		Task<bool> SaveObeliskGroupsForSite(int siteId, ObeliskGroup[] obeliskGroups);
		Task<List<Obelisk>> LoadActiveObelisksForSite(int siteId);
		Task<bool> SaveActiveObelisksForSite(int siteId, Obelisk[] obelisks);
	}
}
