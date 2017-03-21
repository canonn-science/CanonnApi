using System.Collections.Generic;
using System.Threading.Tasks;
using CanonnApi.Web.DatabaseModels;

namespace CanonnApi.Web.Services.DataAccess
{
	public interface IBaseDataRepository<T>
		where T : class, IEntity, new()
	{
		Task<List<T>> GetAll();
		Task<T> GetById(int id);
		Task<T> CreateOrUpdateById(int id, T value);
		Task<T> Create(T value);
		Task<T> Update(int id, T value);
		Task<bool> DeleteById(int id);
	}
}
