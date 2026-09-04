using AutoMapper;
using vrms_api.Configs;
using vrms_api.Extensions;
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json").AddEnvironmentVariables();
// Add services to the container.
builder.AddLogics();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

// Add AutoMapper
// Get a logger factory from the host builder
using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

var config = new MapperConfiguration(cfg => {
    cfg.AddProfile<AutoMapperConfig>();

}, loggerFactory); // Pass the logger factory as the 2nd argument

IMapper mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
