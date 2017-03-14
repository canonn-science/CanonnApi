using Microsoft.EntityFrameworkCore;
using RuinsApi.DatabaseModels;

namespace RuinsApi.Services.DataAccess
{
	public class RuinLayoutRepository : BaseDataRepository<RuinLayout>, IRuinLayoutRepository
	{
		public RuinLayoutRepository(RuinsContext context)
			: base(context)
		{
		}

		protected override DbSet<RuinLayout> DbSet()
		{
			return RuinsContext.RuinLayout;
		}

		protected override void MapValues(RuinLayout source, RuinLayout target)
		{
			target.RuintypeId = source.RuintypeId;
			target.Name = source.Name;
		}
	}
}
