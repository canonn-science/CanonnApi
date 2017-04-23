using System.Collections.Generic;
using System.Threading.Tasks;
using CanonnApi.Web.DatabaseModels;
using CanonnApi.Web.Services.DataAccess;

namespace CanonnApi.Web.Services.RuinSites
{
	public interface IRuinSiteRepository: IBaseDataRepository<RuinSite>
	{
		Task<RuinSiteWithObeliskData> GetForSiteEditor(int siteId);
		Task<RuinSiteWithObeliskData> SaveFromEditor(RuinSiteWithObeliskData siteId);

		Task<List<ObeliskGroupWithActiveState>> LoadActiveObeliskGroupsForSite(int siteId);
		Task<List<Obelisk>> LoadActiveObelisksForSite(int siteId);

		Task<bool> SaveObeliskGroupsForSite(int siteId, IEnumerable<ObeliskGroup> obeliskGroups);
		Task<bool> SaveActiveObelisksForSite(int siteId, IEnumerable<Obelisk> obelisks);
		Task<List<RuinSite>> SearchSitesForData(string categoryName, int entryNumber);
	}
}
