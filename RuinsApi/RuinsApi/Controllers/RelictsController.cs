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
	[Route("v1/relicts")]
	public class RelictsController : Controller
	{
		private readonly ILogger _logger;
		private readonly IRelictRepository _repository;

		public RelictsController(ILogger<RelictsController> logger, IRelictRepository relictRepository)
		{
			if (logger == null)
				throw new ArgumentNullException(nameof(logger));
			if (relictRepository == null)
				throw new ArgumentNullException(nameof(relictRepository));

			_logger = logger;
			_repository = relictRepository;
		}

		[HttpGet]
		public async Task<List<Relict>> Get()
		{
			return await _repository.GetAll();
		}

		[HttpGet("{id}")]
		public async Task<Relict> Get(int id)
		{
			var relict = await _repository.GetById(id);
			if (relict == null)
				throw new HttpNotFoundException();

			return relict;
		}

		[HttpPost()]
		[Authorize(Policy = "add:relict")]
		public async Task<Relict> Create([FromBody] Relict data)
		{
			return await _repository.Create(data);
		}

		[HttpPut("{id}")]
		[Authorize(Policy = "add:relict")]
		[Authorize(Policy = "edit:relict")]
		public async Task<Relict> CreateOrUpdate([FromBody] Relict data, int id)
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
		[Authorize(Policy = "edit:relict")]
		public async Task<Relict> Update([FromBody] Relict data, int id)
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
		[Authorize(Policy = "delete:relict")]
		public async Task<ActionResult> Delete(int id)
		{
			return (await _repository.DeleteById(id))
				? (ActionResult)Ok()
				: NotFound();
		}
	}
}
