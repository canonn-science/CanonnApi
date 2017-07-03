using System.Collections.Generic;

namespace CanonnApi.Web.Services.Maps
{
	public class MapsSystem
	{
		public int SystemId { get; set; }
		public string SystemName { get; set; }
		public int? EdsmExtId { get; set; }
		public List<MapsRuins> Ruins { get; set; } = new List<MapsRuins>();
	}
}
