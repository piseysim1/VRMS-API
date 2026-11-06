using Microsoft.EntityFrameworkCore;
using vrms_api.Domains;
using vrms_api.Helper;
using static vrms_api.Domains.SettingDomain;
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
                entityType.SetTableName($"vrms_{entityType.GetTableName()}");
            }
        }
        // Setting
        public DbSet<LookUpType> LookUpTypes { get; set; }
        public DbSet<LookUpItem> LookUpItems { get; set; }

        // Core
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<LoginSession> LoginSessions { get; set; }

        public DbSet<Booking> Bookings { get; set; }
        public DbSet<BookingDetail> BookingDetails { get; set; }

    }
    public static class DbContextSeedData
    {
        static readonly DateTime DefaultRowDate = new DateTime(2026, 10, 5);
        public static void InitSomeData(this ModelBuilder mb, IConfiguration configuration)
        {
            mb.Entity<Role>().HasData(
                new Role
                {
                    Id = (long)RoleEnum.Admin,
                    Name = RoleNames.Admin,
                    Note = "Administrator",
                    Active = true,
                    CreatedAt = DefaultRowDate
                },
                new Role
                {
                    Id = (long)RoleEnum.User,
                    Name = RoleNames.User,
                    Note = "Normal User",
                    Active = true,
                    CreatedAt = DefaultRowDate
                }
            );
        }
    }
}
