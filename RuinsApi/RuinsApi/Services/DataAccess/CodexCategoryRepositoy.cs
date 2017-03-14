using Microsoft.EntityFrameworkCore;
using RuinsApi.DatabaseModels;

namespace RuinsApi.Services.DataAccess
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
