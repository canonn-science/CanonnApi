using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CanonnApi.Web.DatabaseModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using Microsoft.Extensions.Logging;

namespace CanonnApi.Web.Controllers
{
	/// <summary>
	/// Provides data for a galactic map representation
	/// </summary>
	[Route("v1/galmaps")]
	[Produces("application/json")]
	public class GalacticMapController : Controller
	{
		private readonly ILogger<GalacticMapController> _logger;
		private readonly IGalacticMapRepository _repository;
		private readonly IMapper _mapper;

		/// <summary>
		/// Initializes a new instance of the <see cref="GalacticMapController"/>
		/// </summary>
		/// <param name="logger">An instance of an <see cref="ILogger"/></param>
		/// <param name="repository">An instance of an <see cref="IGalacticMapRepository"/> for data storage access</param>
		/// <param name="mapper">An instance of an <see cref="IMapper"/></param>
		public GalacticMapController(ILogger<GalacticMapController> logger, IGalacticMapRepository repository, IMapper mapper)
		{
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
			_repository = repository ?? throw new ArgumentNullException(nameof(repository));
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		}

		/// <summary>
		/// Returns systems and ruins in them in a format required for the galactic 3D map tool
		/// </summary>
		/// <returns></returns>
		[HttpGet()]
		[Route("ruins")]
		public async Task<GalacticMapDto> GetRuinsMap()
		{
			var dataTask = _repository.LoadMapCategories();
			var systemsTask = _repository.LoadSystems();

			var result = new GalacticMapDto();
			result.Systems = (await systemsTask).ToArray();
			result.Categories = await dataTask;

			return result;
		}
	}



	public class GalacticMapDto
	{
		public Dictionary<string, Dictionary<string, GalacticMapCategoryDto>> Categories { get; set; }
		public GalacticMapSystemDto[] Systems { get; set; }
	}

	public class GalacticMapCategoryDto
	{
		public string Name { get; set; }
		public string Color { get; set; }
	}

	public class GalacticMapSystemDto
	{
		public string Name { get; set; }
		public GalacticMapCoordinatesDto Coords { get; set; }
		public string[] Cat { get; set; }
		public string Infos { get; set; }
		public string Url { get; set; }
	}


	public class GalacticMapCoordinatesDto
	{
		public string X { get; set; }
		public string Y { get; set; }
		public string Z { get; set; }
	}

	public interface IGalacticMapRepository
	{
		Task<List<GalacticMapSystemDto>> LoadSystems();
		Task<Dictionary<string, Dictionary<string, GalacticMapCategoryDto>>> LoadMapCategories();
	}

	/// <summary>
	/// Manages data storage access for the galactic 3D map endpoints
	/// </summary>
	public class GalacticMapRepository : IGalacticMapRepository
	{
		private const int RuintypeOffset = 100;
		private const int CodexDataOffset = 1000;

		private readonly RuinsContext _context;
		private readonly ILogger<GalacticMapRepository> _logger;

		/// <summary>
		/// Initalizes a new instance of the <see cref="GalacticMapRepository"/>
		/// </summary>
		/// <param name="logger">An instance of an <see cref="ILogger"/></param>
		/// <param name="context">An instance of the <see cref="RuinsContext"/> for database access</param>
		public GalacticMapRepository(ILogger<GalacticMapRepository> logger, RuinsContext context)
		{
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
			_context = context ?? throw new ArgumentNullException(nameof(context));
		}

