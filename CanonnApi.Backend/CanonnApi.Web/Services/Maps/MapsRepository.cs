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

		public async Task<object> LoadScanData()
		{
			var dataGraph = await _ruinsContext.Obelisk
				.Include(o => o.Obeliskgroup.Ruintype)
				.Include(o => o.Artifact)
				.Include(o => o.Codexdata.Category.Artifact)
				.Where(o => o.CodexdataId != null)
				.OrderBy(o => o.Obeliskgroup.RuintypeId)
					.ThenBy(o => o.Obeliskgroup.Name)
					.ThenBy(o => o.Number)
				.ToListAsync();

			// build resulting structure
			var result = new Dictionary<string, Dictionary<string, Dictionary<string, ScanDataDto>>>();

			foreach (var scanData in dataGraph)
			{
				Dictionary<string, Dictionary<string, ScanDataDto>> obeliskGroupLevel;
				Dictionary<string, ScanDataDto> obeliskLevel;

				if (!result.TryGetValue(scanData.Obeliskgroup.Ruintype.Name.ToLowerInvariant(), out obeliskGroupLevel))
				{
					obeliskGroupLevel = new Dictionary<string, Dictionary<string, ScanDataDto>>();
					result.Add(scanData.Obeliskgroup.Ruintype.Name.ToLowerInvariant(), obeliskGroupLevel);
				}

				if (!obeliskGroupLevel.TryGetValue(scanData.Obeliskgroup.Name.ToLowerInvariant(), out obeliskLevel))
				{
					obeliskLevel = new Dictionary<string, ScanDataDto>();
					obeliskGroupLevel.Add(scanData.Obeliskgroup.Name.ToLowerInvariant(), obeliskLevel);
				}

				var data = new ScanDataDto()
				{
					Scan = $"{scanData.Codexdata.Category.Name} {scanData.Codexdata.EntryNumber}",
				};

				data.Items.Add(scanData.Codexdata.Category.Artifact.Name);
				if (scanData.Artifact != null)
				{
					data.Items.Add(scanData.Artifact.Name);
				}

				obeliskLevel.Add(scanData.Number.ToString(), data);
			}

			return result;
		}
	}
}
