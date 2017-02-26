using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Remotion.Linq.Clauses;
using RuinsApi.DatabaseModels;

namespace RuinsApi.Controllers
{
	[Route("v1/codex")]
	public class CodexController : Controller
	{
		private readonly ILogger _logger;
		private readonly RuinsContext _ruinsContext;

		public CodexController(ILogger<CodexController> logger, RuinsContext ruinsContext)
		{
			if (logger == null)
				throw new ArgumentNullException(nameof(logger));
			if (ruinsContext == null)
				throw new ArgumentNullException(nameof(ruinsContext));

			_logger = logger;
			_ruinsContext = ruinsContext;
		}

		[HttpGet]
		[Route("categories")]
		public async Task<List<CodexCategory>> GetCategories()
		{
			return await _ruinsContext.CodexCategory.ToListAsync();
		}

		[HttpGet]
		[Route("data")]
		public async Task<List<CodexData>> GetData()
		{
			return await _ruinsContext.CodexData.ToListAsync();
		}

		[HttpGet]
		[Route("data/{categoryId}")]
		public async Task<List<CodexData>> GetData(int categoryId)
		{
			return await _ruinsContext.CodexData
				.Where(d => d.CategoryId == categoryId)
				.ToListAsync();
		}

		[HttpGet]
		[Route("data/{categoryId}/{entryId}")]
		public async Task<List<CodexData>> GetData(int categoryId, int entryId)
		{
			return await _ruinsContext.CodexData
				.Where(d => (d.CategoryId == categoryId) && (d.EntryNumber == entryId))
				.ToListAsync();
		}

		[HttpGet]
		[Route("data/overview")]
		[Produces("application/json")]
		public object GetOverview()
		{
			return from data in _ruinsContext.CodexData
				join category in _ruinsContext.CodexCategory on data.CategoryId equals category.Id
				join relict in _ruinsContext.Relict on category.PrimaryRelict equals relict.Id
				select new
				{
					CategoryName = category.Name,
					EntryId = data.EntryNumber,
					PrimaryRelic = relict.Name,
					Text = data.Text,
					Amount = (from amount in _ruinsContext.CodexData where amount.CategoryId == data.CategoryId select data.Id).Count()
				};
		}
	}
}
