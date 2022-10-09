using AdventurerOfficialProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdventurerOfficialProject.Areas.Identity.Data;

public class AdventurerDbContext : IdentityDbContext<IdentityUser>
{
    public DbSet<UserAtributes> UserAtributesDbSet { get; set; }
    public DbSet<Activities> ActivitiesDbSet { get; set; }
    public DbSet<CountryModel> CountryModelDbSet { get; set; }
    public DbSet<CityModel> CityModelDbSet { get; set; }


    public AdventurerDbContext(DbContextOptions<AdventurerDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
        builder.ApplyConfiguration(new ApplicationUserEntityConfigutarion());
    }

    public class ApplicationUserEntityConfigutarion : IEntityTypeConfiguration<UniqueCodeToDatabase>
    {
        public void Configure(EntityTypeBuilder<UniqueCodeToDatabase> builder)
        {
            builder.Property(un => un.UniqueCode).HasMaxLength(255);
        }
    }
}
