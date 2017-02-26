using System;
using System.Collections.Generic;

namespace RuinsApi.DatabaseModels
{
	public partial class Relict
	{
		public Relict()
		{
			CodexCategory = new HashSet<CodexCategory>();
		}

		public int Id { get; set; }
		public string Name { get; set; } // Inserted manually??
		public DateTime Created { get; set; }
		public DateTime Updated { get; set; }

		public virtual ICollection<CodexCategory> CodexCategory { get; set; }
	}
}
