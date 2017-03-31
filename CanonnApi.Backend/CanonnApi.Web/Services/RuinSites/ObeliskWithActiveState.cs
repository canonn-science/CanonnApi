using CanonnApi.Web.DatabaseModels;

namespace CanonnApi.Web.Services.RuinSites
{
	public class ObeliskWithActiveState : Obelisk
	{
		public bool Active { get; set; }

		public ObeliskWithActiveState() { }

		public ObeliskWithActiveState(Obelisk obelisk)
		{
			Id = obelisk.Id;
			ArtifactId = obelisk.ArtifactId;
			CodexdataId = obelisk.CodexdataId;
			IsBroken = obelisk.IsBroken;
			Number = obelisk.Number;
			ObeliskgroupId = obelisk.ObeliskgroupId;
			Created = obelisk.Created;
			Updated = obelisk.Updated;

			Artifact = obelisk.Artifact;
			Codexdata = obelisk.Codexdata;
			Obeliskgroup = obelisk.Obeliskgroup;
		}
	}
}
