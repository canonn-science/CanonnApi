using System.Collections.Generic;

namespace CanonnApi.Web.Services.Maps
{
	public class SystemDto
	{
		public int SystemId { get; set; }
		public string SystemName { get; set; }
		public List<RuinsDto> Ruins { get; set; } = new List<RuinsDto>();

		public SystemDto(DatabaseModels.System system)
		{
			SystemId = system.Id;
			SystemName = system.Name;
		}
	}
}