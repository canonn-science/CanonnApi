using Microsoft.EntityFrameworkCore;

namespace RuinsApi.DatabaseModels
{
	public partial class RuinsContext
	{
		public RuinsContext(DbContextOptions<RuinsContext> options)
			:base(options)
		{ }
	}

	public partial class Artifact: IEntity { }
	public partial class CodexCategory: IEntity { }
	public partial class CodexData: IEntity { }
	public partial class ObeliskGroup: IEntity { }
	public partial class RuinType: IEntity { }
	public partial class System: IEntity { }
	public partial class Body: IEntity { }
	public partial class Obelisk: IEntity { }
	public partial class RuinSite: IEntity { }
}
