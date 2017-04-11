using System.Collections.Generic;
using CanonnApi.Web.DatabaseModels;

namespace CanonnApi.Web.Services.RuinSites
{
	public class RuinSiteWithObeliskData : RuinSite
	{
		public List<ObeliskWithActiveState> Obelisks { get; set; }
		public List<ObeliskGroupWithActiveState> ObeliskGroups { get; set; }

		public string SelectedSystem { get; set; }
		public string SelectedBody { get; set; }

		public RuinSiteWithObeliskData()
		{
			Obelisks = new List<ObeliskWithActiveState>();
			ObeliskGroups = new List<ObeliskGroupWithActiveState>();
		}

		public RuinSiteWithObeliskData(RuinSite site)
			: this()
		{
			Id = site.Id;
			BodyId = site.BodyId;
			Latitude = site.Latitude;
			Longitude = site.Longitude;
			RuintypeId = site.RuintypeId;
			Created = site.Created;
			Updated = site.Updated;

			Body = site.Body;
			Ruintype = site.Ruintype;

			SelectedBody = Body?.Name;
			SelectedSystem = Body?.System?.Name;
		}
	}
}
