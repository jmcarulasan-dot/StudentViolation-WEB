using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using StudentViolationWeb;
using StudentViolationWeb.Data;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddBlazoredLocalStorage();
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<StudentService>();
builder.Services.AddScoped<GuardService>();
builder.Services.AddScoped<GuidanceService>();
builder.Services.AddScoped<SaoService>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://192.168.98.11:5277/") });

await builder.Build().RunAsync();