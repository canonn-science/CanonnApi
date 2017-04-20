using System.Linq;
using System.Threading.Tasks;

namespace CanonnApi.Web.Services.Maps
{
	public interface IMapsRepository
	{
		Task<IOrderedEnumerable<SystemDto>> LoadSitesOverview();
		Task<object> LoadScanData();
		Task<object> LoadRuinInfo(int id);
		Task<object> LoadBrokenObelisks();
	}
}
