namespace CanonnApi.Web.Controllers.Models
{
	/// <summary>
	/// Representation of an obelisk that may or may not be active on a site
	/// </summary>
	public class ObeliskWithActiveStateDto: ObeliskDto
	{
		/// <summary>
		/// Determines whether this obelisk is active or not
		/// </summary>
		public bool Active { get; set; }
	}
}
