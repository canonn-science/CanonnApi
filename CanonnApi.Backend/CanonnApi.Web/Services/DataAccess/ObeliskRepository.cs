using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CanonnApi.Web.DatabaseModels;

namespace CanonnApi.Web.Services.DataAccess
{
	public class ObeliskRepository : BaseDataRepository<Obelisk>, IObeliskRepository
	{
		public ObeliskRepository(RuinsContext context)
			: base(context)
		{
		}

		protected override DbSet<Obelisk> DbSet()
		{
			return RuinsContext.Obelisk;
		}

		protected override void MapValues(Obelisk source, Obelisk target)
		{
			target.ObeliskgroupId = source.ObeliskgroupId;
			target.Number = source.Number;
			target.IsBroken = source.IsBroken;
			target.IsVerified = source.IsVerified;
			target.CodexdataId = source.CodexdataId;
			target.ArtifactId = source.ArtifactId;
		}

		public Task<List<Obelisk>> Search(int ruintypeId, int obeliskgroupId)
		{
			var query = DbSet().AsQueryable();

			if (ruintypeId > 0)
			{
				query = query.Where(o => o.Obeliskgroup.Ruintype.Id == ruintypeId);
			}

			if (obeliskgroupId > 0)
			{
				query = query.Where(o => o.Obeliskgroup.Id == obeliskgroupId);
			}

			return query.ToListAsync();
		}
	}
}
