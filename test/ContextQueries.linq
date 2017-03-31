<Query Kind="Program">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <NuGetReference>Pomelo.EntityFrameworkCore.MySql</NuGetReference>
  <Namespace>Microsoft.EntityFrameworkCore</Namespace>
  <Namespace>Microsoft.EntityFrameworkCore.Metadata</Namespace>
  <Namespace>Microsoft.EntityFrameworkCore.Metadata.Builders</Namespace>
  <Namespace>Newtonsoft.Json</Namespace>
</Query>

void Main()
{
	var builder = new DbContextOptionsBuilder<RuinsContext>();
	builder.UseMySql("server=localhost;database=ruinsdb;user id=ruinsdb;password=ruinsdb");
	var ctx = new RuinsContext(builder.Options);
	
	int siteId = 1;

	ctx.RuinSite
/*
		.Include(rs => rs.Ruintype.ObeliskGroup)
			.ThenInclude(og => og.Obelisk)
		.Include(rs => rs.RuinsiteActiveobelisks)
		.Include(rs => rs.RuinsiteActiveobelisks)
*/
		.Include(rs => rs.RuinsiteObeliskgroups)
		.Where(rs => rs.Id == siteId)
		.Select(rs => new {
			rs.Id,
			rs.BodyId,
			rs.Longitude,
			rs.Latitude,
	
			SelectedSystem = rs.Body.SystemId,
			SelectedBody = rs.BodyId,
			RuintypeId = rs.RuintypeId,
			
			// Ruintype = rs.Ruintype,

			ObeliskGroups = rs.Ruintype.ObeliskGroup.Select(og => new {
				og.Id,
				og.Name,
				og.Count,
				Active = og.RuinsiteObeliskgroups.Select(rsog => rsog.Ruinsite).Contains(rs),
			}),
			Obelisks = rs.Ruintype.ObeliskGroup.SelectMany(og => og.Obelisk).Select(o => new
			{
				o.Id,
				hasData = o.CodexdataId != null,
				o.IsBroken,
				Active = o.RuinsiteActiveobelisks.Select(rsao => rsao.Ruinsite).Contains(rs),
			}),

			/*
			allGroups = rs.Ruintype.ObeliskGroup.Select(og => new { og.Id, og.Name, og.Count }),
			allObelisks = rs.Ruintype.ObeliskGroup.SelectMany(og => og.Obelisk).Select(o => new {
				o.Id,
				hasData = o.CodexdataId != null,
				o.IsBroken,
			}),
			*/
			//ObeliskGroups = rs.RuinsiteObeliskgroups.Select(rsog => rsog.Obeliskgroup),
			// Obelisks = rs.RuinsiteActiveobelisks.Select(rsao => rsao.Obelisk),
		})
		.Single().Dump();
}

public partial class Obelisk
{

}

// Define other methods and classes here
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

public partial class RuinsContext : DbContext
{
	public virtual DbSet<Artifact> Artifact { get; set; }
	public virtual DbSet<Body> Body { get; set; }
	public virtual DbSet<CodexCategory> CodexCategory { get; set; }
	public virtual DbSet<CodexData> CodexData { get; set; }
	public virtual DbSet<Obelisk> Obelisk { get; set; }
	public virtual DbSet<ObeliskGroup> ObeliskGroup { get; set; }
	public virtual DbSet<RuinSite> RuinSite { get; set; }
	public virtual DbSet<RuinType> RuinType { get; set; }
	public virtual DbSet<RuinsiteActiveobelisks> RuinsiteActiveobelisks { get; set; }
	public virtual DbSet<RuinsiteObeliskgroups> RuinsiteObeliskgroups { get; set; }
	public virtual DbSet<System> System { get; set; }
	/*
			protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
			{
				#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
				optionsBuilder.UseMySql(@"server=localhost;database=ruinsdb;userid=ruinsdb;password=ruinsdb");
			}
	*/
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Artifact>(entity =>
		{
			entity.ToTable("artifact");

			entity.Property(e => e.Id)
				.HasColumnName("id")
				.HasColumnType("int(11)");

			entity.Property(e => e.Created)
				.HasColumnName("created")
				.HasColumnType("datetime")
				.HasDefaultValueSql("CURRENT_TIMESTAMP");

			entity.Property(e => e.Name)
				.IsRequired()
				.HasColumnName("name")
				.HasColumnType("varchar(50)");

			entity.Property(e => e.Updated)
				.HasColumnName("updated")
				.HasColumnType("datetime")
				.HasDefaultValueSql("CURRENT_TIMESTAMP");
		});

