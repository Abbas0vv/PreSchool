namespace PreSchool.Database;
using Microsoft.EntityFrameworkCore;
using PreSchool.Database.Models;

public class PreSchoolDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = "Server=localhost;Port=5432;Database=PreSchool;User Id=postgres;Password=admin;";
        optionsBuilder.UseNpgsql(connectionString);

        base.OnConfiguring(optionsBuilder);
    }

    public DbSet<SlideBanner> SlideBanners { get; set; }
}
