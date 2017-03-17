using Microsoft.EntityFrameworkCore;
using RuinsApi.DatabaseModels;

namespace RuinsApi.Services.DataAccess
{
	public class SystemRepository : BaseDataRepository<DatabaseModels.System>, ISystemRepository
	{ 
		public SystemRepository(RuinsContext context)
			: base (context)
		{
		}

		protected override DbSet<DatabaseModels.System> DbSet()
		{
			return RuinsContext.System;
		}

		protected override void MapValues(DatabaseModels.System source, DatabaseModels.System target)
		{
			target.Name = source.Name;
			target.EdsmExtId = source.EdsmExtId;
			target.EddbExtId = source.EddbExtId;
		}
	}
}
