using System.Collections.Generic;
using System.Threading.Tasks;

namespace CanonnApi.Web.Services.Maps
{
	public interface IMapsRepository
	{
		Task<List<MapsSystem>> LoadSitesOverview();
		Task<object> LoadScanData();
		Task<object> LoadRuinInfo(int id);
	}
}
