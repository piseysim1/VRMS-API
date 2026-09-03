using Microsoft.EntityFrameworkCore;
using vrms_api.Helper;

namespace vrms_api
{
    public class VrmsDbContext(IConfiguration configuration) : DbContext
    {
        private readonly IConfiguration _configuration = configuration;
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql(_configuration.GetConnectionString(DatabaseConfigKeys.ConnectionString),
                x => x.MigrationsHistoryTable(DatabaseConfigKeys.VrmsVersion));
            base.OnConfiguring(options);

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.InitSomeData(_configuration);
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                entityType.SetTableName($"Vrms_{entityType.GetTableName()}");
            }
        }
        
    }
    public static class DbContextSeedData
    {
        static readonly DateTime DefaultRowDate = new DateTime(2026, 8, 25);
        public static void InitSomeData(this ModelBuilder mb, IConfiguration configuration)
        {

        }
    }
}