		modelBuilder.Entity<Body>(entity =>
		{
			entity.ToTable("body");

			entity.HasIndex(e => e.SystemId)
				.HasName("FK_body_system");

			entity.HasIndex(e => new { e.SystemId, e.Name })
				.HasName("UX_body")
				.IsUnique();

			entity.Property(e => e.Id)
				.HasColumnName("id")
				.HasColumnType("int(11)");

			entity.Property(e => e.Created)
				.HasColumnName("created")
				.HasColumnType("timestamp")
				.HasDefaultValueSql("CURRENT_TIMESTAMP");

			entity.Property(e => e.EddbExtId)
				.HasColumnName("eddb_ext_id")
				.HasColumnType("int(11)");

			entity.Property(e => e.EdsmExtId)
				.HasColumnName("edsm_ext_id")
				.HasColumnType("int(11)");

			entity.Property(e => e.Name)
				.IsRequired()
				.HasColumnName("name")
				.HasColumnType("varchar(150)");

			entity.Property(e => e.SystemId)
				.HasColumnName("system_id")
				.HasColumnType("int(11)");

			entity.Property(e => e.Updated)
				.HasColumnName("updated")
				.HasColumnType("timestamp")
				.HasDefaultValueSql("CURRENT_TIMESTAMP");

			entity.HasOne(d => d.System)
				.WithMany(p => p.Bodies)
				.HasForeignKey(d => d.SystemId)
				.OnDelete(DeleteBehavior.Restrict)
				.HasConstraintName("FK_body_system");
		});

		modelBuilder.Entity<CodexCategory>(entity =>
		{
			entity.ToTable("codex_category");

			entity.HasIndex(e => e.ArtifactId)
				.HasName("FK_codexcategory_artifact");

			entity.Property(e => e.Id)
				.HasColumnName("id")
				.HasColumnType("int(11)");

			entity.Property(e => e.ArtifactId)
				.HasColumnName("artifact_id")
				.HasColumnType("int(11)");

			entity.Property(e => e.Created)
				.HasColumnName("created")
				.HasColumnType("datetime")
				.HasDefaultValueSql("CURRENT_TIMESTAMP");

			entity.Property(e => e.Name)
				.IsRequired()
				.HasColumnName("name")
				.HasColumnType("varchar(50)");

			entity.Property(e => e.Updated)
				.HasColumnName("updated")
				.HasColumnType("datetime")
				.HasDefaultValueSql("CURRENT_TIMESTAMP");

			entity.HasOne(d => d.Artifact)
				.WithMany(p => p.CodexCategory)
				.HasForeignKey(d => d.ArtifactId)
				.OnDelete(DeleteBehavior.Restrict)
				.HasConstraintName("FK_codexcategory_artifact");
		});

		modelBuilder.Entity<CodexData>(entity =>
		{
			entity.ToTable("codex_data");

			entity.HasIndex(e => e.CategoryId)
				.HasName("FK_codexdata_codexcategory");

			entity.Property(e => e.Id)
				.HasColumnName("id")
				.HasColumnType("int(11)");

			entity.Property(e => e.CategoryId)
				.HasColumnName("category_id")
				.HasColumnType("int(11)");

			entity.Property(e => e.Created)
				.HasColumnName("created")
				.HasColumnType("datetime")
				.HasDefaultValueSql("CURRENT_TIMESTAMP");

			entity.Property(e => e.EntryNumber)
				.HasColumnName("entry_number")
				.HasColumnType("int(11)");

			entity.Property(e => e.Text)
				.IsRequired()
				.HasColumnName("text")
				.HasColumnType("text");

			entity.Property(e => e.Updated)
				.HasColumnName("updated")
				.HasColumnType("datetime")
				.HasDefaultValueSql("CURRENT_TIMESTAMP");

			entity.HasOne(d => d.Category)
				.WithMany(p => p.CodexData)
				.HasForeignKey(d => d.CategoryId)
				.OnDelete(DeleteBehavior.Restrict)
				.HasConstraintName("FK_codexdata_codexcategory");
		});

