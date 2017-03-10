using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RuinsApi.DatabaseModels
{
    public partial class RuinsContext : DbContext
    {
        public virtual DbSet<ActiveObelisk> ActiveObelisk { get; set; }
        public virtual DbSet<CodexCategory> CodexCategory { get; set; }
        public virtual DbSet<CodexData> CodexData { get; set; }
        public virtual DbSet<LayoutVariant> LayoutVariant { get; set; }
        public virtual DbSet<Obelisk> Obelisk { get; set; }
        public virtual DbSet<ObeliskGroup> ObeliskGroup { get; set; }
        public virtual DbSet<Relict> Relict { get; set; }
        public virtual DbSet<RuinLayout> RuinLayout { get; set; }
        public virtual DbSet<RuinType> RuinType { get; set; }
        public virtual DbSet<RuinLayoutObeliskGroups> RuinlayoutObeilskgroups { get; set; }

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

                entity.HasIndex(e => e.VariantId)
                    .HasName("FK_activeobelisk_variant");

                entity.HasIndex(e => new { e.ObeliskId, e.VariantId })
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

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.VariantId)
                    .HasColumnName("variant_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Obelisk)
                    .WithMany(p => p.ActiveObelisk)
                    .HasForeignKey(d => d.ObeliskId)
                    .HasConstraintName("FK_activeobelisk_obelisk");

                entity.HasOne(d => d.Variant)
                    .WithMany(p => p.ActiveObelisk)
                    .HasForeignKey(d => d.VariantId)
                    .HasConstraintName("FK_activeobelisk_variant");
            });

            modelBuilder.Entity<CodexCategory>(entity =>
            {
                entity.ToTable("codex_category");

                entity.HasIndex(e => e.PrimaryRelict)
                    .HasName("FK_codexcategory_relict");

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

                entity.Property(e => e.PrimaryRelict)
                    .HasColumnName("primary_relict")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.PrimaryRelictNavigation)
                    .WithMany(p => p.CodexCategory)
                    .HasForeignKey(d => d.PrimaryRelict)
                    .HasConstraintName("FK_codexcategory_relic");
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

            modelBuilder.Entity<LayoutVariant>(entity =>
            {
                entity.ToTable("layout_variant");

                entity.HasIndex(e => new { e.LayoutId, e.Name })
                    .HasName("UX_layout_variant")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.LayoutId)
                    .HasColumnName("layout_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Layout)
                    .WithMany(p => p.LayoutVariant)
                    .HasForeignKey(d => d.LayoutId)
                    .HasConstraintName("FK_layoutvariant_ruinlayout");
            });

            modelBuilder.Entity<Obelisk>(entity =>
            {
                entity.ToTable("obelisk");

                entity.HasIndex(e => e.DataId)
                    .HasName("FK_obelisk_data");

                entity.HasIndex(e => e.RelictId)
                    .HasName("FK_obelisk_relict");

                entity.HasIndex(e => new { e.GroupId, e.Number })
                    .HasName("UX_obelisk")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.DataId)
                    .HasColumnName("data_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.GroupId)
                    .HasColumnName("group_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Number)
                    .HasColumnName("number")
                    .HasColumnType("int(11)");

                entity.Property(e => e.RelictId)
                    .HasColumnName("relict_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Data)
                    .WithMany(p => p.Obelisk)
                    .HasForeignKey(d => d.DataId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_obelisk_data");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Obelisk)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK_obelisk_group");

                entity.HasOne(d => d.Relict)
                    .WithMany(p => p.Obelisk)
                    .HasForeignKey(d => d.RelictId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_obelisk_relict");
            });

            modelBuilder.Entity<ObeliskGroup>(entity =>
            {
                entity.ToTable("obelisk_group");

                entity.HasIndex(e => e.TypeId)
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

                entity.Property(e => e.TypeId)
                    .HasColumnName("type_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.ObeliskGroup)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_obeliskgroup_ruintype");
            });

            modelBuilder.Entity<Relict>(entity =>
            {
                entity.ToTable("relict");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            modelBuilder.Entity<RuinLayout>(entity =>
            {
                entity.ToTable("ruin_layout");

                entity.HasIndex(e => new { e.TypeId, e.Name })
                    .HasName("UX_ruinlayout")
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

                entity.Property(e => e.TypeId)
                    .HasColumnName("type_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.RuinLayout)
                    .HasForeignKey(d => d.TypeId)
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

            modelBuilder.Entity<RuinLayoutObeliskGroups>(entity =>
            {
                entity.ToTable("ruinlayout_obeilskgroups");

                entity.HasIndex(e => e.GroupId)
                    .HasName("FK_ruinlayoutobeliskgroups_obeliskgroup");

                entity.HasIndex(e => new { e.LayoutId, e.GroupId })
                    .HasName("UX_ruinlayout_obeliskgroups")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.GroupId)
                    .HasColumnName("group_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.LayoutId)
                    .HasColumnName("layout_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.RuinLayoutObeliskGroups)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK_ruinlayoutobeliskgroups_obeliskgroup");

                entity.HasOne(d => d.Layout)
                    .WithMany(p => p.RuinlayoutObeilskgroups)
                    .HasForeignKey(d => d.LayoutId)
                    .HasConstraintName("FK_ruinlayoutobeliskgroups_ruinlayout");
            });
        }
    }
}