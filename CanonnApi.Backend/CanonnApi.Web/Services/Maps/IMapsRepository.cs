using System.Linq;
using System.Threading.Tasks;

namespace CanonnApi.Web.Services.Maps
{
	public interface IMapsRepository
	{
		Task<IOrderedEnumerable<SystemDto>> LoadSitesOverview();
	}
}
