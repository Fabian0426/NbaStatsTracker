using MediatR;
using NbaStatsTrackerBackend.Application.UseCases.GetAllTeams;
using NbaStatsTrackerBackend.Infrastructure.Http;

var builder = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHttpClient<BalldontlieApiClient>(client =>
{
    client.BaseAddress = new Uri("https://balldontlie.io/");
});

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblyContaining<GetAllTeamsRequest>());

var app = builder.Build();

app.Urls.Add("http://0.0.0.0:5000"); 
app.MapControllers();
app.Run();