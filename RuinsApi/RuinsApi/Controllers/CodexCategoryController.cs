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
	[Route("v1/codex/category")]
	public class CodexCategoryController : Controller
	{
		private readonly ILogger _logger;
		private readonly ICodexRepository _repository;

		public CodexCategoryController(ILogger<CodexDataController> logger, ICodexRepository repository)
		{
			if (logger == null)
				throw new ArgumentNullException(nameof(logger));
			if (repository == null)
				throw new ArgumentNullException(nameof(repository));

			_logger = logger;
			_repository = repository;
		}

		[HttpGet]
		public async Task<List<CodexCategory>> Get()
		{
			return await _repository.GetAllCategories();
		}

		[HttpGet("{id}")]
		public async Task<CodexCategory> Get(int id)
		{
			var relict = await _repository.GetCategoryById(id);
			if (relict == null)
				throw new HttpNotFoundException();

			return relict;
		}

		[HttpPost()]
		[Authorize(Policy = "add:codexcategory")]
		public async Task<CodexCategory> Create([FromBody] CodexCategory data)
		{
			return await _repository.CreateCategory(data);
		}

		[HttpPut("{id}")]
		[Authorize(Policy = "add:codexcategory")]
		[Authorize(Policy = "edit:codexcategory")]
		public async Task<CodexCategory> CreateOrUpdate([FromBody] CodexCategory data, int id)
		{
			try
			{
				return await _repository.CreateOrUpdateCategory(id, data);
			}
			catch (Exception e)
			{
				throw new HttpException(HttpStatusCode.Conflict, "Conflict", e);
			}
		}

		[HttpPatch("{id}")]
		[Authorize(Policy = "edit:codexcategory")]
		public async Task<CodexCategory> Update([FromBody] CodexCategory data, int id)
		{
			try
			{
				return await _repository.UpdateCategory(id, data);
			}
			catch (Exception e)
			{
				throw new HttpException(HttpStatusCode.Conflict, "Conflict", e);
			}
		}

		[HttpDelete("{id}")]
		[Authorize(Policy = "delete:codexcategory")]
		public async Task<ActionResult> Delete(int id)
		{
			return (await _repository.DeleteCategoryById(id))
				? (ActionResult)Ok()
				: NotFound();
		}
	}
}
