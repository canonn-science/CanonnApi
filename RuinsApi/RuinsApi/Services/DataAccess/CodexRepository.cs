using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RuinsApi.DatabaseModels;
using RuinsApi.Models;

namespace RuinsApi.Services.DataAccess
{
	public class CodexRepository : ICodexRepository
	{
		private readonly RuinsContext _ruinsContext;

		public CodexRepository(RuinsContext context)
		{
			if (context == null)
				throw new ArgumentNullException(nameof(context));

			_ruinsContext = context;
		}

		public async Task<List<CodexData>> GetAllData()
		{
			return await GetAllData(false);
		}

		public async Task<List<CodexData>> GetAllData(bool withCategoryName)
		{
			IQueryable<CodexData> query = _ruinsContext.CodexData.AsNoTracking();

			if (withCategoryName)
			{
				query = query.Include(data => data.Category);
			}

			var resultList = await query.ToListAsync();

			if (withCategoryName)
			{
				resultList = new List<CodexData>(resultList.Select(data => new CodexDataDto(data)));
			}

			return resultList;
		}

		public async Task<CodexData> GetDataById(int id)
		{
			return await _ruinsContext.CodexData.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);
		}

		public async Task<CodexData> CreateOrUpdateData(int id, CodexData value)
		{
			if (_ruinsContext.Relict.AsNoTracking().Any(r => r.Id == id))
			{
				return await UpdateData(id, value);
			}
			else
			{
				return await CreateDataWithId(id, value);
			}
		}

		public async Task<CodexData> CreateData(CodexData value)
		{
			var entry = new CodexData()
			{
				CategoryId = value.CategoryId,
				EntryNumber = value.EntryNumber,
				Text = value.Text,
			};

			_ruinsContext.CodexData.Add(entry);
			await _ruinsContext.SaveChangesAsync();

			return entry;
		}

		private async Task<CodexData> CreateDataWithId(int id, CodexData value)
		{
			var entry = new CodexData()
			{
				Id = id,
				CategoryId = value.CategoryId,
				EntryNumber = value.EntryNumber,
				Text = value.Text,
			};

			_ruinsContext.CodexData.Add(entry);
			await _ruinsContext.SaveChangesAsync();

			return entry;
		}

		public async Task<CodexData> UpdateData(int id, CodexData value)
		{
			var entry = new CodexData()
			{
				Id = id,
				CategoryId = value.CategoryId,
				EntryNumber = value.EntryNumber,
				Text = value.Text,
			};

			_ruinsContext.CodexData.Update(entry);
			await _ruinsContext.SaveChangesAsync();
			return entry;
		}

		public async Task<bool> DeleteDataById(int id)
		{
			// delete by stub
			_ruinsContext.CodexData.Remove(new CodexData() { Id = id });
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

		public async Task<List<CodexCategory>> GetAllCategories()
		{
			return await _ruinsContext.CodexCategory.AsNoTracking().ToListAsync();
		}

		public async Task<CodexCategory> GetCategoryById(int id)
		{
			return await _ruinsContext.CodexCategory.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);
		}

		public async Task<CodexCategory> CreateOrUpdateCategory(int id, CodexCategory value)
		{
			if (_ruinsContext.Relict.AsNoTracking().Any(r => r.Id == id))
			{
				return await UpdateCategory(id, value);
			}
			else
			{
				return await CreateCategoryWithId(id, value);
			}
		}

		public async Task<CodexCategory> CreateCategory(CodexCategory value)
		{
			var entry = new CodexCategory()
			{
				Name = value.Name,
				PrimaryRelict = value.PrimaryRelict,
			};

			_ruinsContext.CodexCategory.Add(entry);
			await _ruinsContext.SaveChangesAsync();

			return entry;
		}

		private async Task<CodexCategory> CreateCategoryWithId(int id, CodexCategory value)
		{
			var entry = new CodexCategory()
			{
				Id = id,
				Name = value.Name,
				PrimaryRelict = value.PrimaryRelict,
			};

			_ruinsContext.CodexCategory.Add(entry);
			await _ruinsContext.SaveChangesAsync();

			return entry;
		}
		public async Task<CodexCategory> UpdateCategory(int id, CodexCategory value)
		{
			var entry = new CodexCategory()
			{
				Id = id,
				Name = value.Name,
				PrimaryRelict = value.PrimaryRelict,
			};

			_ruinsContext.CodexCategory.Update(entry);
			await _ruinsContext.SaveChangesAsync();

			return entry;
		}

		public async Task<bool> DeleteCategoryById(int id)
		{
			// delete by stub
			_ruinsContext.CodexCategory.Remove(new CodexCategory() { Id = id });
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
