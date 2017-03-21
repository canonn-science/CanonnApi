using CanonnApi.Web.DatabaseModels;

namespace CanonnApi.Web.Models
{
	public class CodexDataDto : CodexData
	{
		public CodexDataDto() { }

		public CodexDataDto(CodexData data)
			: this()
		{
			Id = data.Id;
			CategoryId = data.CategoryId;
			Created = data.Created;
			EntryNumber = data.EntryNumber;
			Text = data.Text;
			Updated = data.Updated;

			CategoryName = data.Category?.Name;
		}

		public string CategoryName { get; set; }
	}
}
