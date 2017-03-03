using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RuinsApi.DatabaseModels;

namespace RuinsApi.Services.DataAccess
{
	public class RelictRepository : IRelictRepository
	{ 
		private readonly RuinsContext _ruinsContext;

		public RelictRepository(RuinsContext context)
		{
			if (context == null)
				throw new ArgumentNullException(nameof(context));

			_ruinsContext = context;
		}

		public async Task<List<Relict>> GetAllRelicts()
		{
			return await _ruinsContext.Relict.ToListAsync();
		}

		public async Task<Relict> GetRelictById(int id)
		{
			return await _ruinsContext.Relict.FirstOrDefaultAsync(r => r.Id == id);
		}

		public async Task<Relict> CreateOrUpdateRelictById(int id, Relict value)
		{
			if (_ruinsContext.Relict.Any(r => r.Id == id))
			{
				return await UpdateRelict(id, value);
			}
			else
			{
				return await CreateRelictWithId(id, value);
			}
		}

		public async Task<Relict> CreateRelict(Relict value)
		{
			var entry = new Relict()
			{
				Name = value.Name,
			};

			_ruinsContext.Relict.Add(entry);
			await _ruinsContext.SaveChangesAsync();

			return entry;
		}

		private async Task<Relict> CreateRelictWithId(int id, Relict value)
		{
			var entry = new Relict()
			{
				Id = id,
				Name = value.Name,
			};

			_ruinsContext.Relict.Add(entry);
			await _ruinsContext.SaveChangesAsync();

			return entry;
		}

		public async Task<Relict> UpdateRelict(int id, Relict value)
		{
			var entry = new Relict()
			{
				Id = id,
				Name = value.Name,
			};

			_ruinsContext.Relict.Update(entry);
			await _ruinsContext.SaveChangesAsync();
			return entry;
		}

		public async Task<bool> DeleteRelictById(int id)
		{
			// delete by stub
			_ruinsContext.Relict.Remove(new Relict() { Id = id });
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
