using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RuinsApi.DatabaseModels;

namespace RuinsApi.Services.DataAccess
{
	public class RuinRepository : IRuinRepository
	{ 
		private readonly RuinsContext _ruinsContext;

		public RuinRepository(RuinsContext context)
		{
			if (context == null)
				throw new ArgumentNullException(nameof(context));

			_ruinsContext = context;
		}

		public async Task<List<RuinType>> GetAllRuinTypes()
		{
			return await _ruinsContext.RuinType.ToListAsync();
		}

		public async Task<RuinType> GetRuinTypeById(int id)
		{
			return await _ruinsContext.RuinType.FirstOrDefaultAsync(r => r.Id == id);
		}

		public async Task<RuinType> CreateOrUpdateRuinTypeById(int id, RuinType value)
		{
			if (_ruinsContext.RuinType.Any(r => r.Id == id))
			{
				return await UpdateRuinType(id, value);
			}
			else
			{
				return await CreateRuinTypeWithId(id, value);
			}
		}

		public async Task<RuinType> CreateRuinType(RuinType value)
		{
			var entry = new RuinType()
			{
				Name = value.Name,
			};

			_ruinsContext.RuinType.Add(entry);
			await _ruinsContext.SaveChangesAsync();

			return entry;
		}

		private async Task<RuinType> CreateRuinTypeWithId(int id, RuinType value)
		{
			var entry = new RuinType()
			{
				Id = id,
				Name = value.Name,
			};

			_ruinsContext.RuinType.Add(entry);
			await _ruinsContext.SaveChangesAsync();

			return entry;
		}

		public async Task<RuinType> UpdateRuinType(int id, RuinType value)
		{
			var entry = new RuinType()
			{
				Id = id,
				Name = value.Name,
			};

			_ruinsContext.RuinType.Update(entry);
			await _ruinsContext.SaveChangesAsync();
			return entry;
		}

		public async Task<bool> DeleteRuinTypeById(int id)
		{
			// delete by stub
			_ruinsContext.RuinType.Remove(new RuinType() { Id = id });
			try
			{
				await _ruinsContext.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				return false;
			}

			return true;
		}
	}
}
