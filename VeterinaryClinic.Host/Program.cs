using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using VeterinaryClinic.Api;
using VeterinaryClinic.Api.Filters;
using VeterinaryClinic.DataAccess;
using Veterinary—linic.Handlers.Installers;
using Veterinary—linic.Repositories.Implementation;
using VeterinaryClinic.Core.Extensions; 

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
builder.Logging.AddSerilog();
builder.Services.AddControllers();
builder.Services.AddInstallersFromAssemblies(builder.Configuration,
    typeof(ApplicationDbContext), typeof(RepositoryManager),
    typeof(HandlersInstaller), typeof(ApiAssemblyMarker));

var app = builder.Build();
app.UseMiddleware<GlobalErrorHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();

