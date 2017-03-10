using Microsoft.EntityFrameworkCore;

namespace RuinsApi.DatabaseModels
{
	public partial class RuinsContext
	{
		public RuinsContext(DbContextOptions<RuinsContext> options)
			:base(options)
		{ }
	}
}
