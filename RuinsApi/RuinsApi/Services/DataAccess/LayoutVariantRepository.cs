using Microsoft.EntityFrameworkCore;
using RuinsApi.DatabaseModels;

namespace RuinsApi.Services.DataAccess
{
	public class LayoutVariantRepository : BaseDataRepository<LayoutVariant>, ILayoutVariantRepository
	{
		public LayoutVariantRepository(RuinsContext context)
			: base(context)
		{
		}

		protected override DbSet<LayoutVariant> DbSet()
		{
			return RuinsContext.LayoutVariant;
		}

		protected override void MapValues(LayoutVariant source, LayoutVariant target)
		{
			target.LayoutId = source.LayoutId;
			target.Name = source.Name;
		}
	}
}
