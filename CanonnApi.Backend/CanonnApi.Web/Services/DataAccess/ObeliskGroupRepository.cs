using Microsoft.EntityFrameworkCore;
using CanonnApi.Web.DatabaseModels;

namespace CanonnApi.Web.Services.DataAccess
{
	public class ObeliskGroupRepository : BaseDataRepository<ObeliskGroup>, IObeliskGroupRepository
	{
		public ObeliskGroupRepository(CanonnApiDatabaseContext context)
			: base(context)
		{
		}

		protected override DbSet<ObeliskGroup> DbSet()
		{
			return CanonnApiDatabaseContext.ObeliskGroup;
		}

		protected override void MapValues(ObeliskGroup source, ObeliskGroup target)
		{
			target.RuintypeId = source.RuintypeId;
			target.Count = source.Count;
			target.Name = source.Name;
		}
	}
}