		modelBuilder.Entity<Obelisk>(entity =>
		{
			entity.ToTable("obelisk");

			entity.HasIndex(e => e.ArtifactId)
				.HasName("FK_obelisk_artifact");

			entity.HasIndex(e => e.CodexdataId)
				.HasName("FK_obelisk_codexdata");

			entity.HasIndex(e => new { e.ObeliskgroupId, e.Number })
				.HasName("UX_obeliskgroupid")
				.IsUnique();

			entity.Property(e => e.Id)
				.HasColumnName("id")
				.HasColumnType("int(11)");

			entity.Property(e => e.ArtifactId)
				.HasColumnName("artifact_id")
				.HasColumnType("int(11)");

			entity.Property(e => e.CodexdataId)
				.HasColumnName("codexdata_id")
				.HasColumnType("int(11)");

			entity.Property(e => e.Created)
				.HasColumnName("created")
				.HasColumnType("datetime")
				.HasDefaultValueSql("CURRENT_TIMESTAMP");

			entity.Property(e => e.IsBroken)
				.HasColumnName("is_broken")
				.HasColumnType("bit(1)")
				.HasDefaultValueSql("b'0'");

			entity.Property(e => e.Number)
				.HasColumnName("number")
				.HasColumnType("int(11)");

			entity.Property(e => e.ObeliskgroupId)
				.HasColumnName("obeliskgroup_id")
				.HasColumnType("int(11)");

			entity.Property(e => e.Updated)
				.HasColumnName("updated")
				.HasColumnType("datetime")
				.HasDefaultValueSql("CURRENT_TIMESTAMP");

			entity.HasOne(d => d.Artifact)
				.WithMany(p => p.Obelisk)
				.HasForeignKey(d => d.ArtifactId)
				.HasConstraintName("FK_obelisk_artifact");

			entity.HasOne(d => d.Codexdata)
				.WithMany(p => p.Obelisk)
				.HasForeignKey(d => d.CodexdataId)
				.HasConstraintName("FK_obelisk_codexdata");

			entity.HasOne(d => d.Obeliskgroup)
				.WithMany(p => p.Obelisk)
				.HasForeignKey(d => d.ObeliskgroupId)
				.OnDelete(DeleteBehavior.Restrict)
				.HasConstraintName("FK_obelisk_obeliskgroup");
		});

		modelBuilder.Entity<ObeliskGroup>(entity =>
		{
			entity.ToTable("obelisk_group");

			entity.HasIndex(e => e.RuintypeId)
				.HasName("FK_obeliskgroup_ruintype");

			entity.Property(e => e.Id)
				.HasColumnName("id")
				.HasColumnType("int(11)");

			entity.Property(e => e.Count)
				.HasColumnName("count")
				.HasColumnType("int(11)");

			entity.Property(e => e.Created)
				.HasColumnName("created")
				.HasColumnType("datetime")
				.HasDefaultValueSql("CURRENT_TIMESTAMP");

			entity.Property(e => e.Name)
				.IsRequired()
				.HasColumnName("name")
				.HasColumnType("varchar(50)");

			entity.Property(e => e.RuintypeId)
				.HasColumnName("ruintype_id")
				.HasColumnType("int(11)");

			entity.Property(e => e.Updated)
				.HasColumnName("updated")
				.HasColumnType("datetime")
				.HasDefaultValueSql("CURRENT_TIMESTAMP");

			entity.HasOne(d => d.Ruintype)
				.WithMany(p => p.ObeliskGroup)
				.HasForeignKey(d => d.RuintypeId)
				.OnDelete(DeleteBehavior.Restrict)
				.HasConstraintName("FK_obeliskgroup_ruintype");
		});

