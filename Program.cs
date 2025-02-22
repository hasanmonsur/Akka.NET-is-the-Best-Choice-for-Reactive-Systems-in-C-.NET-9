using AgriMarketAnalysis.Actors;
using AgriMarketAnalysis.Data;
using Akka.Actor;
using Akka.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Register Akka.NET ActorSystem with dependency injection
builder.Services.AddSingleton(provider =>
{
    var bootstrap = BootstrapSetup.Create();
    var di = DependencyResolverSetup.Create(provider);
    var actorSystemSetup = bootstrap.And(di);
    return ActorSystem.Create("AgriMarketSystem", actorSystemSetup);
});

// Register MarketDataProcessorActor with dependency injection
builder.Services.AddSingleton(provider =>
{
    var actorSystem = provider.GetRequiredService<ActorSystem>();
    var props = DependencyResolver.For(actorSystem).Props<MarketDataProcessorActor>();
    return actorSystem.ActorOf(props, "marketDataProcessor");
});

// Register other services
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Swagger services
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "My API", Version = "v1" });
});

var app = builder.Build();

// Enable Swagger middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1"));
}
app.UseAuthentication();
app.UseAuthorization();
app.UseRouting();
app.MapControllers();
app.Run();