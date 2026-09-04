using Microsoft.EntityFrameworkCore;
using vrms_api.Logics;

namespace vrms_api.Extensions
{
	public static class BuilderExtension
	{
        public static void AddLogics(this WebApplicationBuilder builder)
        {
            var logics = AppDomain.CurrentDomain.GetAssemblies()
                       .SelectMany(t => t.GetTypes())
                       .Where(t => t.IsClass && t.Namespace == typeof(ShareLogic).Namespace && t.CustomAttributes?.Count() == 0 && t.Name != "SharedLogic");
            foreach (var logic in logics)
            {
                builder.Services.AddScoped(logic);
            }
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<VrmsDbContext>(x => x.UseNpgsql(connectionString)); ;
        }
    }
}

