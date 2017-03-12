using Microsoft.EntityFrameworkCore;

namespace RuinsApi.DatabaseModels
{
	public partial class RuinsContext
	{
		public RuinsContext(DbContextOptions<RuinsContext> options)
			:base(options)
		{ }
	}

	public partial class Relict: IEntity { }
	public partial class CodexCategory: IEntity { }
	public partial class CodexData: IEntity { }
	public partial class ObeliskGroup: IEntity { }
	public partial class RuinType: IEntity { }
}
