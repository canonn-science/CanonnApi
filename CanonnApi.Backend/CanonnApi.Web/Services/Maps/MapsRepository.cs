﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using CanonnApi.Web.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CanonnApi.Web.Services.Maps
{
	public class MapsRepository : IMapsRepository
	{
		private readonly IConfiguration _configuration;
		private readonly IMapper _mapper;
		private readonly RuinsContext _ruinsContext;

		public MapsRepository(IConfiguration configuration, IMapper mapper, RuinsContext ruinsContext)
		{
			_configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
			_ruinsContext = ruinsContext ?? throw new ArgumentNullException(nameof(ruinsContext));
		}

		public async Task<List<MapsSystem>> LoadSitesOverview()
		{
			var sitesGraph = await _ruinsContext.RuinSite.Include(rs => rs.Body.System).Include(rs => rs.Ruintype).ToListAsync();

			// build resulting structure
			Dictionary<int, MapsSystem> systems = new Dictionary<int, MapsSystem>();

			foreach (var site in sitesGraph)
			{
				MapsSystem mapsSystem;

				if (!systems.TryGetValue(site.Body.System.Id, out mapsSystem))
				{
					mapsSystem = _mapper.Map<DatabaseModels.System, MapsSystem>(site.Body.System);
					systems.Add(mapsSystem.SystemId, mapsSystem);
				}

				mapsSystem.Ruins.Add(_mapper.Map<RuinSite, MapsRuins>(site));
			}

			var result = systems.Values.OrderBy(s => s.SystemName);
			foreach (var sys in result)
			{
				sys.Ruins.Sort((a, b) => a.BodyName.CompareTo(b.BodyName));
			}

			return result.ToList();
		}

		public async Task<object> LoadScanData()
		{
			var dataGraph = await _ruinsContext.Obelisk
				.Include(o => o.Codexdata.Artifact)
				.Include(o => o.Codexdata.Category.Artifact)
				.Include(o => o.Obeliskgroup.Ruintype)
				.Where(o => o.IsBroken || o.Codexdata != null)
				.OrderBy(o => o.Obeliskgroup.Ruintype.Name)
					.ThenBy(o => o.Obeliskgroup.Name)
					.ThenBy(o => o.Number)
				.ToListAsync();

			var projectedData = dataGraph.Select(o => new
				{
					Number = o.Number.ToString(),
					PrimaryArtifact = o.Codexdata?.Category.Artifact.Name,
					SecondaryArtifact = o.Codexdata?.Artifact?.Name,
					CategoryName = o.Codexdata?.Category.Name,
					CodexDataNumber = o.Codexdata?.EntryNumber.ToString(),
					ObeliskGroup = o.Obeliskgroup.Name.ToLowerInvariant(),
					RuinType = o.Obeliskgroup.Ruintype.Name.ToLowerInvariant(),
					IsVerified = o.IsVerified,
					IsBroken = o.IsBroken,
				});

			// build resulting structure
			var result = new Dictionary<string, Dictionary<string, Dictionary<string, ScanDataDto>>>();

			foreach (var scanData in projectedData)
			{
				Dictionary<string, Dictionary<string, ScanDataDto>> obeliskGroupLevel;
				Dictionary<string, ScanDataDto> obeliskLevel;

				if (!result.TryGetValue(scanData.RuinType, out obeliskGroupLevel))
				{
					obeliskGroupLevel = new Dictionary<string, Dictionary<string, ScanDataDto>>();
					result.Add(scanData.RuinType, obeliskGroupLevel);
				}

				if (!obeliskGroupLevel.TryGetValue(scanData.ObeliskGroup, out obeliskLevel))
				{
					obeliskLevel = new Dictionary<string, ScanDataDto>();
					obeliskGroupLevel.Add(scanData.ObeliskGroup, obeliskLevel);
				}

				var data = new ScanDataDto()
				{
					Scan = !String.IsNullOrWhiteSpace(scanData.CodexDataNumber) ? $"{scanData.CategoryName} {scanData.CodexDataNumber}" : null,
					IsVerified = scanData.IsVerified,
					IsBroken = scanData.IsBroken,
				};

				if (scanData.PrimaryArtifact != null)
				{
					data.Items.Add(scanData.PrimaryArtifact);
				}

				if (scanData.SecondaryArtifact != null)
				{
					data.Items.Add(scanData.SecondaryArtifact);
				}

				obeliskLevel.Add(scanData.Number, data);
			}

			return result;
		}

		public async Task<object> LoadRuinInfo(int id)
		{
			var ruinTask = LoadRuinSiteById(id);
			var obeliskGroupTask = LoadObeliskGroupsForSiteId(id);
			var activeObelisksTask = LoadActiveObelisksForSite(id);
			await Task.WhenAll(new Task[] { ruinTask, obeliskGroupTask, activeObelisksTask });

			var ruin = await ruinTask;
			var obeliskGroups = await obeliskGroupTask;
			var activeObelisks = await activeObelisksTask;

			// TODO: Move to mapping configuration
			var result = new RuinInfoDto()
			{
				RuinId = ruin.Id,
				RuinTypeName = ruin.Ruintype.Name,
				BodyId = ruin.BodyId,
				BodyName = ruin.Body.Name,
				BodyDistance = ruin.Body.Distance,
				Coordinates = new decimal[] { ruin.Latitude, ruin.Longitude },
				SystemId = ruin.Body.SystemId,
				SystemName = ruin.Body.System.Name,
				SystemCoordinates = (ruin.Body.System?.EdsmCoordX != null)
					? new float[] { ruin.Body.System.EdsmCoordX.Value, ruin.Body.System.EdsmCoordY.Value, ruin.Body.System.EdsmCoordZ.Value }
					: new float[] {},
				EdsmSystemLink = (ruin.Body.System.EdsmExtId.HasValue && !String.IsNullOrWhiteSpace(ruin.Body.System.Name))
					? String.Format(_configuration.GetSection("externalLinks:edsmSystem").Value, ruin.Body.System.EdsmExtId, WebUtility.UrlEncode(ruin.Body.System.Name))
					: null,
				EdsmBodyLink = (ruin.Body.System.EdsmExtId.HasValue && !String.IsNullOrWhiteSpace(ruin.Body.System.Name) && ruin.Body.EdsmExtId.HasValue && !String.IsNullOrWhiteSpace(ruin.Body.Name))
					? String.Format(_configuration.GetSection("externalLinks:edsmBody").Value, ruin.Body.System.EdsmExtId, WebUtility.UrlEncode(ruin.Body.System.Name), ruin.Body.EdsmExtId, WebUtility.UrlEncode(ruin.Body.Name))
					: null,
				Obelisks = BuildObeliskData(obeliskGroups, activeObelisks),
			};

			return result;
		}

		private Task<RuinSite> LoadRuinSiteById(int id)
		{
			return _ruinsContext.RuinSite
				.Include(r => r.Body.System)
				.Include(r => r.Ruintype)
				.SingleAsync(r => r.Id == id);
		}

		private Task<List<ObeliskGroup>> LoadObeliskGroupsForSiteId(int id)
		{
			return _ruinsContext.RuinsiteObeliskgroups
				.Where(rsog => rsog.RuinsiteId == id)
				.Select(rsog => rsog.Obeliskgroup)
				.OrderBy(og => og.Name)
				.ToListAsync();
		}

		private async Task<Dictionary<string, HashSet<int>>> LoadActiveObelisksForSite(int id)
		{
			var activeObelisks = await _ruinsContext.RuinsiteActiveobelisks
				.Where(rsao => rsao.RuinsiteId == id)
				.Select(rsao => new
				{
					ObeliskGroup = rsao.Obelisk.Obeliskgroup.Name,
					ObeliskNumber = rsao.Obelisk.Number,
				})
				.ToListAsync();

			var result = new Dictionary<string, HashSet<int>>();

			foreach (var activeObelisk in activeObelisks)
			{
				HashSet<int> actives;
				if (!result.TryGetValue(activeObelisk.ObeliskGroup, out actives))
				{
					actives = new HashSet<int>();
					result.Add(activeObelisk.ObeliskGroup, actives);
				}

				actives.Add(activeObelisk.ObeliskNumber);
			}

			return result;
		}

		private Dictionary<string, Dictionary<string, int>> BuildObeliskData(List<ObeliskGroup> obeliskGroups, Dictionary<string, HashSet<int>> activeObeliskData)
		{
			var result = new Dictionary<string, Dictionary<string, int>>();

			foreach (var obeliskGroup in obeliskGroups.OrderBy(og => og.Name))
			{
				var inner = new Dictionary<string, int>();
				result.Add(obeliskGroup.Name.ToLowerInvariant(), inner);

				HashSet<int> activeObelisks = null;
				activeObeliskData.TryGetValue(obeliskGroup.Name, out activeObelisks);

				for (var i = 1; i <= obeliskGroup.Count; i++)
				{
					inner.Add(i.ToString(), ((activeObelisks != null) && activeObelisks.Contains(i)) ? 1 : 0);
				}
			}

			return result;
		}
	}
}
