using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CanonnApi.Web.DatabaseModels;
using CanonnApi.Web.Middlewares;

namespace CanonnApi.Web.Services.DataAccess
{
	public abstract class BaseDataRepository<T> : IBaseDataRepository<T>
		 where T : class, IEntity, new()
	{
		protected readonly RuinsContext RuinsContext;

		protected BaseDataRepository(RuinsContext context)
		{
			RuinsContext = context ?? throw new ArgumentNullException(nameof(context));
		}

		protected abstract DbSet<T> DbSet();

		protected virtual T CreateEntity()
		{
			return new T();
		}

		protected abstract void MapValues(T source, T target);

		public async Task<List<T>> GetAll()
		{
			return await DbSet().ToListAsync();
		}

		public async Task<T> GetById(int id)
		{
			return await DbSet().FirstOrDefaultAsync(r => r.Id == id);
		}

		public async Task<T> CreateOrUpdateById(int id, T value)
		{
			if (DbSet().Any(r => r.Id == id))
			{
				return await Update(id, value);
			}
			else
			{
				return await CreateWithId(id, value);
			}
		}

		public async Task<T> Create(T value)
		{
			T entry = CreateEntity();
			MapValues(value, entry);

			DbSet().Add(entry);
			await RuinsContext.SaveChangesAsync();

			return entry;
		}

		private async Task<T> CreateWithId(int id, T value)
		{
			T entry = CreateEntity();
			entry.Id = id;

			MapValues(value, entry);

			DbSet().Add(entry);
			await RuinsContext.SaveChangesAsync();

			return entry;
		}

		public async Task<T> Update(int id, T value)
		{
			T entry = await GetById(id);
			if (entry == null)
				throw new HttpNotFoundException();

			MapValues(value, entry);

			await RuinsContext.SaveChangesAsync();

			return entry;
		}

		public virtual async Task<bool> DeleteById(int id)
		{
			// delete by stub
			DbSet().Remove(new T() { Id = id });

			try
			{
				await RuinsContext.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				return false;
			}

			return true;
		}

		public virtual async Task<int> SaveChanges()
		{
			return await RuinsContext.SaveChangesAsync();
		}
	}
}
