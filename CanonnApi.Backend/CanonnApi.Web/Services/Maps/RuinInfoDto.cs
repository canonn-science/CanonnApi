using System.Collections.Generic;

namespace CanonnApi.Web.Services.Maps
{
	public class RuinInfoDto
	{
		public int RuinId { get; set; }
		public string BodyName { get; set; }
		public string RuinTypeName { get; set; }
		public decimal[] Coordinates { get; set; } = new decimal[0];
		public Dictionary<string, Dictionary<string, int>> Obelisks { get; set; } = new Dictionary<string, Dictionary<string, int>>();
		public int BodyId { get; set; }
		public int? BodyDistance { get; set; }
		public int SystemId { get; set; }
		public string SystemName { get; set; }
		public float[] SystemCoordinates { get; set; }
	}
}