		modelBuilder.Entity<RuinSite>(entity =>
		{
			entity.ToTable("ruin_site");

			entity.HasIndex(e => e.BodyId)
				.HasName("FK_ruinsite_body");

			entity.HasIndex(e => e.RuintypeId)
				.HasName("FK_ruinsite_ruintype");

			entity.Property(e => e.Id)
				.HasColumnName("id")
				.HasColumnType("int(11)");

			entity.Property(e => e.BodyId)
				.HasColumnName("body_id")
				.HasColumnType("int(11)");

			entity.Property(e => e.Created)
				.HasColumnName("created")
				.HasColumnType("timestamp")
				.HasDefaultValueSql("CURRENT_TIMESTAMP");

			entity.Property(e => e.Latitude)
				.HasColumnName("latitude")
				.HasColumnType("decimal(6,4)");

			entity.Property(e => e.Longitude)
				.HasColumnName("longitude")
				.HasColumnType("decimal(7,4)");

			entity.Property(e => e.RuintypeId)
				.HasColumnName("ruintype_id")
				.HasColumnType("int(11)");

			entity.Property(e => e.Updated)
				.HasColumnName("updated")
				.HasColumnType("timestamp")
				.HasDefaultValueSql("CURRENT_TIMESTAMP");

			entity.HasOne(d => d.Body)
				.WithMany(p => p.RuinSites)
				.HasForeignKey(d => d.BodyId)
				.OnDelete(DeleteBehavior.Restrict)
				.HasConstraintName("FK_ruinsite_body");

			entity.HasOne(d => d.Ruintype)
				.WithMany(p => p.RuinSite)
				.HasForeignKey(d => d.RuintypeId)
				.OnDelete(DeleteBehavior.Restrict)
				.HasConstraintName("FK_ruinsite_ruintype");
		});

		modelBuilder.Entity<RuinType>(entity =>
		{
			entity.ToTable("ruin_type");

			entity.Property(e => e.Id)
				.HasColumnName("id")
				.HasColumnType("int(11)");

			entity.Property(e => e.Created)
				.HasColumnName("created")
				.HasColumnType("datetime")
				.HasDefaultValueSql("CURRENT_TIMESTAMP");

			entity.Property(e => e.Name)
				.IsRequired()
				.HasColumnName("name")
				.HasColumnType("varchar(50)");

			entity.Property(e => e.Updated)
				.HasColumnName("updated")
				.HasColumnType("datetime")
				.HasDefaultValueSql("CURRENT_TIMESTAMP");
		});

		modelBuilder.Entity<RuinsiteActiveobelisks>(entity =>
		{
			entity.HasKey(e => new { e.RuinsiteId, e.ObeliskId })
				.HasName("PK_ruinsite_activeobelisks");

			entity.ToTable("ruinsite_activeobelisks");

			entity.HasIndex(e => e.ObeliskId)
				.HasName("FK_ruinsiteactiveobelisk_obelisk");

			entity.Property(e => e.RuinsiteId)
				.HasColumnName("ruinsite_id")
				.HasColumnType("int(11)");

			entity.Property(e => e.ObeliskId)
				.HasColumnName("obelisk_id")
				.HasColumnType("int(11)");

			entity.HasOne(d => d.Obelisk)
				.WithMany(p => p.RuinsiteActiveobelisks)
				.HasForeignKey(d => d.ObeliskId)
				.OnDelete(DeleteBehavior.Restrict)
				.HasConstraintName("FK_ruinsiteactiveobelisk_obelisk");

			entity.HasOne(d => d.Ruinsite)
				.WithMany(p => p.RuinsiteActiveobelisks)
				.HasForeignKey(d => d.RuinsiteId)
				.OnDelete(DeleteBehavior.Restrict)
				.HasConstraintName("FK_ruinsiteactiveobelisk_ruinsite");
		});

