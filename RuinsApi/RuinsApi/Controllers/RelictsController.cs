using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RuinsApi.DatabaseModels;
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
			return await _repository.GetAllRelicts();
		}

		[HttpGet("{id}")]
		public async Task<ActionResult> Get(int id)
		{
			var relict = await _repository.GetRelictById(id);
			if (relict != null)
				return Json(relict);

			return NotFound();
		}

		[HttpPost()]
		[Authorize(Policy = "add:relict")]
		public async Task<ActionResult> Create([FromBody] Relict relictData)
		{
			return Json(await _repository.CreateRelict(relictData));
		}

		[HttpPut("{id}")]
		[Authorize(Policy = "add:relict")]
		[Authorize(Policy = "edit:relict")]
		public async Task<ActionResult> CreateOrUpdate([FromBody] Relict relictData, int id)
		{
			try
			{
				return Json(await _repository.CreateOrUpdateRelictById(id, relictData));
			}
			catch (Exception)
			{
				return new StatusCodeResult(409); // conflict
			}
		}

		[HttpPatch("{id}")]
		[Authorize(Policy = "edit:relict")]
		public async Task<ActionResult> Update([FromBody] Relict relictData, int id)
		{
			try
			{
				return Json(await _repository.UpdateRelict(id, relictData));
			}
			catch (Exception)
			{
				return new StatusCodeResult(409); // conflict
			}
		}

		[HttpDelete("{id}")]
		[Authorize(Policy = "delete:relict")]
		public async Task<ActionResult> Delete(int id)
		{
			return (await _repository.DeleteRelictById(id))
				? (ActionResult)Ok()
				: NotFound();
		}
	}
}
