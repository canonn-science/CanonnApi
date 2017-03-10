using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace RuinsApi.DatabaseModels
{
	public partial class ObeliskGroup
	{
		public ObeliskGroup()
		{
			Obelisk = new HashSet<Obelisk>();
			RuinLayoutObeliskGroups = new HashSet<RuinLayoutObeliskGroups>();
		}

		public int Id { get; set; }
		public int Count { get; set; }
		public DateTime Created { get; set; }
		public string Name { get; set; }
		public int TypeId { get; set; }
		public DateTime Updated { get; set; }

		public virtual ICollection<Obelisk> Obelisk { get; set; }
		public virtual ICollection<RuinLayoutObeliskGroups> RuinLayoutObeliskGroups { get; set; }

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual RuinType Type { get; set; }
	}
}
