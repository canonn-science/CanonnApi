using System;
using System.ComponentModel.DataAnnotations;

namespace CanonnApi.Web.Controllers.Models
{
	/// <summary>
	/// Representation of a ruin type
	/// </summary>
	public class RuinTypeDto
	{
		/// <summary>
		/// The Id of the ruin type
		/// </summary>
		public int Id { get; set; }
		/// <summary>
		/// The name of the ruin type
		/// </summary>
		[Required]
		public string Name { get; set; }
		/// <summary>
		/// When the entry for this ruin type was created. Set by server
		/// </summary>
		public DateTime Created { get; set; }
		/// <summary>
		/// When the entry of this ruin type was updated the last time. Set by server
		/// </summary>
		public DateTime Updated { get; set; }
	}
}