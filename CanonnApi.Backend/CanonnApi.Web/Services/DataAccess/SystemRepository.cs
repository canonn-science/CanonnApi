using Microsoft.EntityFrameworkCore;
using CanonnApi.Web.DatabaseModels;

namespace CanonnApi.Web.Services.DataAccess
{
	public class SystemRepository : BaseDataRepository<DatabaseModels.System>, ISystemRepository
	{ 
		public SystemRepository(CanonnApiDatabaseContext context)
			: base (context)
		{
		}

		protected override DbSet<DatabaseModels.System> DbSet()
		{
			return CanonnApiDatabaseContext.System;
		}

		protected override void MapValues(DatabaseModels.System source, DatabaseModels.System target)
		{
			target.Name = source.Name;
			target.EdsmExtId = source.EdsmExtId;
			target.EddbExtId = source.EddbExtId;
		}
	}
}
