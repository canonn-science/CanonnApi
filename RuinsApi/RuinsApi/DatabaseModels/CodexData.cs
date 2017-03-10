using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace RuinsApi.DatabaseModels
{
	public partial class CodexData
	{
		public CodexData()
		{
			Obelisk = new HashSet<Obelisk>();
		}

		public int Id { get; set; }
		public int CategoryId { get; set; }
		public DateTime Created { get; set; }
		public int EntryNumber { get; set; }
		public string Text { get; set; }
		public DateTime Updated { get; set; }

		public virtual ICollection<Obelisk> Obelisk { get; set; }

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual CodexCategory Category { get; set; }
	}
}
