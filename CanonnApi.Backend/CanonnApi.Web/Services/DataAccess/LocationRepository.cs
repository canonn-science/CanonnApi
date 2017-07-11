using Microsoft.EntityFrameworkCore;
using CanonnApi.Web.DatabaseModels;

namespace CanonnApi.Web.Services.DataAccess
{
	public class LocationRepository : BaseDataRepository<Location>, ILocationRepository
	{
		public LocationRepository(CanonnApiDatabaseContext context)
			: base(context)
		{
		}

		protected override DbSet<Location> DbSet()
		{
			return CanonnApiDatabaseContext.Location;
		}

		protected override void MapValues(Location source, Location target)
		{
			target.LocationtypeId = source.LocationtypeId;
			target.SystemId = source.SystemId;

			target.BodyId = source.BodyId;
			target.Latitude = source.Latitude;
			target.Longitude = source.Longitude;

			target.DirectionSystemId = source.DirectionSystemId;
			target.Distance = source.Distance;

			target.IsVisible = source.IsVisible;
		}
	}
}
