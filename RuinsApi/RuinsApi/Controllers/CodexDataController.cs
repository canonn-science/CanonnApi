using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RuinsApi.DatabaseModels;
using RuinsApi.Middlewares;
using RuinsApi.Services.DataAccess;

namespace RuinsApi.Controllers
{
	[Route("v1/codex/data")]
	public class CodexDataController : Controller
	{
		private readonly ILogger _logger;
		private readonly ICodexRepository _repository;

		public CodexDataController(ILogger<CodexDataController> logger, ICodexRepository repository)
		{
			if (logger == null)
				throw new ArgumentNullException(nameof(logger));
			if (repository == null)
				throw new ArgumentNullException(nameof(repository));

			_logger = logger;
			_repository = repository;
		}

		[HttpGet]
		public async Task<List<CodexData>> Get(bool withCategoryName = false)
		{
			return await _repository.GetAllData(withCategoryName);
		}

		[HttpGet("{id}")]
		public async Task<CodexData> Get(int id)
		{
			var relict = await _repository.GetDataById(id);
			if (relict == null)
				throw new HttpNotFoundException();

			return relict;
		}

		[HttpPost()]
		[Authorize(Policy = "add:codexdata")]
		public async Task<CodexData> Create([FromBody] CodexData data)
		{
			return await _repository.CreateData(data);
		}

		[HttpPut("{id}")]
		[Authorize(Policy = "add:codexdata")]
		[Authorize(Policy = "edit:codexdata")]
		public async Task<CodexData> CreateOrUpdate([FromBody] CodexData data, int id)
		{
			try
			{
				return await _repository.CreateOrUpdateData(id, data);
			}
			catch (Exception e)
			{
				throw new HttpException(HttpStatusCode.Conflict, "Conflict", e);
			}
		}

		[HttpPatch("{id}")]
		[Authorize(Policy = "edit:codexdata")]
		public async Task<CodexData> Update([FromBody] CodexData data, int id)
		{
			try
			{
				return await _repository.UpdateData(id, data);
			}
			catch (Exception e)
			{
				throw new HttpException(HttpStatusCode.Conflict, "Conflict", e);
			}
		}

		[HttpDelete("{id}")]
		[Authorize(Policy = "delete:codexdata")]
		public async Task<ActionResult> Delete(int id)
		{
			return (await _repository.DeleteDataById(id))
				? (ActionResult)Ok()
				: NotFound();
		}
	}
}
