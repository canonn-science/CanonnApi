using System.Collections.Generic;
using System.Threading.Tasks;
using RuinsApi.DatabaseModels;

namespace RuinsApi.Services.DataAccess
{
	public interface ICodexRepository
	{
		Task<List<CodexData>> GetAllData();
		Task<CodexData> GetDataById(int id);
		Task<CodexData> CreateOrUpdateData(int id, CodexData value);
		Task<CodexData> CreateData(CodexData value);
		Task<CodexData> UpdateData(int id, CodexData value);
		Task<bool> DeleteDataById(int id);

		Task<List<CodexCategory>> GetAllCategories();
		Task<CodexCategory> GetCategoryById(int id);
		Task<CodexCategory> CreateOrUpdateCategory(int id, CodexCategory value);
		Task<CodexCategory> CreateCategory(CodexCategory value);
		Task<CodexCategory> UpdateCategory(int id, CodexCategory value);
		Task<bool> DeleteCategoryById(int id);
	}
}
