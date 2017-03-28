using CanonnApi.Web.DatabaseModels;

namespace CanonnApi.Web.Services.RuinSites
{
	public class ObeliskGroupWithActiveState: ObeliskGroup
	{
		public bool Active { get; set; }

		public ObeliskGroupWithActiveState(ObeliskGroup data)
		{
			Id = data.Id;
			Count = data.Count;
			Name = data.Name;
			RuintypeId = data.RuintypeId;

			Created = data.Created;
			Updated = data.Updated;

			// calculated properties
			Active = data.RuinsiteObeliskgroups != null;
		}
	}
}
