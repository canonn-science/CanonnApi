using System;
using System.ComponentModel.DataAnnotations;

namespace CanonnApi.Web.Controllers.Models
{
	/// <summary>
	/// Representation of a body
	/// </summary>
	public class BodyDto
	{
		/// <summary>
		/// The Id of the body
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// The name of the body
		/// </summary>
		[Required]
		public string Name { get; set; }
		/// <summary>
		/// The id of the system this body is in
		/// </summary>
		[Required]
		public int SystemId { get; set; }

		/// <summary>
		/// The distance of this body from the system entry point
		/// </summary>
		public int? Distance { get; set; }
		/// <summary>
		/// The external id of this body in the EDDB database
		/// </summary>
		public int? EddbExtId { get; set; }
		/// <summary>
		/// The external id of this body in the EDSM database
		/// </summary>
		public int? EdsmExtId { get; set; }

		/// <summary>
		/// When the entry for this body was created. Set by server
		/// </summary>
		public DateTime Created { get; set; }
		/// <summary>
		/// When the entry of this body was updated the last time. Set by server
		/// </summary>
		public DateTime Updated { get; set; }
	}
}
