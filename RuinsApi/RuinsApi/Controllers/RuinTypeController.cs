using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RuinsApi.DatabaseModels;
using RuinsApi.Middlewares;
using RuinsApi.Services.DataAccess;

namespace RuinsApi.Controllers
{
	[Route("v1/ruins/types")]
	public class RuinsController : Controller
	{
		private readonly ILogger _logger;
		private readonly IRuinRepository _repository;

		public RuinsController(ILogger<RelictsController> logger, IRuinRepository ruinRepository)
		{
			if (logger == null)
				throw new ArgumentNullException(nameof(logger));
			if (ruinRepository == null)
				throw new ArgumentNullException(nameof(ruinRepository));

			_logger = logger;
			_repository = ruinRepository;
		}

		[HttpGet]
		public async Task<List<RuinType>> Get()
		{
			return await _repository.GetAllRuinTypes();
		}

		[HttpGet("{id}")]
		public async Task<RuinType> Get(int id)
		{
			var relict = await _repository.GetRuinTypeById(id);
			if (relict == null)
				throw new HttpNotFoundException();

			return relict;
		}

		[HttpPost()]
		[Authorize(Policy = "add:ruintype")]
		public async Task<RuinType> Create([FromBody] RuinType data)
		{
			return await _repository.CreateRuinType(data);
		}

		[HttpPut("{id}")]
		[Authorize(Policy = "add:ruintype")]
		[Authorize(Policy = "edit:ruintype")]
		public async Task<RuinType> CreateOrUpdate([FromBody] RuinType data, int id)
		{
			try
			{
				return await _repository.CreateOrUpdateRuinTypeById(id, data);
			}
			catch (Exception e)
			{
				throw new HttpException(HttpStatusCode.Conflict, "Conflict", e);
			}
		}

		[HttpPatch("{id}")]
		[Authorize(Policy = "edit:ruintype")]
		public async Task<RuinType> Update([FromBody] RuinType data, int id)
		{
			try
			{
				return await _repository.UpdateRuinType(id, data);
			}
			catch (Exception e)
			{
				throw new HttpException(HttpStatusCode.Conflict, "Conflict", e);
			}
		}

		[HttpDelete("{id}")]
		[Authorize(Policy = "delete:ruintype")]
		public async Task<ActionResult> Delete(int id)
		{
			return (await _repository.DeleteRuinTypeById(id))
				? (ActionResult)Ok()
				: NotFound();
		}
	}
}
