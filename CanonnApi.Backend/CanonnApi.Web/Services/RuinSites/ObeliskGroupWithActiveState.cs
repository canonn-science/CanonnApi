using CanonnApi.Web.DatabaseModels;

namespace CanonnApi.Web.Services.RuinSites
{
	public class ObeliskGroupWithActiveState: ObeliskGroup
	{
		public bool Active { get; set; }

		public ObeliskGroupWithActiveState() { }

		public ObeliskGroupWithActiveState(ObeliskGroup data)
		{
			Id = data.Id;
			Count = data.Count;
			Name = data.Name;
			RuintypeId = data.RuintypeId;
			Created = data.Created;
			Updated = data.Updated;

			Obelisk = data.Obelisk;
			RuinsiteObeliskgroups = data.RuinsiteObeliskgroups;
			Ruintype = data.Ruintype;
		}
	}
}
