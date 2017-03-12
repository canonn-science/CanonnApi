using System;

namespace RuinsApi.DatabaseModels
{
	public interface IEntity
	{
		int Id { get; set; }
		DateTime Created { get; set; }
		DateTime Updated { get; set; }
	}
}
