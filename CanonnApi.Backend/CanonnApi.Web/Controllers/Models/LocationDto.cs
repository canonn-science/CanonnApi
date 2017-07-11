using System;
using System.ComponentModel.DataAnnotations;

namespace CanonnApi.Web.Controllers.Models
{
	/// <summary>
	/// Representation of a location
	/// </summary>
	public class LocationDto
	{
		/// <summary>
		/// The Id of the location type
		/// </summary>
		public int Id { get; set; }
		/// <summary>
		/// If of the location type
		/// </summary>
		public int LocationtypeId { get; set; }
		/// <summary>
		/// The Id of a body this location is related to
		/// </summary>
		public int? BodyId { get; set; }
		/// <summary>
		/// The system to target when at the body
		/// </summary>
		public int? DirectionSystemId { get; set; }
		/// <summary>
		/// The distance to fly from the body to the direction system
		/// </summary>
		public int? Distance { get; set; }
		/// <summary>
		/// If this location should be visible on reports
		/// </summary>
		public bool IsVisible { get; set; }
		/// <summary>
		/// If the location is on surface, the latitude
		/// </summary>
		public decimal? Latitude { get; set; }
		/// <summary>
		/// If the location is on surface, the longitude
		/// </summary>
		public decimal? Longitude { get; set; }
		/// <summary>
		/// The Id of the system this location is in
		/// </summary>
		public int SystemId { get; set; }
		
		/// <summary>
		/// When the entry for this location was created. Set by server
		/// </summary>
		public DateTime Created { get; set; }
		/// <summary>
		/// When the entry of this location was updated the last time. Set by server
		/// </summary>
		public DateTime Updated { get; set; }
	}
}
