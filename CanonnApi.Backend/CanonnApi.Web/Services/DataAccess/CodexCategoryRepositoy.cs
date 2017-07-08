using Microsoft.EntityFrameworkCore;
using CanonnApi.Web.DatabaseModels;

namespace CanonnApi.Web.Services.DataAccess
{
	public class CodexCategoryRepositoy : BaseDataRepository<CodexCategory>, ICodexCategoryRepository
	{
		public CodexCategoryRepositoy(CanonnApiDatabaseContext context)
			:base(context)
		{
		}

		protected override DbSet<CodexCategory> DbSet()
		{
			return CanonnApiDatabaseContext.CodexCategory;
		}

		protected override void MapValues(CodexCategory source, CodexCategory target)
		{
			target.Name = source.Name;
			target.ArtifactId = source.ArtifactId;
		}
	}
}
