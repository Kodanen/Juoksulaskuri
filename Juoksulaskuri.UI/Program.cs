using Blazored.LocalStorage;
using Juoksulaskuri.UI;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using System.Diagnostics;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();

// Add singleton StateContainer for site-wide memory
builder.Services.AddSingleton<Juoksulaskuri.Core.UI.MemContainer>();
// Local storage
builder.Services.AddBlazoredLocalStorage();

await builder.Build().RunAsync();
