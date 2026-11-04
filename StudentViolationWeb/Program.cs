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
builder.Services.AddScoped<AdmissionService>();
builder.Services.AddScoped<SaoOperationsService>();
// The web client runs on this PC with the API's local HTTPS development profile.
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7001/") });

await builder.Build().RunAsync();
