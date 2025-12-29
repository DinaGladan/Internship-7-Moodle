using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MoodleSystem.Application;
using MoodleSystem.Console.Views;

var builder = Host.CreateApplicationBuilder(args);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

builder.Services.AddApplication(builder.Configuration);

builder.Services.AddScoped<MenuManager>();

var host = builder.Build();

using (var scope = host.Services.CreateScope())
{
    var menuManager = scope.ServiceProvider.GetRequiredService<MenuManager>();
    await menuManager.RunAsync();
}