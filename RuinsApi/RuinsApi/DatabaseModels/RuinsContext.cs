using Microsoft.EntityFrameworkCore;

namespace RuinsApi.DatabaseModels
{
	public partial class RuinsContext : DbContext
	{
		public virtual DbSet<CodexCategory> CodexCategory { get; set; }
		public virtual DbSet<CodexData> CodexData { get; set; }
		public virtual DbSet<Relict> Relict { get; set; }

		public RuinsContext(DbContextOptions<RuinsContext> options)
			:base(options)
		{ }

		/*
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
			optionsBuilder.UseMySql(@"server=localhost;database=ruinsdb;userid=ruinsdb;password=ruinsdb");
		}
		*/

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<CodexCategory>(entity =>
			{
				entity.ToTable("codex_category");

				entity.HasIndex(e => e.PrimaryRelict)
						 .HasName("fk_relict");

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
						 .HasConstraintName("fk_relict");
			});

			modelBuilder.Entity<CodexData>(entity =>
			{
				entity.ToTable("codex_data");

				entity.HasIndex(e => e.CategoryId)
						 .HasName("fk_category");

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
						 .HasConstraintName("fk_category");
			});

			modelBuilder.Entity<Relict>(entity =>
			{
				entity.ToTable("relict");

				entity.Property(e => e.Id)
						 .HasColumnName("id")
						 .HasColumnType("int(11)");

				entity.Property(e => e.Name) // added manually??
						 .IsRequired()
						 .HasColumnName("name")
						 .HasColumnType("tinytext");

				entity.Property(e => e.Created)
						 .HasColumnName("created")
						 .HasColumnType("datetime")
						 .HasDefaultValueSql("CURRENT_TIMESTAMP");

				entity.Property(e => e.Updated)
						 .HasColumnName("updated")
						 .HasColumnType("datetime")
						 .HasDefaultValueSql("CURRENT_TIMESTAMP");
			});
		}
	}
}
