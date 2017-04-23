using AutoMapper;
using CanonnApi.Web.DatabaseModels;

namespace CanonnApi.Web.Controllers.Models
{
	/// <summary>
	/// Representation of a ruin site with additional info about obelisk groups and obelisks
	/// </summary>
	public class RuinSiteWithObeliskDataDto : RuinSiteDto
	{
		/// <summary>
		/// Name of the system this ruin site is in
		/// </summary>
		public string SelectedSystem { get; set; }
		/// <summary>
		/// name of the body this ruin site is located on
		/// </summary>
		public string SelectedBody { get; set; }

		/// <summary>
		/// All obelisk groups for this ruin type, marked available or not
		/// </summary>
		public ObeliskGroupWithActiveStateDto[] ObeliskGroups { get; set; }
		/// <summary>
		/// All obelisks for this ruin type, marked active or not
		/// </summary>
		public ObeliskWithActiveStateDto[] Obelisks { get; set; }
	}
}
