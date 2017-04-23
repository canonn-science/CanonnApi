using System;
using System.ComponentModel.DataAnnotations;

namespace CanonnApi.Web.Controllers.Models
{
	/// <summary>
	/// Representation of an obelisk
	/// </summary>
	public class ObeliskDto
	{
		/// <summary>
		/// The Id of the obelisk
		/// </summary>
		public int Id { get; set; }
		/// <summary>
		/// The id of the obelisk group this obelisk is in
		/// </summary>
		[Required]
		public int ObeliskgroupId { get; set; }
		/// <summary>
		/// The number of this obelisk within the obelisk group
		/// </summary>
		[Required]
		public int Number { get; set; }
		/// <summary>
		/// Determines whether this obelisk is broken
		/// </summary>
		public bool IsBroken { get; set; }
		/// <summary>
		/// The id of the codex data entry, if this obelisk yields data
		/// </summary>
		public int? CodexdataId { get; set; }
		/// <summary>
		/// Determines whether the state of this obelisk is officially verified
		/// </summary>
		public bool IsVerified { get; set; }

		/// <summary>
		/// When the entry for this obelisk was created. Set by server
		/// </summary>
		public DateTime Created { get; set; }
		/// <summary>
		/// When the entry of this obelisk was updated the last time. Set by server
		/// </summary>
		public DateTime Updated { get; set; }
	}
}