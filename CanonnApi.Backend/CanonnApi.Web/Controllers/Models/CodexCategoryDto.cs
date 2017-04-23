using System;
using System.ComponentModel.DataAnnotations;

namespace CanonnApi.Web.Controllers.Models
{
	/// <summary>
	/// Representation of a codex category
	/// </summary>
	public class CodexCategoryDto
	{
		/// <summary>
		/// The Id of the codex category
		/// </summary>
		public int Id { get; set; }
		/// <summary>
		/// The name of the codex category
		/// </summary>
		[Required]
		public string Name { get; set; }
		/// <summary>
		/// The id of the artifact used to unlock this codex category
		/// </summary>
		[Required]
		public int ArtifactId { get; set; }
		/// <summary>
		/// When the entry for this codex category was created. Set by server
		/// </summary>
		public DateTime Created { get; set; }
		/// <summary>
		/// When the entry of this codex category was updated the last time. Set by server
		/// </summary>
		public DateTime Updated { get; set; }
	}
}
