using System;
using System.ComponentModel.DataAnnotations;

namespace CanonnApi.Web.Controllers.Models
{
	/// <summary>
	/// Representation of a ruin site
	/// </summary>
	public class RuinSiteDto
	{
		/// <summary>
		/// The Id of the ruin site, equals the GS# designation
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// The id of the body this ruin site is located on
		/// </summary>
		[Required]
		public int BodyId { get; set; }
		/// <summary>
		/// The latitude where this ruin site can be found
		/// </summary>
		public decimal Latitude { get; set; }
		/// <summary>
		/// The longitude where this ruin site can be found ruin site
		/// </summary>
		public decimal Longitude { get; set; }

		/// <summary>
		/// The id of the ruin type this site is of
		/// </summary>
		public int? RuintypeId { get; set; }

		/// <summary>
		/// When the entry for this ruin site was created. Set by server
		/// </summary>
		public DateTime Created { get; set; }
		/// <summary>
		/// When the entry of this ruin site was updated the last time. Set by server
		/// </summary>
		public DateTime Updated { get; set; }

		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			RuinSiteDto other = obj as RuinSiteDto;

			if ((object) other == null)
				return false;

			return (other.Id == Id);
		}

		public bool Equals(RuinSiteDto other)
		{
			if ((object)other == null)
				return false;

			return (other.Id == Id);
		}
	}
}
