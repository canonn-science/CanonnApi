using Microsoft.EntityFrameworkCore;
using CanonnApi.Web.DatabaseModels;

namespace CanonnApi.Web.Services.DataAccess
{
	public class LocationTypeRepository : BaseDataRepository<LocationType>, ILocationTypeRepository
	{
		public LocationTypeRepository(CanonnApiDatabaseContext context)
			: base(context)
		{
		}

		protected override DbSet<LocationType> DbSet()
		{
			return CanonnApiDatabaseContext.LocationType;
		}

		protected override void MapValues(LocationType source, LocationType target)
		{
			target.Name = source.Name;
			target.ShortName = source.ShortName;
			target.IsSurface = source.IsSurface;
		}
	}
}
