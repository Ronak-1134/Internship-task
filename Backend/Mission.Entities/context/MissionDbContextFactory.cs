using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Mission.Entities.context;

public class MissionDbContextFactory : IDesignTimeDbContextFactory<MissionDbContext>
{
    public MissionDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<MissionDbContext>();

        // Update this connection string if needed
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Mission;Username=postgres;Password=admin123");

        return new MissionDbContext(optionsBuilder.Options);
    }
}
