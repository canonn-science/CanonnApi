using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CanonnApi.Web.DatabaseModels
{
    public partial class CanonnApiDatabaseContext : DbContext
    {
        public virtual DbSet<Artifact> Artifact { get; set; }
        public virtual DbSet<Body> Body { get; set; }
        public virtual DbSet<CanonndbMetadata> CanonndbMetadata { get; set; }
        public virtual DbSet<CodexCategory> CodexCategory { get; set; }
        public virtual DbSet<CodexData> CodexData { get; set; }
        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<LocationType> LocationType { get; set; }
        public virtual DbSet<Obelisk> Obelisk { get; set; }
        public virtual DbSet<ObeliskGroup> ObeliskGroup { get; set; }
        public virtual DbSet<RuinSite> RuinSite { get; set; }
        public virtual DbSet<RuinType> RuinType { get; set; }
        public virtual DbSet<RuinsiteActiveobelisks> RuinsiteActiveobelisks { get; set; }
        public virtual DbSet<RuinsiteObeliskgroups> RuinsiteObeliskgroups { get; set; }
        public virtual DbSet<System> System { get; set; }
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
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Distance)
                    .HasColumnName("distance")
                    .HasColumnType("int(11)");

                entity.Property(e => e.EarthMasses).HasColumnName("earth_masses");

                entity.Property(e => e.EddbExtId)
                    .HasColumnName("eddb_ext_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.EdsmExtId)
                    .HasColumnName("edsm_ext_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.EdsmLastUpdate)
                    .HasColumnName("edsm_last_update")
                    .HasColumnType("datetime");

                entity.Property(e => e.Gravity).HasColumnName("gravity");

                entity.Property(e => e.IsLandable)
                    .HasColumnName("is_landable")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(150)");

                entity.Property(e => e.Radius).HasColumnName("radius");

                entity.Property(e => e.SystemId)
                    .HasColumnName("system_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.System)
                    .WithMany(p => p.Body)
                    .HasForeignKey(d => d.SystemId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_body_system");
            });

            modelBuilder.Entity<CanonndbMetadata>(entity =>
            {
                entity.HasKey(e => e.Name)
                    .HasName("PK_canonndb_metadata");

                entity.ToTable("canonndb_metadata");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasColumnName("value")
                    .HasColumnType("varchar(255)");
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

                entity.HasIndex(e => e.ArtifactId)
                    .HasName("FK_codexdata_artifact");

                entity.HasIndex(e => e.CategoryId)
                    .HasName("FK_codexdata_codexcategory");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ArtifactId)
                    .HasColumnName("artifact_id")
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

                entity.HasOne(d => d.Artifact)
                    .WithMany(p => p.CodexData)
                    .HasForeignKey(d => d.ArtifactId)
                    .HasConstraintName("FK_codexdata_artifact");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.CodexData)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_codexdata_codexcategory");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("location");

                entity.HasIndex(e => e.BodyId)
                    .HasName("FK_location_body");

                entity.HasIndex(e => e.DirectionSystemId)
                    .HasName("FK_location_directionsystem");

                entity.HasIndex(e => e.LocationtypeId)
                    .HasName("FK_location_locationtype");

                entity.HasIndex(e => e.SystemId)
                    .HasName("FK_location_system");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.BodyId)
                    .HasColumnName("body_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.DirectionSystemId)
                    .HasColumnName("direction_system_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Distance)
                    .HasColumnName("distance")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IsVisible)
                    .HasColumnName("is_visible")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("b'1'");

                entity.Property(e => e.Latitude)
                    .HasColumnName("latitude")
                    .HasColumnType("decimal(6,4)");

                entity.Property(e => e.LocationtypeId)
                    .HasColumnName("locationtype_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Longitude)
                    .HasColumnName("longitude")
                    .HasColumnType("decimal(7,4)");

                entity.Property(e => e.SystemId)
                    .HasColumnName("system_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Body)
                    .WithMany(p => p.Location)
                    .HasForeignKey(d => d.BodyId)
                    .HasConstraintName("FK_location_body");

                entity.HasOne(d => d.DirectionSystem)
                    .WithMany(p => p.LocationDirectionSystem)
                    .HasForeignKey(d => d.DirectionSystemId)
                    .HasConstraintName("FK_location_directionsystem");

                entity.HasOne(d => d.Locationtype)
                    .WithMany(p => p.Location)
                    .HasForeignKey(d => d.LocationtypeId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_location_locationtype");

                entity.HasOne(d => d.System)
                    .WithMany(p => p.LocationSystem)
                    .HasForeignKey(d => d.SystemId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_location_system");
            });

            modelBuilder.Entity<LocationType>(entity =>
            {
                entity.ToTable("location_type");

                entity.HasIndex(e => e.ShortName)
                    .HasName("IX_location_short_name");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.IsSurface)
                    .HasColumnName("is_surface")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("text");

                entity.Property(e => e.ShortName)
                    .IsRequired()
                    .HasColumnName("short_name")
                    .HasColumnType("tinytext");

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            modelBuilder.Entity<Obelisk>(entity =>
            {
                entity.ToTable("obelisk");

                entity.HasIndex(e => e.CodexdataId)
                    .HasName("FK_obelisk_codexdata");

                entity.HasIndex(e => e.Number)
                    .HasName("IX_obelisk_number");

                entity.HasIndex(e => new { e.ObeliskgroupId, e.Number })
                    .HasName("UX_obeliskgroupid")
                    .IsUnique();

                entity.HasIndex(e => new { e.IsBroken, e.CodexdataId, e.Number })
                    .HasName("IX_obelisk_scandata");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
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

                entity.Property(e => e.IsVerified)
                    .HasColumnName("is_verified")
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

                entity.HasIndex(e => e.Name)
                    .HasName("IX_obeliskgroup_name");

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

                entity.HasIndex(e => e.LocationId)
                    .HasName("FK_ruinsite_location");

                entity.HasIndex(e => e.RuintypeId)
                    .HasName("FK_ruinsite_ruintype");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.LocationId)
                    .HasColumnName("location_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.RuintypeId)
                    .HasColumnName("ruintype_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.RuinSite)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ruinsite_location");

                entity.HasOne(d => d.Ruintype)
                    .WithMany(p => p.RuinSite)
                    .HasForeignKey(d => d.RuintypeId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ruinsite_ruintype");
            });

            modelBuilder.Entity<RuinType>(entity =>
            {
                entity.ToTable("ruin_type");

                entity.HasIndex(e => e.Name)
                    .HasName("IX_ruintype_name");

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
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.EddbExtId)
                    .HasColumnName("eddb_ext_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.EdsmCoordUpdated)
                    .HasColumnName("edsm_coord_updated")
                    .HasColumnType("datetime");

                entity.Property(e => e.EdsmCoordX).HasColumnName("edsm_coord_x");

                entity.Property(e => e.EdsmCoordY).HasColumnName("edsm_coord_y");

                entity.Property(e => e.EdsmCoordZ).HasColumnName("edsm_coord_z");

                entity.Property(e => e.EdsmExtId)
                    .HasColumnName("edsm_ext_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(150)");

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            AmendModel(modelBuilder);
        }
    }
}
