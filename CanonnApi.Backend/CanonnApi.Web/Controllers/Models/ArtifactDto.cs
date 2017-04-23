using System;
using System.ComponentModel.DataAnnotations;

namespace CanonnApi.Web.Controllers.Models
{
	/// <summary>
	/// Representation of an artifact
	/// </summary>
	public class ArtifactDto
	{
		/// <summary>
		/// The Id of the artifact
		/// </summary>
		public int Id { get; set; }
		/// <summary>
		/// The name of the artifact
		/// </summary>
		[Required]
		public string Name { get; set; }
		/// <summary>
		/// When the entry for this artifact was created. Set by server
		/// </summary>
		public DateTime Created { get; set; }
		/// <summary>
		/// When the entry of this artifact was updated the last time. Set by server
		/// </summary>
		public DateTime Updated { get; set; }
	}
}
