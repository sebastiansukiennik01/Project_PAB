using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TravelAgency.BlazorClient;
using TravelAgency.BlazorClient.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress =
     new Uri(builder.Configuration.GetValue<string>("TravelAgencyAPIUrl"))
});

builder.Services.AddScoped<IOfferService, OfferService>();

await builder.Build().RunAsync();
