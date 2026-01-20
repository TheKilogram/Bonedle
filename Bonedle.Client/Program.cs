using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Bonedle.Client;
using Bonedle.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Register services
builder.Services.AddScoped<IStorageService, StorageService>();
builder.Services.AddSingleton<IBoneDataService, BoneDataService>();
builder.Services.AddScoped<IDailyPuzzleService, DailyPuzzleService>();
builder.Services.AddScoped<IGameService, GameService>();

await builder.Build().RunAsync();
