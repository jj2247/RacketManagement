using RacketManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace RacketManagement.Data
{
  public class RacketManagementContext : IdentityDbContext<ApplicationUser>
  {
    public RacketManagementContext(DbContextOptions<RacketManagementContext> options) : base(options)
    {
    }

    public DbSet<Racket> Rackets { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Model> Models { get; set; }
    public DbSet<GripSize> GripSizes { get; set; }
    public DbSet<Loan> Loans { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Brand>().ToTable("Brand");
      modelBuilder.Entity<Model>().ToTable("Model");
      modelBuilder.Entity<GripSize>().ToTable("GripSize");
      modelBuilder.Entity<Racket>().ToTable("Racket");
      modelBuilder.Entity<Loan>().ToTable("Loan");
      base.OnModelCreating(modelBuilder);
    }
  }

}