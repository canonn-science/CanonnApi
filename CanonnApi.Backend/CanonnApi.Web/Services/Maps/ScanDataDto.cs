using System.Collections.Generic;

namespace CanonnApi.Web.Services.Maps
{
	public class ScanDataDto
	{
		public string Scan { get; set; }
		public List<string> Items { get; set; } = new List<string>();
	}
}
