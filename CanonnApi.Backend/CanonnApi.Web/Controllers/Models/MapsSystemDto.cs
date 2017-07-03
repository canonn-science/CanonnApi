namespace CanonnApi.Web.Controllers.Models
{
	/// <summary>
	/// Representation of a system with ruins in it
	/// </summary>
	public class MapsSystemDto
	{
		/// <summary>
		/// The id of the system
		/// </summary>
		public int SystemId { get; set; }
		/// <summary>
		/// The name of the system
		/// </summary>
		public string SystemName { get; set; }
		/// <summary>
		/// A link to this system in EDSM
		/// </summary>
		public string EdsmSystemLink { get; set; }
		/// <summary>
		/// A collection of all ruins within this system
		/// </summary>
		public MapsRuinDto[] Ruins { get; set; }
	}
}
