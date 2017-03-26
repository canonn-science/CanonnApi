using System;
using System.Threading.Tasks;
using CanonnApi.Web.Services.Maps;
using Microsoft.AspNetCore.Mvc;

namespace CanonnApi.Web.Controllers
{
	[Route("v1/maps")]
	public class MapsController : Controller
	{
		private IMapsRepository _repository { get; }

		public MapsController(IMapsRepository repository)
		{
			_repository = repository ?? throw new ArgumentNullException(nameof(repository));
		}

		[HttpGet("systemoverview")]
		public async Task<ActionResult> GetSystemsOverview()
		{
			return Json(await _repository.LoadSitesOverview());
		}

		[HttpGet("scandata")]
		public async Task<ActionResult> GetScanData()
		{
			return Json(await _repository.LoadScanData());
		}
	}
}
