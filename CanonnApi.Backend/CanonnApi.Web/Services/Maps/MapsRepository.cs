using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CanonnApi.Web.DatabaseModels;
using Microsoft.EntityFrameworkCore;

namespace CanonnApi.Web.Services.Maps
{
	public class MapsRepository : IMapsRepository
	{
		private readonly RuinsContext _ruinsContext;

		public MapsRepository(RuinsContext ruinsContext)
		{
			_ruinsContext = ruinsContext ?? throw new ArgumentNullException(nameof(ruinsContext));
		}

		public async Task<IOrderedEnumerable<SystemDto>> LoadSitesOverview()
		{
			var sitesGraph = await _ruinsContext.RuinSite.Include(rs => rs.Body.System).Include(rs => rs.Ruintype).ToListAsync();

			// build resulting structure
			Dictionary<int, SystemDto> systems = new Dictionary<int, SystemDto>();

			foreach (var site in sitesGraph)
			{
				SystemDto system;

				if (!systems.TryGetValue(site.Body.System.Id, out system))
				{
					system = new SystemDto(site.Body.System);
					systems.Add(system.SystemId, system);
				}

				system.Ruins.Add(new RuinsDto(site));
			}

			var result = systems.Values.OrderBy(s => s.SystemName);
			foreach (var sys in result)
			{
				sys.Ruins.Sort((a, b) => a.BodyName.CompareTo(b.BodyName));
			}

			return result;
		}
	}
}
