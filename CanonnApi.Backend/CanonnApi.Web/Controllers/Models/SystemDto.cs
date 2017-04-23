using System;
using System.ComponentModel.DataAnnotations;

namespace CanonnApi.Web.Controllers.Models
{
	/// <summary>
	/// Representation of a system
	/// </summary>
	public class SystemDto
	{
		/// <summary>
		/// The Id of the system
		/// </summary>
		public int Id { get; set; }
		/// <summary>
		/// The name of the system
		/// </summary>
		[Required]
		public string Name { get; set; }
		/// <summary>
		/// The external id of this system in the EDSM database
		/// </summary>
		public int? EdsmExtId { get; set; }
		/// <summary>
		/// The external id of this system in the EDDB database
		/// </summary>
		public int? EddbExtId { get; set; }

		/// <summary>
		/// The X coordinate of this system in the galactic coordinate system
		/// </summary>
		public float? EdsmCoordX { get; set; }
		/// <summary>
		/// The Y coordinate of this system in the galactic coordinate system
		/// </summary>
		public float? EdsmCoordY { get; set; }
		/// <summary>
		/// The Z coordinate of this system in the galactic coordinate system
		/// </summary>
		public float? EdsmCoordZ { get; set; }
		/// <summary>
		/// When the EDSM coordinates for this system were updated the last time. Set by server
		/// </summary>
		public DateTime EdsmCoordUpdated { get; set; }

		/// <summary>
		/// When the entry for this system was created. Set by server
		/// </summary>
		public DateTime Created { get; set; }
		/// <summary>
		/// When the entry of this system was updated the last time. Set by server
		/// </summary>
		public DateTime Updated { get; set; }
	}
}
