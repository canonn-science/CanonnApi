using System.Collections.Generic;

namespace CanonnApi.Web.Services.Maps
{
	public class ScanDataDto
	{
		public string Scan { get; set; }
		public bool IsVerified { get; set; }
		public bool IsBroken { get; set; }
		public List<string> Items { get; set; } = new List<string>();
	}
}
