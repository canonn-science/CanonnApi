using System.Collections.Generic;
using System.Threading.Tasks;
using RuinsApi.DatabaseModels;

namespace RuinsApi.Services.DataAccess
{
	public interface IRuinRepository
	{
		Task<List<RuinType>> GetAllRuinTypes();
		Task<RuinType> GetRuinTypeById(int id);
		Task<RuinType> CreateOrUpdateRuinTypeById(int id, RuinType value);
		Task<RuinType> CreateRuinType(RuinType value);
		Task<RuinType> UpdateRuinType(int id, RuinType value);
		Task<bool> DeleteRuinTypeById(int id);
	}
}