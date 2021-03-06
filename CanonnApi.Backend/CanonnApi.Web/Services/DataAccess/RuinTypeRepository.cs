﻿using Microsoft.EntityFrameworkCore;
using CanonnApi.Web.DatabaseModels;

namespace CanonnApi.Web.Services.DataAccess
{
	public class RuinTypeRepository : BaseDataRepository<RuinType>, IRuinTypeRepository
	{
		public RuinTypeRepository(RuinsContext context)
			: base(context)
		{
		}

		protected override DbSet<RuinType> DbSet()
		{
			return RuinsContext.RuinType;
		}

		protected override void MapValues(RuinType source, RuinType target)
		{
			target.Name = source.Name;
		}
	}
}
