using Microsoft.EntityFrameworkCore;
using CanonnApi.Web.DatabaseModels;

namespace CanonnApi.Web.Services.DataAccess
{
	public class CodexDataRepository : BaseDataRepository<CodexData>, ICodexDataRepository
	{
		public CodexDataRepository(CanonnApiDatabaseContext context)
			:base(context)
		{
		}

		protected override DbSet<CodexData> DbSet()
		{
			return CanonnApiDatabaseContext.CodexData;
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
