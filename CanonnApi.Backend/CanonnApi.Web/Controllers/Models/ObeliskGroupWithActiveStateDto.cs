namespace CanonnApi.Web.Controllers.Models
{
	/// <summary>
	/// Representation of an obelisk group that may or may not be available on a certain site
	/// </summary>
	public class ObeliskGroupWithActiveStateDto: ObeliskGroupDto
	{
		/// <summary>
		/// Determines whether this obelisk group is available or not
		/// </summary>
		public bool Active { get; set; }
	}
}
