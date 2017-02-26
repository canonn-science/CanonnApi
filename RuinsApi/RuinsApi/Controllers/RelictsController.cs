using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RuinsApi.DatabaseModels;

namespace RuinsApi.Controllers
{
	[Route("v1/relicts")]
	public class RelictsController : Controller
	{
		private readonly ILogger _logger;
		private readonly RuinsContext _ruinsContext;

		public RelictsController(ILogger<RelictsController> logger, RuinsContext ruinsContext)
		{
			if (logger == null)
				throw new ArgumentNullException(nameof(logger));
			if (ruinsContext == null)
				throw new ArgumentNullException(nameof(ruinsContext));

			_logger = logger;
			_ruinsContext = ruinsContext;
		}

		[HttpGet]
		public async Task<List<Relict>> Get()
		{
			return await _ruinsContext.Relict.ToListAsync();
		}
	}
}
