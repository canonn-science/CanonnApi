using Microsoft.EntityFrameworkCore;
using RuinsApi.DatabaseModels;

namespace RuinsApi.Services.DataAccess
{
	public class RuinlayoutVariantRepository : BaseDataRepository<RuinlayoutVariant>, IRuinlayoutVariantRepository
	{
		public RuinlayoutVariantRepository(RuinsContext context)
			: base(context)
		{
		}

		protected override DbSet<RuinlayoutVariant> DbSet()
		{
			return RuinsContext.RuinlayoutVariant;
		}

		protected override void MapValues(RuinlayoutVariant source, RuinlayoutVariant target)
		{
			target.RuinlayoutId = source.RuinlayoutId;
			target.Name = source.Name;
		}
	}
}
