using Microsoft.EntityFrameworkCore;

namespace TollFee.Infrastructure.Data;

//TODO: add fee rules in database, for now we didn't use db, also all work with DB should be implemented in infrastrucure project
public partial class TollDBContext : DbContext
{
    public TollDBContext()
    {
    }

    public TollDBContext(DbContextOptions<TollDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ToolFee> Fees { get; set; }
    public virtual DbSet<TollFree> TollFrees { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=TollDB;Trusted_Connection=True;");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

        modelBuilder.Entity<ToolFee>(entity =>
        {
            entity.HasKey(e => new { e.Year, e.FromMinute, e.ToMinute, e.Price });

            entity.ToTable("Fee");

            entity.Property(e => e.Price).HasColumnType("decimal(2, 0)");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<TollFree>(entity =>
        {
            entity.HasKey(e => new { e.Year, e.Date });

            entity.ToTable("TollFree");

            entity.Property(e => e.Date).HasColumnType("date");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
