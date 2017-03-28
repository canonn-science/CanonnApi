using System.Collections.Generic;
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
			: base (context)
		{
		}

		protected override DbSet<RuinSite> DbSet()
		{
			return RuinsContext.RuinSite;
		}

		protected override void MapValues(RuinSite source, RuinSite target)
		{
			target.BodyId = source.BodyId;
			target.Latitude = source.Latitude;
			target.Longitude = source.Longitude;
			target.RuintypeId = source.RuintypeId;
		}

		public async Task<List<ObeliskGroupWithActiveState>> LoadActiveObeliskGroupsForSite(int siteId)
		{
			return await RuinsContext.ObeliskGroup
				.Include(og => og.RuinsiteObeliskgroups)
				.Where(og => og.Ruintype.RuinSite.Any(rs => rs.Id == siteId))
				.Select(og => new ObeliskGroupWithActiveState(og))
				.ToListAsync();
		}

		public async Task<bool> SaveObeliskGroupsForSite(int siteId, ObeliskGroup[] obeliskGroups)
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

		public async Task<List<Obelisk>> LoadActiveObelisksForSite(int siteId)
		{
			return await RuinsContext.RuinsiteActiveobelisks
				.Include(e => e.Obelisk)
				.Where(e => e.RuinsiteId == siteId)
				.Select(e => e.Obelisk)
				.ToListAsync();
		}

		public async Task<bool> SaveActiveObelisksForSite(int siteId, Obelisk[] obelisks)
		{
			var currentActiveObelisks = await RuinsContext.RuinsiteActiveobelisks
				.Where(e => e.RuinsiteId == siteId)
				.ToListAsync();

			var obelisksToDelete = currentActiveObelisks
				.Where(g => obelisks.All(og => og.Id != g.Obelisk.Id));

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
	}
}
