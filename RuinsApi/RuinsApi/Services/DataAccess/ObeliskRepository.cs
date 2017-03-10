using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RuinsApi.DatabaseModels;

namespace RuinsApi.Services.DataAccess
{
	public class ObeliskRepository : IObeliskRepository
	{ 
		private readonly RuinsContext _ruinsContext;

		public ObeliskRepository(RuinsContext context)
		{
			if (context == null)
				throw new ArgumentNullException(nameof(context));

			_ruinsContext = context;
		}

		public async Task<List<ObeliskGroup>> GetAllObeliskGroups()
		{
			return await _ruinsContext.ObeliskGroup.ToListAsync();
		}

		public async Task<ObeliskGroup> GetObeliskGroupById(int id)
		{
			return await _ruinsContext.ObeliskGroup.FirstOrDefaultAsync(r => r.Id == id);
		}

		public async Task<ObeliskGroup> CreateOrUpdateObeliskGroupById(int id, ObeliskGroup value)
		{
			if (_ruinsContext.ObeliskGroup.Any(r => r.Id == id))
			{
				return await UpdateObeliskGroup(id, value);
			}
			else
			{
				return await CreateObeliskGroupWithId(id, value);
			}
		}

		public async Task<ObeliskGroup> CreateObeliskGroup(ObeliskGroup value)
		{
			var entry = new ObeliskGroup()
			{
				Name = value.Name,
				TypeId = value.TypeId,
				Count = value.Count,
			};

			_ruinsContext.ObeliskGroup.Add(entry);
			await _ruinsContext.SaveChangesAsync();

			return entry;
		}

		private async Task<ObeliskGroup> CreateObeliskGroupWithId(int id, ObeliskGroup value)
		{
			var entry = new ObeliskGroup()
			{
				Id = id,
				Name = value.Name,
				TypeId = value.TypeId,
				Count = value.Count,
			};

			_ruinsContext.ObeliskGroup.Add(entry);
			await _ruinsContext.SaveChangesAsync();

			return entry;
		}

		public async Task<ObeliskGroup> UpdateObeliskGroup(int id, ObeliskGroup value)
		{
			var entry = new ObeliskGroup()
			{
				Id = id,
				Name = value.Name,
				TypeId = value.TypeId,
				Count = value.Count,
			};

			_ruinsContext.ObeliskGroup.Update(entry);
			await _ruinsContext.SaveChangesAsync();
			return entry;
		}

		public async Task<bool> DeleteObeliskGroupById(int id)
		{
			// delete by stub
			_ruinsContext.ObeliskGroup.Remove(new ObeliskGroup() { Id = id });
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
