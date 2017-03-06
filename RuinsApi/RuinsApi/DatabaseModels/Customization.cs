using Microsoft.EntityFrameworkCore;

namespace RuinsApi.DatabaseModels
{
	public partial class RuinsContext
	{
		public RuinsContext(DbContextOptions<RuinsContext> options)
			:base(options)
		{ }
	}

	public partial class Relict
	{
		public string Name { get; set; }
	}
}