		public async Task<List<GalacticMapSystemDto>> LoadSystems()
		{
			var systems = await _context.System
				.Include(s => s.Body)
				.ThenInclude(b => b.RuinSite)
				.ThenInclude(rs => rs.RuinsiteActiveobelisks)
				.ThenInclude(rsao => rsao.Obelisk)
				.ThenInclude(o => o.Codexdata)
				.ThenInclude(o => o.Artifact)
				.AsNoTracking()
				.ToListAsync();

			var result = new List<GalacticMapSystemDto>();

			foreach (var system in systems)
			{
				if (!system.EdsmCoordUpdated.HasValue)
					continue;

				var categories = new List<string>();
				var infoString = new StringBuilder();

				var galSys = new GalacticMapSystemDto()
				{
					Name = system.Name,
				};

				infoString.Append($"<h1>System: {system.Name}</h1>");

				foreach (var body in system.Body)
				{
					infoString.Append($"<h2>Body: {body.Name}</h2><ul>");
					
					foreach (var ruin in body.RuinSite)
					{
						infoString.Append($"<li><a href=\"https://ruins.canonn.technology/#GS{ruin.Id}\" target=\"new\">GS{ruin.Id}</a>");

						categories.Add((RuintypeOffset + ruin.RuintypeId).ToString());
						categories.AddRange(ruin.RuinsiteActiveobelisks.Select(ao => ao.Obelisk)
							.Select(o => (CodexDataOffset + o.CodexdataId).ToString()));
					}

					infoString.Append($"</ul>");
				}

				galSys.Cat = categories.Where(c => !String.IsNullOrWhiteSpace(c)).Distinct().OrderBy(c => c).ToArray();

				galSys.Coords = new GalacticMapCoordinatesDto()
				{
					X = system.EdsmCoordX.Value.ToString(CultureInfo.InvariantCulture),
					Y = system.EdsmCoordY.Value.ToString(CultureInfo.InvariantCulture),
					Z = system.EdsmCoordZ.Value.ToString(CultureInfo.InvariantCulture),
				};

				galSys.Infos = infoString.ToString();

				result.Add(galSys);
			}

			return result;
/*
			return Task.Run(() =>
			{
				return new List<GalacticMapSystemDto>()
				{
					new GalacticMapSystemDto()
					{
						Name = "SYNUEFE XR-H D11-102",
						Coords = new GalacticMapCoordinatesDto() { X = "357.344", Y = "-49.3438", Z = "-74.75" },
						Cat = new [] { "1", "1001" },
						Infos = @"<p>Bodies:</p>
<ul>
	<li>1 B<br/>
		<p>Ruins:</p>
		<ul>
			<li><a href=""https://ruins.canonn.technology/#GS1"" target=""new"">GS1</a> at -31.7347, -128.92912<br />Type: BETA<br/> Data:<br/><ul><li>H9</li> <li>L10</li> <li> L12 </li> <li>H8 </li> <li>H9</li> <li>H10</li></ul> </li>
			<li><a href=""https://ruins.canonn.technology/#GS2"" target=""new"">GS2</a> at -29.1664, -30.5041<br />Type: GAMMA<br/> Data:<br/><ul><li>H9</li> <li>L10</li> <li> L12 </li> <li>H8 </li> <li>H9</li> <li>H10</li></ul> </li>
		</ul>
	</li>
	<li>5 B1<br/>
		<p>Ruins:</p>
		<ul>
			<li><a href=""https://ruins.canonn.technology/#GS1"" target=""new"">GS1</a> at -31.7347, -128.92912<br />Type: BETA<br/> Data:<br/><ul><li>H9</li> <li>L10</li> <li> L12 </li> <li>H8 </li> <li>H9</li> <li>H10</li></ul> </li>
			<li><a href=""https://ruins.canonn.technology/#GS2"" target=""new"">GS2</a> at -29.1664, -30.5041<br />Type: GAMMA<br/> Data:<br/><ul><li>H9</li> <li>L10</li> <li> L12 </li> <li>H8 </li> <li>H9</li> <li>H10</li></ul> </li>
		</ul>
	</li>
</ul>"
					},
					new GalacticMapSystemDto()
					{
						Name = "SKAUDAI AM-B D14-138",
						Coords = new GalacticMapCoordinatesDto() { X = "-5477.59", Y = "-504.156", Z = "10436.2" },
						Cat = new [] { "2", "3", "1001, 1002" },
						Infos = "<p>Ruins:</p><ul><li>GS1</li><li>GS2</li></ul>"
					},
				};
			});
			*/
		}

