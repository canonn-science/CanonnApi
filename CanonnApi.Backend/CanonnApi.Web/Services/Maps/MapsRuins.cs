using CanonnApi.Web.DatabaseModels;

namespace CanonnApi.Web.Services.Maps
{
	public class MapsRuins
	{
		public int RuinId { get; set; }
		public string BodyName { get; set; }
		public string RuinTypeName { get; set; }
		public decimal[] Coordinates { get; set; } = new decimal[2];

		public MapsRuins(RuinSite site)
		{
			RuinId = site.Id;
			BodyName = site.Body.Name;
			RuinTypeName = site.Ruintype.Name;
			Coordinates[0] = site.Latitude;
			Coordinates[1] = site.Longitude;
		}
	}
}