		modelBuilder.Entity<RuinsiteObeliskgroups>(entity =>
		{
			entity.HasKey(e => new { e.RuinsiteId, e.ObeliskgroupId })
				.HasName("PK_ruinsite_obeliskgroups");

			entity.ToTable("ruinsite_obeliskgroups");

			entity.HasIndex(e => e.ObeliskgroupId)
				.HasName("FK_ruinsiteobeliskgroup_obeliskgroup");

			entity.Property(e => e.RuinsiteId)
				.HasColumnName("ruinsite_id")
				.HasColumnType("int(11)");

			entity.Property(e => e.ObeliskgroupId)
				.HasColumnName("obeliskgroup_id")
				.HasColumnType("int(11)");

			entity.HasOne(d => d.Obeliskgroup)
				.WithMany(p => p.RuinsiteObeliskgroups)
				.HasForeignKey(d => d.ObeliskgroupId)
				.OnDelete(DeleteBehavior.Restrict)
				.HasConstraintName("FK_ruinsiteobeliskgroup_obeliskgroup");

			entity.HasOne(d => d.Ruinsite)
				.WithMany(p => p.RuinsiteObeliskgroups)
				.HasForeignKey(d => d.RuinsiteId)
				.OnDelete(DeleteBehavior.Restrict)
				.HasConstraintName("FK_ruinsiteobeliskgroup_ruinsite");
		});

		modelBuilder.Entity<System>(entity =>
		{
			entity.ToTable("system");

			entity.HasIndex(e => e.Name)
				.HasName("UX_name")
				.IsUnique();

			entity.Property(e => e.Id)
				.HasColumnName("id")
				.HasColumnType("int(11)");

			entity.Property(e => e.Created)
				.HasColumnName("created")
				.HasColumnType("timestamp")
				.HasDefaultValueSql("CURRENT_TIMESTAMP");

			entity.Property(e => e.EddbExtId)
				.HasColumnName("eddb_ext_id")
				.HasColumnType("int(11)");

			entity.Property(e => e.EdsmExtId)
				.HasColumnName("edsm_ext_id")
				.HasColumnType("int(11)");

			entity.Property(e => e.Name)
				.IsRequired()
				.HasColumnName("name")
				.HasColumnType("varchar(150)");

			entity.Property(e => e.Updated)
				.HasColumnName("updated")
				.HasColumnType("timestamp")
				.HasDefaultValueSql("CURRENT_TIMESTAMP");
		});

		AmendModel(modelBuilder);
	}
}

public partial class Artifact
{
	public Artifact()
	{
		CodexCategory = new HashSet<CodexCategory>();
		Obelisk = new HashSet<Obelisk>();
	}

	public int Id { get; set; }
	public DateTime Created { get; set; }
	public string Name { get; set; }
	public DateTime Updated { get; set; }

	public virtual ICollection<CodexCategory> CodexCategory { get; set; }
	public virtual ICollection<Obelisk> Obelisk { get; set; }
}

public partial class Body
{
	public Body()
	{
		RuinSites = new HashSet<RuinSite>();
	}

	public int Id { get; set; }
	public DateTime Created { get; set; }
	public int? EddbExtId { get; set; }
	public int? EdsmExtId { get; set; }
	public string Name { get; set; }
	public int SystemId { get; set; }
	public DateTime Updated { get; set; }

	public virtual ICollection<RuinSite> RuinSites { get; set; }
	public virtual System System { get; set; }
}

public partial class CodexCategory
{
	public CodexCategory()
	{
		CodexData = new HashSet<CodexData>();
	}

	public int Id { get; set; }
	public int ArtifactId { get; set; }
	public DateTime Created { get; set; }
	public string Name { get; set; }
	public DateTime Updated { get; set; }

	public virtual ICollection<CodexData> CodexData { get; set; }
	public virtual Artifact Artifact { get; set; }
}

public partial class CodexData
{
	public CodexData()
	{
		Obelisk = new HashSet<Obelisk>();
	}

