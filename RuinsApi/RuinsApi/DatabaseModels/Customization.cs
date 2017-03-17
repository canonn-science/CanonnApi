using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RuinsApi.DatabaseModels
{
	public partial class RuinsContext
	{
		public RuinsContext(DbContextOptions<RuinsContext> options)
			: base(options)
		{ }

		

		private void AmendModel(ModelBuilder modelBuilder)
		{
			// make all Created and Updated properties readonly
			modelBuilder.Entity<Artifact>(etb => MarkReadonly(etb.Property(e => e.Created), etb.Property(e => e.Updated)));
			modelBuilder.Entity<CodexCategory>(etb => MarkReadonly(etb.Property(e => e.Created), etb.Property(e => e.Updated)));
			modelBuilder.Entity<CodexData>(etb => MarkReadonly(etb.Property(e => e.Created), etb.Property(e => e.Updated)));
			modelBuilder.Entity<ObeliskGroup>(etb => MarkReadonly(etb.Property(e => e.Created), etb.Property(e => e.Updated)));
			modelBuilder.Entity<RuinType>(etb => MarkReadonly(etb.Property(e => e.Created), etb.Property(e => e.Updated)));
			modelBuilder.Entity<System>(etb => MarkReadonly(etb.Property(e => e.Created), etb.Property(e => e.Updated)));
			modelBuilder.Entity<Body>(etb => MarkReadonly(etb.Property(e => e.Created), etb.Property(e => e.Updated)));
			modelBuilder.Entity<Obelisk>(etb => MarkReadonly(etb.Property(e => e.Created), etb.Property(e => e.Updated)));
			modelBuilder.Entity<RuinSite>(etb => MarkReadonly(etb.Property(e => e.Created), etb.Property(e => e.Updated)));
		}

		private void MarkReadonly(PropertyBuilder<DateTime> created, PropertyBuilder<DateTime> updated)
		{
			MarkReadonly(created.Metadata);
			MarkReadonly(updated.Metadata);
		}

		private void MarkReadonly(IMutableProperty metadata)
		{
			metadata.IsReadOnlyAfterSave = true;
			metadata.IsReadOnlyBeforeSave = true;
		}
	}

	public partial class Artifact: IEntity { }
	public partial class CodexCategory: IEntity { }
	public partial class CodexData: IEntity { }
	public partial class ObeliskGroup: IEntity { }
	public partial class RuinType: IEntity { }
	public partial class System: IEntity { }
	public partial class Body: IEntity { }
	public partial class Obelisk: IEntity { }
	public partial class RuinSite: IEntity { }
}
