using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("ocelot.json", false, true);

builder.Services.AddOcelot();

builder.WebHost.UseUrls("http://localhost:7000/");

var app = builder.Build();

if (!app.Environment.IsDevelopment()) app.UseDeveloperExceptionPage();

app.UseRouting();

await app.UseOcelot();

app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.Run();