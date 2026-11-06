using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
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
        public static void AddSwaggerInfo(this WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            var assembly = Assembly.GetExecutingAssembly();
            var fileInfo = new FileInfo(assembly.Location);
            var info = new OpenApiInfo()
            {
                Title = "vrms-api",
                Description = $"Latest update: {fileInfo.LastWriteTime:o}",
                Version = "v1"
            };

            builder.Services.AddSwaggerGen(o =>
            {
                o.SwaggerDoc("v1", info);

                o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter your JWT token into the field as: Bearer {your_token}",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                o.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });

                var prefix = builder.Configuration.GetSection("Swagger:RoutePrefix").Value;
                if (!string.IsNullOrEmpty(prefix))
                {
                    o.DocumentFilter<RoutePrefixDocumentFilter>(prefix.ToLower());
                }
            });
        }
        public class RoutePrefixDocumentFilter(string routePrefix) : IDocumentFilter
        {
            private readonly string _routePrefix = routePrefix;

            // Append the prefix to the existing paths
            public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
            {
                var paths = new OpenApiPaths();
                foreach (var path in swaggerDoc.Paths)
                {
                    paths.Add($"/{_routePrefix}{path.Key}", path.Value);
                }
                swaggerDoc.Paths = paths;
            }
        }
    }
}

