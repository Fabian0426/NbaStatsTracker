using MediatR;
using NbaStatsTrackerBackend.Application;
using NbaStatsTrackerBackend.Application.UseCases.GetAllTeams;
using NbaStatsTrackerBackend.Application.Interfaces; 
using NbaStatsTrackerBackend.Infrastructure.Config;
using NbaStatsTrackerBackend.Infrastructure.Http;

var builder = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);

builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection(ApiSettings.SectionName));

builder.Services.AddControllers();

builder.Services.AddHttpClient<IBalldontlieApiClient, BalldontlieApiClient>(); 

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(NbaStatsTrackerBackend.Application.DependencyInjection).Assembly));

var app = builder.Build();

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

app.Run();
