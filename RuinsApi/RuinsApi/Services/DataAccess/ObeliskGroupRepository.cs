using Microsoft.EntityFrameworkCore;
using RuinsApi.DatabaseModels;

namespace RuinsApi.Services.DataAccess
{
	public class ObeliskGroupRepository : BaseDataRepository<ObeliskGroup>, IObeliskGroupRepository
	{
		public ObeliskGroupRepository(RuinsContext context)
			: base(context)
		{
		}

		protected override DbSet<ObeliskGroup> DbSet()
		{
			return RuinsContext.ObeliskGroup;
		}

		protected override void MapValues(ObeliskGroup source, ObeliskGroup target)
		{
			target.TypeId = source.TypeId;
			target.Count = source.Count;
			target.Name = source.Name;
		}
	}
}
