using Microsoft.EntityFrameworkCore;
using CanonnApi.Web.DatabaseModels;

namespace CanonnApi.Web.Services.DataAccess
{
	public class BodyRepository : BaseDataRepository<Body>, IBodyRepository
	{ 
		public BodyRepository(RuinsContext context)
			: base (context)
		{
		}

		protected override DbSet<Body> DbSet()
		{
			return RuinsContext.Body;
		}

		protected override void MapValues(Body source, Body target)
		{
			target.SystemId = source.SystemId;
			target.Name = source.Name;
			target.EdsmExtId = source.EdsmExtId;
			target.EddbExtId = source.EddbExtId;
		}
	}
}
