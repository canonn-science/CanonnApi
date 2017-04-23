using System;
using System.ComponentModel.DataAnnotations;

namespace CanonnApi.Web.Controllers.Models
{
	/// <summary>
	/// Represents an obelisk group
	/// </summary>
	public class ObeliskGroupDto
	{
		/// <summary>
		/// The Id of the obelisk group
		/// </summary>
		public int Id { get; set; }
		/// <summary>
		/// The number of obelisks that belong to this group
		/// </summary>
		[Required]
		public int Count { get; set; }
		/// <summary>
		/// The name of this obelisk group
		/// </summary>
		[Required]
		public string Name{ get; set; }
		/// <summary>
		/// The id of the ruin type this obelisk group belongs to
		/// </summary>
		public int RuintypeId { get; set; }

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