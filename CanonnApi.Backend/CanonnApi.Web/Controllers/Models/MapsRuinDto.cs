namespace CanonnApi.Web.Controllers.Models
{
	/// <summary>
	/// Representation of a ruin for the interactive maps api
	/// </summary>
	public class MapsRuinDto
	{
		/// <summary>
		/// The id of the ruin, also known as the GS# designation
		/// </summary>
		public int RuinId { get; set; }
		/// <summary>
		/// The name of the body this ruin is located on
		/// </summary>
		public string BodyName { get; set; }
		/// <summary>
		/// The name of the ruin type
		/// </summary>
		public string RuinTypeName { get; set; }
		/// <summary>
		/// The coordinates of this ruin on the body
		/// </summary>
		public decimal[] Coordinates { get; set; }
		/// <summary>
		/// A link to this body in EDSM
		/// </summary>
		public string EdsmBodyLink { get; set; }
	}
}
