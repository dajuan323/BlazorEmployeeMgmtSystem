using Client.Extensions;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;



var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Registrars
builder.RegisterServices(typeof(Program));

await builder.Build().RunAsync();
