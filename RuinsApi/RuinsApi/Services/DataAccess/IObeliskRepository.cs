using System.Collections.Generic;
using System.Threading.Tasks;
using RuinsApi.DatabaseModels;

namespace RuinsApi.Services.DataAccess
{
	public interface IObeliskRepository
	{
		Task<List<ObeliskGroup>> GetAllObeliskGroups();
		Task<ObeliskGroup> GetObeliskGroupById(int id);
		Task<ObeliskGroup> CreateOrUpdateObeliskGroupById(int id, ObeliskGroup value);
		Task<ObeliskGroup> CreateObeliskGroup(ObeliskGroup value);
		Task<ObeliskGroup> UpdateObeliskGroup(int id, ObeliskGroup value);
		Task<bool> DeleteObeliskGroupById(int id);
	}
}
