using Microsoft.EntityFrameworkCore;
using CanonnApi.Web.DatabaseModels;

namespace CanonnApi.Web.Services.DataAccess
{
	public class CodexCategoryRepositoy : BaseDataRepository<CodexCategory>, ICodexCategoryRepository
	{
		public CodexCategoryRepositoy(RuinsContext context)
			:base(context)
		{
		}

		protected override DbSet<CodexCategory> DbSet()
		{
			return RuinsContext.CodexCategory;
		}

		protected override void MapValues(CodexCategory source, CodexCategory target)
		{
			target.Name = source.Name;
			target.ArtifactId = source.ArtifactId;
		}
	}
}
