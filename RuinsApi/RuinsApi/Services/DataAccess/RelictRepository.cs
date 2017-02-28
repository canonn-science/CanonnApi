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

		public async Task<Relict> CreateRelict(Relict relictData)
		{
			// create new entry
			var relict = new Relict()
			{
				Name = relictData.Name,
			};

			_ruinsContext.Relict.Add(relict);
			await _ruinsContext.SaveChangesAsync();

			return relict;
		}

		private async Task<Relict> CreateRelictWithId(int id, Relict relictData)
		{
			// create new entry
			var relict = new Relict()
			{
				Id = id,
				Name = relictData.Name,
			};

			_ruinsContext.Relict.Add(relict);
			await _ruinsContext.SaveChangesAsync();

			return relict;
		}

		public async Task<Relict> UpdateRelict(int id, Relict relictData)
		{
			// Update existing entry
			var relict = new Relict()
			{
				Id = id,
				Name = relictData.Name,
			};

			_ruinsContext.Relict.Update(relict);
			await _ruinsContext.SaveChangesAsync();
			return relict;
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

		public async Task<Relict> CreateOrUpdateRelictById(int id, Relict relictData)
		{
			if (_ruinsContext.Relict.Any(r => r.Id == id))
			{
				// update
				return await UpdateRelict(id, relictData);
			}
			else
			{
				// add
				return await CreateRelictWithId(id,relictData);
			}
		}
	}
}
