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
	[Route("v1/layout/variant")]
	public class LayoutVariantController : Controller
	{
		private readonly ILogger _logger;
		private readonly ILayoutVariantRepository _repository;

		public LayoutVariantController(ILogger<LayoutVariantController> logger, ILayoutVariantRepository repository)
		{
			if (logger == null)
				throw new ArgumentNullException(nameof(logger));
			if (repository == null)
				throw new ArgumentNullException(nameof(repository));

			_logger = logger;
			_repository = repository;
		}

		[HttpGet]
		public async Task<List<LayoutVariant>> Get()
		{
			return await _repository.GetAll();
		}

		[HttpGet("{id}")]
		public async Task<LayoutVariant> Get(int id)
		{
			var entry = await _repository.GetById(id);
			if (entry == null)
				throw new HttpNotFoundException();

			return entry;
		}

		[HttpPost()]
		[Authorize(Policy = "add:layoutvariant")]
		public async Task<LayoutVariant> Create([FromBody] LayoutVariant data)
		{
			return await _repository.Create(data);
		}

		[HttpPut("{id}")]
		[Authorize(Policy = "add:layoutvariant")]
		[Authorize(Policy = "edit:layoutvariant")]
		public async Task<LayoutVariant> CreateOrUpdate([FromBody] LayoutVariant data, int id)
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
		[Authorize(Policy = "edit:layoutvariant")]
		public async Task<LayoutVariant> Update([FromBody] LayoutVariant data, int id)
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
		[Authorize(Policy = "delete:layoutvariant")]
		public async Task<ActionResult> Delete(int id)
		{
			return (await _repository.DeleteById(id))
				? (ActionResult)Ok()
				: NotFound();
		}
	}
}
