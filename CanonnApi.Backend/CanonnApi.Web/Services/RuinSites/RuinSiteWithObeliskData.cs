using System.Collections.Generic;

namespace CanonnApi.Web.Services.RuinSites
{
	public class RuinSiteWithObeliskData : DatabaseModels.RuinSite
	{
		public IEnumerable<ObeliskWithActiveState> Obelisks { get; set; }
		public IEnumerable<ObeliskGroupWithActiveState> ObeliskGroups { get; set; }
	}
}