	public int Id { get; set; }
	public int CategoryId { get; set; }
	public DateTime Created { get; set; }
	public int EntryNumber { get; set; }
	public string Text { get; set; }
	public DateTime Updated { get; set; }

	public virtual ICollection<Obelisk> Obelisk { get; set; }
	public virtual CodexCategory Category { get; set; }
}

public partial class Obelisk
{
	public Obelisk()
	{
		RuinsiteActiveobelisks = new HashSet<RuinsiteActiveobelisks>();
	}

	public int Id { get; set; }
	public int? ArtifactId { get; set; }
	public int? CodexdataId { get; set; }
	public DateTime Created { get; set; }
	public bool IsBroken { get; set; }
	public int Number { get; set; }
	public int ObeliskgroupId { get; set; }
	public DateTime Updated { get; set; }

	public virtual ICollection<RuinsiteActiveobelisks> RuinsiteActiveobelisks { get; set; }
	public virtual Artifact Artifact { get; set; }
	public virtual CodexData Codexdata { get; set; }
	public virtual ObeliskGroup Obeliskgroup { get; set; }
}

public partial class ObeliskGroup
{
	public ObeliskGroup()
	{
		Obelisk = new HashSet<Obelisk>();
		RuinsiteObeliskgroups = new HashSet<RuinsiteObeliskgroups>();
	}

	public int Id { get; set; }
	public int Count { get; set; }
	public DateTime Created { get; set; }
	public string Name { get; set; }
	public int RuintypeId { get; set; }
	public DateTime Updated { get; set; }

	public virtual ICollection<Obelisk> Obelisk { get; set; }
	public virtual ICollection<RuinsiteObeliskgroups> RuinsiteObeliskgroups { get; set; }
	public virtual RuinType Ruintype { get; set; }
}

public partial class RuinSite
{
	public RuinSite()
	{
		RuinsiteActiveobelisks = new HashSet<RuinsiteActiveobelisks>();
		RuinsiteObeliskgroups = new HashSet<RuinsiteObeliskgroups>();
	}

	public int Id { get; set; }
	public int BodyId { get; set; }
	public DateTime Created { get; set; }
	public decimal Latitude { get; set; }
	public decimal Longitude { get; set; }
	public int RuintypeId { get; set; }
	public DateTime Updated { get; set; }

	public virtual ICollection<RuinsiteActiveobelisks> RuinsiteActiveobelisks { get; set; }
	public virtual ICollection<RuinsiteObeliskgroups> RuinsiteObeliskgroups { get; set; }
	public virtual Body Body { get; set; }
	public virtual RuinType Ruintype { get; set; }
}

public partial class RuinsiteActiveobelisks
{
	public int RuinsiteId { get; set; }
	public int ObeliskId { get; set; }

	public virtual Obelisk Obelisk { get; set; }
	public virtual RuinSite Ruinsite { get; set; }
}

public partial class RuinsiteObeliskgroups
{
	public int RuinsiteId { get; set; }
	public int ObeliskgroupId { get; set; }

	public virtual ObeliskGroup Obeliskgroup { get; set; }
	public virtual RuinSite Ruinsite { get; set; }
}

public partial class RuinType
{
	public RuinType()
	{
		ObeliskGroup = new HashSet<ObeliskGroup>();
		RuinSite = new HashSet<RuinSite>();
	}

	public int Id { get; set; }
	public DateTime Created { get; set; }
	public string Name { get; set; }
	public DateTime Updated { get; set; }

	public virtual ICollection<ObeliskGroup> ObeliskGroup { get; set; }
	public virtual ICollection<RuinSite> RuinSite { get; set; }
}

public partial class System
{
	public System()
	{
		Bodies = new HashSet<Body>();
	}

	public int Id { get; set; }
	public DateTime Created { get; set; }
	public int? EddbExtId { get; set; }
	public int? EdsmExtId { get; set; }
	public string Name { get; set; }
	public DateTime Updated { get; set; }

	public virtual ICollection<Body> Bodies { get; set; }
}