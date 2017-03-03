using System.Collections.Generic;
using System.Threading.Tasks;
using RuinsApi.DatabaseModels;

namespace RuinsApi.Services.DataAccess
{
	public interface IRelictRepository
	{
		Task<List<Relict>> GetAllRelicts();
		Task<Relict> GetRelictById(int id);
		Task<Relict> CreateRelict(Relict value);
		Task<bool> DeleteRelictById(int id);
		Task<Relict> CreateOrUpdateRelictById(int id, Relict value);
		Task<Relict> UpdateRelict(int id, Relict value);
	}
}