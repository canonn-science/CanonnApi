using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RuinsApi.DatabaseModels
{
    public partial class RuinsContext : DbContext
    {
        public virtual DbSet<ActiveObelisk> ActiveObelisk { get; set; }
        public virtual DbSet<Artifact> Artifact { get; set; }
        public virtual DbSet<CodexCategory> CodexCategory { get; set; }
        public virtual DbSet<CodexData> CodexData { get; set; }
        public virtual DbSet<Obelisk> Obelisk { get; set; }
        public virtual DbSet<ObeliskGroup> ObeliskGroup { get; set; }
        public virtual DbSet<RuinLayout> RuinLayout { get; set; }
        public virtual DbSet<RuinType> RuinType { get; set; }
        public virtual DbSet<RuinlayoutObeliskgroups> RuinlayoutObeliskgroups { get; set; }
        public virtual DbSet<RuinlayoutVariant> RuinlayoutVariant { get; set; }
/*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            optionsBuilder.UseMySql(@"server=localhost;database=ruinsdb;userid=ruinsdb;password=ruinsdb");
        }
*/
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActiveObelisk>(entity =>
            {
                entity.ToTable("active_obelisk");

                entity.HasIndex(e => e.RuinlayoutvariantId)
                    .HasName("FK_activeobelisk_variant");

                entity.HasIndex(e => new { e.ObeliskId, e.RuinlayoutvariantId })
                    .HasName("UX_active_obelisk")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.ObeliskId)
                    .HasColumnName("obelisk_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.RuinlayoutvariantId)
                    .HasColumnName("ruinlayoutvariant_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Obelisk)
                    .WithMany(p => p.ActiveObelisk)
                    .HasForeignKey(d => d.ObeliskId)
                    .HasConstraintName("FK_activeobelisk_obelisk");

                entity.HasOne(d => d.Ruinlayoutvariant)
                    .WithMany(p => p.ActiveObelisk)
                    .HasForeignKey(d => d.RuinlayoutvariantId)
                    .HasConstraintName("FK_activeobelisk_variant");
            });

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
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_obelisk_artifact");

                entity.HasOne(d => d.Codexdata)
                    .WithMany(p => p.Obelisk)
                    .HasForeignKey(d => d.CodexdataId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_obelisk_codexdata");

                entity.HasOne(d => d.Obeliskgroup)
                    .WithMany(p => p.Obelisk)
                    .HasForeignKey(d => d.ObeliskgroupId)
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
                    .HasColumnType("mediumtext");

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
                    .HasConstraintName("FK_obeliskgroup_ruintype");
            });

            modelBuilder.Entity<RuinLayout>(entity =>
            {
                entity.ToTable("ruin_layout");

                entity.HasIndex(e => new { e.RuintypeId, e.Name })
                    .HasName("UX_ruinlayout_ruintype")
                    .IsUnique();

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

                entity.Property(e => e.RuintypeId)
                    .HasColumnName("ruintype_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Ruintype)
                    .WithMany(p => p.RuinLayout)
                    .HasForeignKey(d => d.RuintypeId)
                    .HasConstraintName("FK_ruinlayout_ruintype");
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
                    .HasColumnType("text");

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            modelBuilder.Entity<RuinlayoutObeliskgroups>(entity =>
            {
                entity.ToTable("ruinlayout_obeliskgroups");

                entity.HasIndex(e => e.ObeliskgroupId)
                    .HasName("FK_ruinlayoutobeliskgroups_obeliskgroup");

                entity.HasIndex(e => new { e.RuinlayoutId, e.ObeliskgroupId })
                    .HasName("UX_ruinlayout_obeliskgroups")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.ObeliskgroupId)
                    .HasColumnName("obeliskgroup_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.RuinlayoutId)
                    .HasColumnName("ruinlayout_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Obeliskgroup)
                    .WithMany(p => p.RuinlayoutObeliskgroups)
                    .HasForeignKey(d => d.ObeliskgroupId)
                    .HasConstraintName("FK_ruinlayoutobeliskgroups_obeliskgroup");

                entity.HasOne(d => d.Ruinlayout)
                    .WithMany(p => p.RuinlayoutObeliskgroups)
                    .HasForeignKey(d => d.RuinlayoutId)
                    .HasConstraintName("FK_ruinlayoutobeliskgroups_ruinlayout");
            });

            modelBuilder.Entity<RuinlayoutVariant>(entity =>
            {
                entity.ToTable("ruinlayout_variant");

                entity.HasIndex(e => new { e.RuinlayoutId, e.Name })
                    .HasName("UX_ruinlayout_variant")
                    .IsUnique();

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

                entity.Property(e => e.RuinlayoutId)
                    .HasColumnName("ruinlayout_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Ruinlayout)
                    .WithMany(p => p.RuinlayoutVariant)
                    .HasForeignKey(d => d.RuinlayoutId)
                    .HasConstraintName("FK_ruinlayoutvariant_ruinlayout");
            });
        }
    }
}