using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Swashbuckle.AspNetCore.SwaggerGen;
using CanonnApi.Web.Controllers.Models;
using CanonnApi.Web.Services.Maps;

namespace CanonnApi.Web.Controllers
{
	/// <summary>
	/// Provides endpoints for the interactive ruins map
	/// </summary>
	[Route("v1/maps")]
	[Produces("application/json")]
	public class MapsController : Controller
	{
		private readonly ILogger<MapsController> _logger;
		private readonly IMapsRepository _repository;
		private readonly IMapper _mapper;

		/// <summary>
		/// Initializes a new instance of the <see cref="MapsController"/>
		/// </summary>
		/// <param name="logger">An instance of an <see cref="ILogger"/></param>
		/// <param name="repository">An instance of an <see cref="IMapsRepository"/> for data storage access</param>
		/// <param name="mapper">An instance of an <see cref="IMapper"/></param>
		public MapsController(ILogger<MapsController> logger, IMapsRepository repository, IMapper mapper)
		{
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
			_repository = repository ?? throw new ArgumentNullException(nameof(repository));
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		}

		/// <summary>
		/// Gets an list about all systems with ruins on them for the interactive ruin map
		/// </summary>
		/// <returns>A list of systems</returns>
		[HttpGet("systemoverview")]
		[SwaggerResponse(200, Type = typeof(MapsSystemDto))] // TODO: When SwaggerUI is updated to 3.x make this return the array again.
		public async Task<List<MapsSystemDto>> GetSystemsOverview()
		{
			var result = await _repository.LoadSitesOverview();
			return _mapper.Map<List<MapsSystem>, List<MapsSystemDto>>(result);
		}

		/// <summary>
		/// Returns the obelisks with their data or broken state in a special format required by the interactive ruin map
		/// </summary>
		/// <returns>A hierarchical structure of scan data</returns>
		[HttpGet("scandata")]
		[SwaggerResponse(200, Description = "OK")]
		public async Task<object> GetScanData()
		{
			return await _repository.LoadScanData();
		}

		/// <summary>
		/// Returns the info for a single ruin and all groups with their active obelisks
		/// </summary>
		/// <param name="id">The id of the ruin site to retrieve the info for</param>
		/// <returns>A hierarchical structure of all infos for the selected ruin</returns>
		[HttpGet("ruininfo/{id}")]
		[SwaggerResponse(200, Description = "OK")]
		[SwaggerResponse(404, Description = "Not found")]
		public async Task<object> GetRuinInfo(int id)
		{
			return await _repository.LoadRuinInfo(id);
		}
	}
}
