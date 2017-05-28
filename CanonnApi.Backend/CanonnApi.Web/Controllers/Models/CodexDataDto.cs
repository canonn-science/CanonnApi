using System;
using System.ComponentModel.DataAnnotations;

namespace CanonnApi.Web.Controllers.Models
{
	/// <summary>
	/// Representation of an codex data entry
	/// </summary>
	public class CodexDataDto
	{
		/// <summary>
		/// The Id of the codex data entry
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// The id of the category this codex data entry belongs to
		/// </summary>
		[Required]
		public int CategoryId { get; set; }
		/// <summary>
		/// The id of the secondary artifact used to unlock this codex data entry
		/// </summary>
		public int? ArtifactId { get; set; }
		/// <summary>
		/// The number of this codex data entry within the category
		/// </summary>
		[Required]
		public int EntryNumber { get; set; }
		/// <summary>
		/// The actual codex data entry text
		/// </summary>
		[Required]
		public string Text { get; set; }

		/// <summary>
		/// When the entry for this codex entry was created. Set by server
		/// </summary>
		public DateTime Created { get; set; }
		/// <summary>
		/// When the entry of this codex entry was updated the last time. Set by server
		/// </summary>
		public DateTime Updated { get; set; }
	}
}