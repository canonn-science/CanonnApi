using System.Collections.Generic;
using CanonnApi.Web.DatabaseModels;

namespace CanonnApi.Web.Services.RuinSites
{
	public class RuinSiteWithObeliskData : RuinSite
	{
		// BACKWARDS COMPATIBILITY
		// these properties were removed from RuinSite with the POI refactoring, but we need them for the clients
		public int? BodyId { get; set; }
		public decimal? Latitude { get; set; }
		public decimal? Longitude { get; set; }
		public Body Body { get; set; }
		// BACKWARDS COMPATIBILITY

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
			BodyId = site.Location.BodyId;
			Latitude = site.Location.Latitude;
			Longitude = site.Location.Longitude;
			RuintypeId = site.RuintypeId;
			Created = site.Created;
			Updated = site.Updated;

			Body = site.Location.Body;
			Ruintype = site.Ruintype;

			SelectedBody = Body?.Name;
			SelectedSystem = Body?.System?.Name;
		}
	}
}
