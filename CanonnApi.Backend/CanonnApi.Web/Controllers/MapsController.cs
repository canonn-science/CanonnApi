using System;
using System.Threading.Tasks;
using CanonnApi.Web.Services.Maps;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CanonnApi.Web.Controllers
{
	[Route("v1/maps")]
	public class MapsController : Controller
	{
		private readonly IMapsRepository _repository;

		public MapsController(IMapsRepository repository)
		{
			_repository = repository ?? throw new ArgumentNullException(nameof(repository));
		}

		public override JsonResult Json(object data)
		{
			// for the map, we want empty collections to be included in the JSON result
			return Json(data, new JsonSerializerSettings()
			{
				ContractResolver = new CamelCasePropertyNamesContractResolver(),
			});
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

		[HttpGet("ruininfo/{id}")]
		public async Task<ActionResult> GetRuinInfo(int id)
		{
			
			return Json(await _repository.LoadRuinInfo(id));
		}
	}
}
