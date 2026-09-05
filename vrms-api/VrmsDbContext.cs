using Microsoft.EntityFrameworkCore;
using vrms_api.Helper;
using static vrms_api.Domains.VrmsDomain;

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
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }

    }
    public static class DbContextSeedData
    {
        static readonly DateTime DefaultRowDate = new DateTime(2026, 8, 25);
        public static void InitSomeData(this ModelBuilder mb, IConfiguration configuration)
        {

        }
    }
}
