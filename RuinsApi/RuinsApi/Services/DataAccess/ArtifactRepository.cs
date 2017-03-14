using Microsoft.EntityFrameworkCore;
using RuinsApi.DatabaseModels;

namespace RuinsApi.Services.DataAccess
{
	public class ArtifactRepository : BaseDataRepository<Artifact>, IArtifactRepository
	{ 
		public ArtifactRepository(RuinsContext context)
			: base (context)
		{
		}

		protected override DbSet<Artifact> DbSet()
		{
			return RuinsContext.Artifact;
		}

		protected override void MapValues(Artifact source, Artifact target)
		{
			target.Name = source.Name;
		}
	}
}
