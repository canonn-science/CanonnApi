using Microsoft.EntityFrameworkCore;
using CanonnApi.Web.DatabaseModels;

namespace CanonnApi.Web.Services.DataAccess
{
	public class ArtifactRepository : BaseDataRepository<Artifact>, IArtifactRepository
	{ 
		public ArtifactRepository(CanonnApiDatabaseContext context)
			: base (context)
		{
		}

		protected override DbSet<Artifact> DbSet()
		{
			return CanonnApiDatabaseContext.Artifact;
		}

		protected override void MapValues(Artifact source, Artifact target)
		{
			target.Name = source.Name;
		}
	}
}
