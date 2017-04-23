namespace CanonnApi.Web.Controllers.Models
{
	/// <summary>
	/// Represents a body instance with update info
	/// </summary>
	public class EdsmUpdatedSystem: SystemDto
	{
		/// <summary>
		/// Determines whether this instance was updated
		/// </summary>
		public bool IsUpdated { get; set; }
		/// <summary>
		/// When not updated, the encountered error message
		/// </summary>
		public string ErrorMessage { get; set; }
	}
}