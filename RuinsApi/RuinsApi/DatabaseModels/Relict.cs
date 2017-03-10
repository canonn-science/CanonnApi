using System;
using System.Collections.Generic;

namespace RuinsApi.DatabaseModels
{
	public partial class Relict
	{
		public Relict()
		{
			CodexCategory = new HashSet<CodexCategory>();
			Obelisk = new HashSet<Obelisk>();
		}

		public int Id { get; set; }
		public string Name { get; set; }
		public DateTime Created { get; set; }
		public DateTime Updated { get; set; }

		public virtual ICollection<CodexCategory> CodexCategory { get; set; }
		public virtual ICollection<Obelisk> Obelisk { get; set; }
	}
}
