using RacketManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace RacketManagement.Data
{
  public class RacketManagementContext : DbContext
  {
    public RacketManagementContext(DbContextOptions<RacketManagementContext> options) : base(options)
    {
    }

    public DbSet<Racket> Rackets { get; set; }
    public DbSet<Brand> Brands { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Racket>().ToTable("Racket");
      modelBuilder.Entity<Brand>().ToTable("Brand");
    }
  }

}