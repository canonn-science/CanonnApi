namespace CanonnApi.Web.Services.Maps
{
	public class MapsRuins
	{
		public int RuinId { get; set; }
		public string BodyName { get; set; }
		public string RuinTypeName { get; set; }
		public decimal[] Coordinates { get; set; } = new decimal[2];
		public string EdsmBodyLink { get; set; }
	}
}
