using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using CanonnApi.Web.Controllers.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CanonnApi.Web.DatabaseModels;
using CanonnApi.Web.Middlewares;
using CanonnApi.Web.Services.RuinSites;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CanonnApi.Web.Controllers
{
	/// <summary>
	/// Provides methods for CRUD operations on <see cref="RuinSite"/> objects
	/// and their relations to obelisk groups and obelisks
	/// </summary>
	[Route("v1/ruinsites")]
	public class RuinSiteController : Controller
	{
		private readonly ILogger _logger;
		private readonly IRuinSiteRepository _repository;
		private readonly IMapper _mapper;

		/// <summary>
		/// Initializes a new instance of the <see cref="ArtifactsController"/> class
		/// </summary>
		/// <param name="logger">An instance of an <see cref="ILogger"/> to log.</param>
		/// <param name="repository">An instance of an <see cref="IRuinSiteRepository"/> to handle database access.</param>
		/// <param name="mapper">An instance of an <see cref="IMapper"/> to automatically map from and to DTOs.</param>
		public RuinSiteController(ILogger<RuinSiteController> logger, IRuinSiteRepository repository, IMapper mapper)
		{
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
			_repository = repository ?? throw new ArgumentNullException(nameof(repository));
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		}

		/// <summary>
		/// Gets an array of ruin site instances from the system
		/// </summary>
		/// <returns>A list of all available ruin sites</returns>
		[HttpGet()]
		[SwaggerResponse(200, Type = typeof(RuinSiteDto))] // TODO: When SwaggerUI is updated to 3.x make this return the array again.
		[SwaggerResponse(404, Description = "Not found")]
		public virtual async Task<List<RuinSiteDto>> Get()
		{
			var result = await _repository.GetAll();
			return _mapper.Map<List<RuinSite>, List<RuinSiteDto>>(result);
		}

		/// <summary>
		/// Gets a single ruin site based on its id
		/// </summary>
		/// <param name="id">The id of the ruin site to select</param>
		/// <returns>A representation of the selected ruin site</returns>
		[HttpGet("{id}")]
		[SwaggerResponse(200, Type = typeof(RuinSiteDto))]
		[SwaggerResponse(404, Description = "Not found")]
		public virtual async Task<RuinSiteDto> Get(int id)
		{
			var entry = await _repository.GetById(id);
			if (entry == null)
				throw new HttpNotFoundException();

			return _mapper.Map<RuinSite, RuinSiteDto>(entry);
		}

		/// <summary>
		/// Creates a new entry for a ruin site
		/// </summary>
		/// <param name="data">A representation of the ruin site to store</param>
		/// <returns>A representation of the freshly created ruin site</returns>
		[HttpPost()]
		[Authorize(Policy = "add:ruinsitedata")]
		[SwaggerResponse(200, Type = typeof(RuinSiteDto))]
		public async Task<RuinSiteDto> Create([FromBody] RuinSiteDto data)
		{
			var value = _mapper.Map<RuinSiteDto, RuinSite>(data);
			var result = await _repository.Create(value);

			return _mapper.Map<RuinSite, RuinSiteDto>(result);
		}

		/// <summary>
		/// Creates or updates an ruin site with a given id
		/// </summary>
		/// <param name="data">A representation of the ruin site to create or update</param>
		/// <param name="id">The id of the ruin site to update or 0 to create a new one</param>
		/// <returns>A representation of the created or updated ruin site</returns>
		[HttpPut("{id}")]
		[Authorize(Policy = "add:ruinsitedata")]
		[Authorize(Policy = "edit:ruinsitedata")]
		[SwaggerResponse(200, Type = typeof(RuinSiteDto))]
		[SwaggerResponse(409, Description = "Conflict")]
		public async Task<RuinSiteDto> CreateOrUpdate([FromBody] RuinSiteDto data, int id)
		{
			try
			{
				var value = _mapper.Map<RuinSiteDto, RuinSite>(data);
				var result = await _repository.CreateOrUpdateById(id, value);

				return _mapper.Map<RuinSite, RuinSiteDto>(result);
			}
			catch (Exception e)
			{
				throw new HttpException(HttpStatusCode.Conflict, "Conflict", e);
			}
		}

		/// <summary>
		/// Edits an existing ruin site entry for a given id
		/// </summary>
		/// <param name="data">A representation of the ruin site to update</param>
		/// <param name="id">The id of the ruin site to update</param>
		/// <returns>A representation of the created or updated ruin site</returns>
		[HttpPatch("{id}")]
		[Authorize(Policy = "edit:ruinsitedata")]
		[SwaggerResponse(200, Type = typeof(RuinSiteDto))]
		[SwaggerResponse(404, Description = "Not found")]
		[SwaggerResponse(409, Description = "Conflict")]
		public async Task<RuinSiteDto> Update([FromBody] RuinSiteDto data, int id)
		{
			try
			{
				var value = _mapper.Map<RuinSiteDto, RuinSite>(data);
				var result = await _repository.Update(id, value);

				return _mapper.Map<RuinSite, RuinSiteDto>(result);
			}
			catch (Exception e)
			{
				throw new HttpException(HttpStatusCode.Conflict, "Conflict", e);
			}
		}

		/// <summary>
		/// Deletes an existing ruin site based on its id
		/// </summary>
		/// <param name="id">The id of the ruin site to delete</param>
		/// <returns>200 if deletion was successful; 404 otherwise</returns>
		[HttpDelete("{id}")]
		[Authorize(Policy = "delete:ruinsitedata")]
		[SwaggerResponse(200, Description = "OK")]
		[SwaggerResponse(404, Description = "Not found")]
		public async Task<ActionResult> Delete(int id)
		{
			return (await _repository.DeleteById(id))
				? (ActionResult)Ok()
				: NotFound();
		}

		//----------------------------------------------------------------------------------------------------

		/// <summary>
		/// Gets a single ruin site with additional information required for the site editor
		/// </summary>
		/// <param name="id">The id of the ruin site to select</param>
		/// <returns>A representation of the requested ruin site</returns>
		[HttpGet("edit/{id}")]
		public virtual async Task<RuinSiteWithObeliskDataDto> GetForEditor(int id)
		{
			var result = await _repository.GetForSiteEditor(id);

			if (result == null)
				throw new HttpNotFoundException();

			return _mapper.Map<RuinSiteWithObeliskData, RuinSiteWithObeliskDataDto>(result);
		}

		/// <summary>
		/// Saves the details for a specific ruin site
		/// </summary>
		/// <param name="data">A representation of the ruin site to save</param>
		/// <returns>A representation of the created or updated ruin site</returns>
		[HttpPost("edit/{id}")]
		[HttpPut("edit/{id}")]
		[Authorize(Policy = "add:ruinsitedata")]
		[Authorize(Policy = "edit:ruinsitedata")]
		public virtual async Task<RuinSiteWithObeliskDataDto> SaveFromEditor([FromBody] RuinSiteWithObeliskData data)
		{
			var result = await _repository.SaveFromEditor(data);
			return _mapper.Map<RuinSiteWithObeliskData, RuinSiteWithObeliskDataDto>(result);
		}

		//----------------------------------------------------------------------------------------------------

		/// <summary>
		/// Gets the obelisk groups that are active on a specific ruin site defined by the site id
		/// </summary>
		/// <param name="id">The id of the ruin site to get the obelisk groups for</param>
		/// <returns>A list of representations of obelisk groups for the given ruin site</returns>
		[HttpGet("{id}/obeliskgroups")]
		[SwaggerResponse(200, Type = typeof(ObeliskGroupWithActiveStateDto))]  // TODO: When SwaggerUI is updated to 3.x make this return the array again.
		[SwaggerResponse(404, Description = "Not found")]
		public async Task<List<ObeliskGroupWithActiveStateDto>> GetObeliskGroups(int id)
		{
			var result = await _repository.LoadActiveObeliskGroupsForSite(id);
			if (result.Count == 0)
				throw new HttpNotFoundException();

			return _mapper.Map<List<ObeliskGroupWithActiveState>, List<ObeliskGroupWithActiveStateDto>>(result);
		}

		/// <summary>
		/// Saves what obelisk groups are active on a ruin site
		/// </summary>
		/// <param name="id">The id of the site to save the active obelisk groups for</param>
		/// <param name="obeliskGroups">A list of obelisk groups that should be marked as active on the site (all not listed here will be set inactive)</param>
		/// <returns>200 if saving was successful; 404 otherwise</returns>
		[HttpPut("{id}/obeliskgroups")]
		[HttpPatch("{id}/obeliskgroups")]
		[Authorize(Policy = "edit:ruinsitedata")]
		[SwaggerResponse(200, Description = "OK")]
		[SwaggerResponse(404, Description = "Not found")]
		public async Task<ActionResult> SaveObeliskGroupsForSite(int id, [FromBody] ObeliskGroupDto[] obeliskGroups)
		{
			var values = _mapper.Map<ObeliskGroupDto[], ObeliskGroup[]>(obeliskGroups);

			return (await _repository.SaveObeliskGroupsForSite(id, values))
				? (ActionResult)Ok()
				: NotFound();
		}

		/// <summary>
		/// Gets the obelisks that are active on a specific ruin site defined by the site id
		/// </summary>
		/// <param name="id">The id of the ruin site to get the obelisks for</param>
		/// <returns>A list of representations of obelisks for the given ruin site</returns>
		[HttpGet("{id}/activeobelisks")]
		[SwaggerResponse(200, Type = typeof(ObeliskDto))]  // TODO: When SwaggerUI is updated to 3.x make this return the array again.
		[SwaggerResponse(404, Description = "Not found")]
		public async Task<List<ObeliskDto>> GetActiveObelisks(int id)
		{
			var result = await _repository.LoadActiveObelisksForSite(id);
			if (result.Count == 0)
				throw new HttpNotFoundException();

			return _mapper.Map<List<Obelisk>, List<ObeliskDto>>(result);
		}

		/// <summary>
		/// Saves what obelisks are active on a ruin site
		/// </summary>
		/// <param name="id">The id of the site to save the active obelisks for</param>
		/// <param name="obelisks">A list of obelisks that should be marked as active on the site (all not listed here will be set inactive)</param>
		/// <returns>200 if saving was successful; 404 otherwise</returns>
		[HttpPut("{id}/activeobelisks")]
		[HttpPatch("{id}/activeobelisks")]
		[Authorize(Policy = "edit:ruinsitedata")]
		[SwaggerResponse(200, Description = "OK")]
		[SwaggerResponse(404, Description = "Not found")]
		public async Task<ActionResult> SaveActiveObelisksForSite(int id, [FromBody] Obelisk[] obelisks)
		{
			return (await _repository.SaveActiveObelisksForSite(id, obelisks))
				? (ActionResult)Ok()
				: NotFound();
		}

		/// <summary>
		/// Searches for a specific data entry and returns all sites that have an available and active obelisk yielding this data entry
		/// </summary>
		/// <param name="categoryName">The name of the data category to search for</param>
		/// <param name="entryNumber">The number of the data entry to search for</param>
		/// <returns>A list of found ruin sites</returns>
		[HttpGet("searchdata/{categoryName}/{entryNumber}")]
		[SwaggerResponse(200, Type = typeof(RuinSiteDto))]  // TODO: When SwaggerUI is updated to 3.x make this return the array again.
		[SwaggerResponse(404, Description = "Not found")]
		public async Task<List<RuinSiteDto>> SearchForDataEntries(string categoryName, int entryNumber)
		{
			var result = await _repository.SearchSitesForData(categoryName, entryNumber);
			if (result.Count == 0)
				throw new HttpNotFoundException();

			return _mapper.Map<List<RuinSite>, List<RuinSiteDto>>(result);
		}

		/// <summary>
		/// Searches for specific data entries and returns all sites that have an available and active obelisk yielding one of the data entries
		/// </summary>
		/// <param name="searchData">A structure defining the data entries to search for</param>
		/// <returns>A structure of located ruin sites for each searched combination</returns>
		[HttpPost("searchdata")]
		[SwaggerResponse(200, Type = typeof(RuinSiteDto))]  // TODO: When SwaggerUI is updated to 3.x make this return the array again.
		[SwaggerResponse(404, Description = "Not found")]
		public async Task<Dictionary<string, Dictionary<int, List<RuinSiteDto>>>> SearchForDataEntries([FromBody] Dictionary<string, int[]> searchData)
		{
			var result = new Dictionary<string, Dictionary<int, List<RuinSiteDto>>>();
			var merger = new List<List<RuinSiteDto>>();

			foreach (var categoryName in searchData.Keys)
			{
				result.Add(categoryName, new Dictionary<int, List<RuinSiteDto>>());
				foreach (var entryNumber in searchData[categoryName])
				{
					var currentResult = await _repository.SearchSitesForData(categoryName, entryNumber);
					var tempList = _mapper.Map<List<RuinSite>, List<RuinSiteDto>>(currentResult);

					merger.Add(tempList);

					if (tempList.Any())
					{
						result[categoryName].Add(entryNumber, tempList);
					}
				}
			}

			IEnumerable<RuinSiteDto> merged = null;
			foreach (var list in merger)
			{
				merged = (merged == null)
					? list
					: merged.Intersect(list, new System.EqualityComparer<RuinSiteDto>((a, b) => a.Id == b.Id));
			}

			result.Add("COMBINED", new Dictionary<int, List<RuinSiteDto>>() {{ 0, merged.ToList() }});

			return result;
		}
	}
}
