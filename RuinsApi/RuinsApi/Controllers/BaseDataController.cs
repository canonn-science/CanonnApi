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
	public abstract class BaseDataController<T> : Controller
		where T: class, IEntity, new()
	{
		private readonly ILogger _logger;
		protected readonly IBaseDataRepository<T> Repository;

		protected BaseDataController(ILogger logger, IBaseDataRepository<T> repository)
		{
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
			Repository = repository ?? throw new ArgumentNullException(nameof(repository));
		}

		[HttpGet]
		public virtual async Task<List<T>> Get()
		{
			return await Repository.GetAll();
		}

		[HttpGet("{id}")]
		public virtual async Task<T> Get(int id)
		{
			var entry = await Repository.GetById(id);
			if (entry == null)
				throw new HttpNotFoundException();

			return entry;
		}

		[HttpPost()]
		[Authorize(Policy = "add:codexcategory")]
		public virtual async Task<T> Create([FromBody] T data)
		{
			return await Repository.Create(data);
		}

		[HttpPut("{id}")]
		[Authorize(Policy = "add:codexcategory")]
		[Authorize(Policy = "edit:codexcategory")]
		public virtual async Task<T> CreateOrUpdate([FromBody] T data, int id)
		{
			try
			{
				return await Repository.CreateOrUpdateById(id, data);
			}
			catch (Exception e)
			{
				throw new HttpException(HttpStatusCode.Conflict, "Conflict", e);
			}
		}

		[HttpPatch("{id}")]
		[Authorize(Policy = "edit:codexcategory")]
		public virtual async Task<T> Update([FromBody] T data, int id)
		{
			try
			{
				return await Repository.Update(id, data);
			}
			catch (Exception e)
			{
				throw new HttpException(HttpStatusCode.Conflict, "Conflict", e);
			}
		}

		[HttpDelete("{id}")]
		[Authorize(Policy = "delete:codexcategory")]
		public virtual async Task<ActionResult> Delete(int id)
		{
			return (await Repository.DeleteById(id))
				? (ActionResult)Ok()
				: NotFound();
		}
	}
}
