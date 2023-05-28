using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using VeterinaryClinic.Api;
using VeterinaryClinic.DataAccess;
using Veterinary—linic.Handlers.Installers;
using Veterinary—linic.Repositories.Implementation;
using VeterinaryClinic.Core.Extensions;
using VeterinaryClinic.Api.Filters.GlobalErrorHandling;
using AspNetCoreRateLimit;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .WriteTo.Console()
    .WriteTo.MSSqlServer(
        builder.Configuration.GetConnectionString(ApplicationDbContext.ConnectionStringKey),
        sinkOptions: new MSSqlServerSinkOptions
        {
            TableName = "Logs",
            SchemaName = "dbo",
            AutoCreateSqlTable = true
        },
        restrictedToMinimumLevel: Enum.Parse<LogEventLevel>(
            builder.Configuration["Logging:LogLevel:Default"] ??
            throw new Exception("Cannot find 'Logging:LogLevel:Default'")))
    .CreateLogger();

builder.Services.AddSingleton(Log.Logger);
builder.Services.AddMemoryCache();
builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
builder.Services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
builder.Services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
builder.Services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
builder.Services.AddInMemoryRateLimiting();
builder.Logging.AddSerilog();
builder.Services.AddInstallersFromAssemblies(builder.Configuration,
    typeof(ApplicationDbContext), typeof(RepositoryManager),
    typeof(HandlersInstaller), typeof(ApiAssemblyMarker));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
app.UseMiddleware<GlobalErrorHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseIpRateLimiting();
app.UseAuthentication();
app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();

