using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CanonnApi.Web.DatabaseModels
{
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

                entity.Property(e => e.Distance)
                    .HasColumnName("distance")
                    .HasColumnType("int(11)");

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
}