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
	[Route("v1/obelisk/groups")]
	public class ObeliskGroupController : Controller
	{
		private readonly ILogger _logger;
		private readonly IObeliskGroupRepository _repository;

		public ObeliskGroupController(ILogger<RelictsController> logger, IObeliskGroupRepository repository)
		{
			if (logger == null)
				throw new ArgumentNullException(nameof(logger));
			if (repository == null)
				throw new ArgumentNullException(nameof(repository));

			_logger = logger;
			_repository = repository;
		}

		[HttpGet]
		public async Task<List<ObeliskGroup>> Get()
		{
			return await _repository.GetAll();
		}

		[HttpGet("{id}")]
		public async Task<ObeliskGroup> Get(int id)
		{
			var relict = await _repository.GetById(id);
			if (relict == null)
				throw new HttpNotFoundException();

			return relict;
		}

		[HttpPost()]
		[Authorize(Policy = "add:obeliskgroup")]
		public async Task<ObeliskGroup> Create([FromBody] ObeliskGroup data)
		{
			return await _repository.Create(data);
		}

		[HttpPut("{id}")]
		[Authorize(Policy = "add:obeliskgroup")]
		[Authorize(Policy = "edit:obeliskgroup")]
		public async Task<ObeliskGroup> CreateOrUpdate([FromBody] ObeliskGroup data, int id)
		{
			try
			{
				return await _repository.CreateOrUpdateById(id, data);
			}
			catch (Exception e)
			{
				throw new HttpException(HttpStatusCode.Conflict, "Conflict", e);
			}
		}

		[HttpPatch("{id}")]
		[Authorize(Policy = "edit:obeliskgroup")]
		public async Task<ObeliskGroup> Update([FromBody] ObeliskGroup data, int id)
		{
			try
			{
				return await _repository.Update(id, data);
			}
			catch (Exception e)
			{
				throw new HttpException(HttpStatusCode.Conflict, "Conflict", e);
			}
		}

		[HttpDelete("{id}")]
		[Authorize(Policy = "delete:obeliskgroup")]
		public async Task<ActionResult> Delete(int id)
		{
			return (await _repository.DeleteById(id))
				? (ActionResult)Ok()
				: NotFound();
		}
	}
}