		public async Task<Dictionary<string, Dictionary<string, GalacticMapCategoryDto>>> LoadMapCategories()
		{
			var result = new Dictionary<string, Dictionary<string, GalacticMapCategoryDto>>();

			var categories = await _context.RuinType
				.OrderBy(rt => rt.Name)
				.AsNoTracking()
				.ToListAsync();

			result.Add(
				"Ruin types",
				categories.ToDictionary(rt => (RuintypeOffset + rt.Id).ToString(),
					rt => new GalacticMapCategoryDto() { Name = rt.Name, Color = RuinTypeColors[rt.Id - 1] })
			);

			var codexEntries = await _context.CodexData
				.Include(cd => cd.Artifact)
				.Include(cd => cd.Category.Artifact)
				.OrderBy(cd => cd.Category.Name)
				.ThenBy(cd => cd.EntryNumber)
				.AsNoTracking()
				.ToListAsync();

			string SecondaryArtifactName(CodexData cd) => (cd.Artifact != null) ? " + " + cd.Artifact.Name : String.Empty;

			result.Add(
				"Codex entries",
				codexEntries.ToDictionary(ce => (CodexDataOffset + ce.Id).ToString(),
					ce => new GalacticMapCategoryDto() { Name = $"{ce.Category.Name} {ce.EntryNumber} ({ce.Category.Artifact.Name}{SecondaryArtifactName(ce)})", Color = DataEntryColors[ce.Id - 1] })
			);

			return result;
		}

		// TODO: This needs a MUCH better solution

		// generated from http://tools.medialab.sciences-po.fr/iwanthue/
		private static readonly string[] RuinTypeColors = new[]
		{
			"ff4c1e",
			"47dc1d",
			"f36aff",
		};

			// generated from http://tools.medialab.sciences-po.fr/iwanthue/
		private static readonly string[] DataEntryColors = new[]
		{
			"cf006a",
			"e70068",
			"b0004d",
			"7d0034",
			"ff4c81",
			"52021f",
			"ff9ba5",
			"441719",
			"aa0025",
			"ff847d",
			"bd8d87",
			"ff6b61",
			"6a0007",
			"ef000c",
			"af1800",
			"ff7550",
			"ffb298",
			"ff602f",
			"efbaa7",
			"872d00",
			"fe6200",
			"ffb081",
			"e76e00",
			"522600",
			"8f4800",
			"f28600",
			"ffb26f",
			"9a6100",
			"fea500",
			"ffb542",
			"cd9400",
			"f0bf3c",
			"e4c368",
			"e3c300",
			"544900",
			"3a3300",
			"cdc500",
			"cdc7a0",
			"cfcb4e",
			"6e8500",
			"a1c200",
			"b5d07b",
			"6eb600",
			"387200",
			"83dc48",
			"1e5b00",
			"0fa000",
			"053500",
			"009020",
			"01cc3e",
			"006e20",
			"62de7c",
			"00dd61",
			"019d54",
			"00be69",
			"005d39",
			"01a777",
			"019c83",
			"00463c",
			"52dbc3",
			"018b88",
			"007988",
			"68d3f7",
			"21c8ff",
			"01749a",
			"7fc9ff",
			"018ed5",
			"003c64",
			"014d94",
			"90afff",
			"0276e3",
			"2a7dff",
			"0260f8",
			"012f97",
			"001f6e",
			"9c8cff",
			"6b4cf9",
			"c8a9ff",
			"3e00a9",
			"2f1e44",
			"3c0363",
			"6c00ae",
			"51007e",
			"d47fff",
			"dc71ff",
			"f695ff",
			"f9abff",
			"b100ab",
			"f8aeed",
			"930089",
			"f828e0",
			"e3bad7",
			"ff5ad0",
			"f300b9",
			"68004b",
			"900065",
			"593447",
			"ff35ac",
			"8c6171",
			"ff70ad",
			"ffa5c3",
		};
	}
}
