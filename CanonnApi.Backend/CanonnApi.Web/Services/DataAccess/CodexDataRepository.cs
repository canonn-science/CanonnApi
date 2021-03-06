using Microsoft.EntityFrameworkCore;
using CanonnApi.Web.DatabaseModels;

namespace CanonnApi.Web.Services.DataAccess
{
	public class CodexDataRepository : BaseDataRepository<CodexData>, ICodexDataRepository
	{
		public CodexDataRepository(RuinsContext context)
			:base(context)
		{
		}

		protected override DbSet<CodexData> DbSet()
		{
			return RuinsContext.CodexData;
		}

		protected override void MapValues(CodexData source, CodexData target)
		{
			target.CategoryId = source.CategoryId;
			target.EntryNumber = source.EntryNumber;
			target.Text = source.Text;
			target.ArtifactId = source.ArtifactId;
		}
	}
}
