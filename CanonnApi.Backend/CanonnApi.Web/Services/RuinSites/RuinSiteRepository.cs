using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using CanonnApi.Web.DatabaseModels;
using CanonnApi.Web.Services.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace CanonnApi.Web.Services.RuinSites
{
	public class RuinSiteRepository : BaseDataRepository<RuinSite>, IRuinSiteRepository
	{
		public RuinSiteRepository(RuinsContext context)
			: base(context)
		{
		}

		protected override DbSet<RuinSite> DbSet()
		{
			return RuinsContext.RuinSite;
		}

		protected override void MapValues(RuinSite source, RuinSite target)
		{
			target.LocationId = source.LocationId;
			target.RuintypeId = source.RuintypeId;
		}

		public async Task<RuinSiteWithObeliskData> GetForSiteEditor(int siteId)
		{
			var siteTask = RuinsContext
				.RuinSite
				.AsNoTracking()
				.Include(rs => rs.Location.Body)
				.Include(rs => rs.Location.System)
				.SingleAsync(s => s.Id == siteId);

			var obeliskGroupTask = RuinsContext
				.RuinSite
				.AsNoTracking()
				.Where(rs => rs.Id == siteId)
				.SelectMany(rs => rs.Ruintype.ObeliskGroup)
				.ToListAsync().ConfigureAwait(false);

			var obeliskTasks = RuinsContext
				.RuinSite
				.AsNoTracking()
				.Where(rs => rs.Id == siteId)
				.SelectMany(rs => rs.Ruintype.ObeliskGroup)
				.SelectMany(og => og.Obelisk)
				.ToListAsync().ConfigureAwait(false);

			var availableObeliskGroupsTask = RuinsContext
				.RuinsiteObeliskgroups
				.AsNoTracking()
				.Where(rsao => rsao.RuinsiteId == siteId)
				.Select(rsao => rsao.ObeliskgroupId)
				.ToListAsync().ConfigureAwait(false);

			var activeObeliskTask = RuinsContext
				.RuinsiteActiveobelisks
				.AsNoTracking()
				.Where(rsao => rsao.RuinsiteId == siteId)
				.Select(rsao => rsao.ObeliskId)
				.ToListAsync().ConfigureAwait(false);

			var result = new RuinSiteWithObeliskData(await siteTask)
			{
				Location = null, // prevent cyclic reference in serializable data, and we don't need the actual location instance here
			};

			// prevent cyclic reference in data:
			var availableGroups = new HashSet<int>(await availableObeliskGroupsTask);
			result.ObeliskGroups.AddRange((await obeliskGroupTask).Select(og => new ObeliskGroupWithActiveState(og) { Active = availableGroups.Contains(og.Id) }));

			var activeObelisks = new HashSet<int>(await activeObeliskTask);
			result.Obelisks.AddRange((await obeliskTasks).Select(o => new ObeliskWithActiveState(o) { Active = activeObelisks.Contains(o.Id) }));

			return result;
		}

		public async Task<RuinSiteWithObeliskData> SaveFromEditor(RuinSiteWithObeliskData data)
		{
			// need to serialize.. sadly
			await CreateOrUpdateById(data.Id, data);
			await SaveObeliskGroupsForSite(data.Id, data.ObeliskGroups.Where(og => og.Active));

			var activeGroups = data.ObeliskGroups.Select(g => g.Id).ToImmutableHashSet();
			await SaveActiveObelisksForSite(data.Id, data.Obelisks.Where(o => o.Active && activeGroups.Contains(o.ObeliskgroupId)));
			return await GetForSiteEditor(data.Id);
		}

		public async Task<List<ObeliskGroupWithActiveState>> LoadActiveObeliskGroupsForSite(int siteId)
		{
			return await RuinsContext.ObeliskGroup
				.Include(og => og.RuinsiteObeliskgroups)
				.Where(og => og.Ruintype.RuinSite.Any(rs => rs.Id == siteId))
				.Select(og => new ObeliskGroupWithActiveState(og))
				.ToListAsync();
		}

		public async Task<List<Obelisk>> LoadActiveObelisksForSite(int siteId)
		{
			return await RuinsContext.RuinsiteActiveobelisks
				.Include(e => e.Obelisk)
				.Where(e => e.RuinsiteId == siteId)
				.Select(e => e.Obelisk)
				.ToListAsync();
		}

		public async Task<bool> SaveObeliskGroupsForSite(int siteId, IEnumerable<ObeliskGroup> obeliskGroups)
		{
			var currentGroups = await RuinsContext.RuinsiteObeliskgroups
				.Include(e => e.Obeliskgroup)
				.Where(e => e.RuinsiteId == siteId)
				.ToListAsync();

			var groupsToDelete = currentGroups
				.Where(g => !obeliskGroups.Any(og => og.Id == g.Obeliskgroup.Id || og.Name == g.Obeliskgroup.Name));

			var groupsToAdd = obeliskGroups
				.Where(og => !currentGroups.Any(g => g.Obeliskgroup.Id == og.Id || g.Obeliskgroup.Name == og.Name));

			// first delete all groups
			foreach (var group in groupsToDelete)
			{
				RuinsContext.RuinsiteObeliskgroups.Remove(group);
			}

			// add all groups where we know the id of
			foreach (var group in groupsToAdd.Where(g => g.Id > 0))
			{
				RuinsContext.RuinsiteObeliskgroups.Add(new RuinsiteObeliskgroups() { RuinsiteId = siteId, ObeliskgroupId = group.Id });
			}

			var groupsToAddWithoutId = groupsToAdd.Where(g => g.Id == 0).Select(g => g.Name).ToArray();
			if (groupsToAddWithoutId.Length > 0)
			{
				// for the groups only given by name (not id), we need to fetch the entities first
				var allGroups = await RuinsContext.ObeliskGroup
					.Where(g => g.Ruintype.RuinSite.Any(s => s.Id == siteId))
					.ToListAsync();

				foreach (var group in allGroups.Where(g => groupsToAddWithoutId.Contains(g.Name)))
				{
					RuinsContext.RuinsiteObeliskgroups.Add(new RuinsiteObeliskgroups() { RuinsiteId = siteId, ObeliskgroupId = group.Id });
				}
			}

			await RuinsContext.SaveChangesAsync();

			return true;
		}

		public async Task<bool> SaveActiveObelisksForSite(int siteId, IEnumerable<Obelisk> obelisks)
		{
			var currentActiveObelisks = await RuinsContext.RuinsiteActiveobelisks
				.Where(e => e.RuinsiteId == siteId)
				.ToListAsync();

			var obelisksToDelete = currentActiveObelisks
				.Where(g => obelisks.All(og => og.Id != g.ObeliskId));

			var obelisksToAdd = obelisks
				.Where(og => currentActiveObelisks.All(g => g.ObeliskId != og.Id));

			// first delete all groups
			foreach (var obelisk in obelisksToDelete)
			{
				RuinsContext.RuinsiteActiveobelisks.Remove(obelisk);
			}

			// add all groups where we know the id of
			foreach (var obelisk in obelisksToAdd)
			{
				RuinsContext.RuinsiteActiveobelisks.Add(new RuinsiteActiveobelisks() { RuinsiteId = siteId, ObeliskId = obelisk.Id });
			}

			await RuinsContext.SaveChangesAsync();

			return true;
		}

		public async Task<List<RuinSite>> SearchSitesForData(string categoryName, int entryNumber)
		{
			var siteIds = (IEnumerable<int>)await RuinsContext.Obelisk
				.Where(o => o.Codexdata.Category.Name == categoryName && o.Codexdata.EntryNumber == entryNumber)
				.SelectMany(o => o.RuinsiteActiveobelisks)
				.Select(rsao => rsao.RuinsiteId)
				.ToArrayAsync();

			siteIds = siteIds
				.Where(id => id < 99997) // exclude reference sites
				.ToImmutableHashSet();

			return await RuinsContext.RuinSite
				.Include(site => site.Location.Body)
				.Include(site => site.Location.System)
				.Include(site => site.Ruintype)
				.Where(site => siteIds.Contains(site.Id))
				.ToListAsync();
		}

		public override Task<bool> DeleteById(int id)
		{
			RuinsContext.RemoveRange(RuinsContext.RuinsiteObeliskgroups.Where(rsog => rsog.RuinsiteId == id));
			RuinsContext.RemoveRange(RuinsContext.RuinsiteActiveobelisks.Where(rsao => rsao.RuinsiteId == id));

			return base.DeleteById(id);
		}
	}
}
