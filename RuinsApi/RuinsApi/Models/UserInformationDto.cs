using Newtonsoft.Json;

namespace RuinsApi.Models
{
	public class UserInformationDto
	{
		public string Email { get; set; }
		public string Name { get; set; }
		[JsonProperty("given_name")]
		public string GivenName { get; set; }
		[JsonProperty("family_name")]
		public string FamilyName { get; set; }
		public string Picture { get; set; }
		public string Gender { get; set; }
		public string Locale { get; set; }
		public string Nickname{ get; set; }
		public string[] Groups { get; set; }
		public string[] Roles { get; set; }
		public string[] Permissions { get; set; }
		[JsonProperty("email_verified")]
		public bool EmailVerified { get; set; }
		[JsonProperty("updated_at")]
		public string UpdatedAt { get; set; }
		public IdentitiesDto[] Identities { get; set; }
		[JsonProperty("created_at")]
		public string CreatedAt{ get; set; }
	}
}
