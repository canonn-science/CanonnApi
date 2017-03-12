using Microsoft.EntityFrameworkCore;
using RuinsApi.DatabaseModels;

namespace RuinsApi.Services.DataAccess
{
	public class RelictRepository : BaseDataRepository<Relict>, IRelictRepository
	{ 
		public RelictRepository(RuinsContext context)
			: base (context)
		{
		}

		protected override DbSet<Relict> DbSet()
		{
			return RuinsContext.Relict;
		}

		protected override void MapValues(Relict source, Relict target)
		{
			target.Name = source.Name;
		}
	}
}
