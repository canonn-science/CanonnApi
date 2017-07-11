using System;
using System.ComponentModel.DataAnnotations;

namespace CanonnApi.Web.Controllers.Models
{
	/// <summary>
	/// Representation of a location type
	/// </summary>
	public class LocationTypeDto
	{
		/// <summary>
		/// The Id of the location type
		/// </summary>
		public int Id { get; set; }
		/// <summary>
		/// The name of the location type
		/// </summary>
		[Required]
		public string Name { get; set; }
		/// <summary>
		/// The short name of the location type
		/// </summary>
		[Required]
		public string ShortName { get; set; }
		/// <summary>
		/// Determines whether the location type is surface bound or not
		/// </summary>
		[Required]
		public bool IsSurface { get; set; }
		/// <summary>
		/// When the entry for this location type was created. Set by server
		/// </summary>
		public DateTime Created { get; set; }
		/// <summary>
		/// When the entry of this location type was updated the last time. Set by server
		/// </summary>
		public DateTime Updated { get; set; }
	}
}
