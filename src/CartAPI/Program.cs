using CartAPI.Actions.FindProduct;
using CartAPI.Actions.SaveToCart;
using CartAPI.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://localhost:7001/");

builder.Services.Configure<ProjectSettings>(builder.Configuration.GetSection(nameof(ProjectSettings)));

builder.Services.AddSingleton<IProjectSettings>(
    sp => sp.GetRequiredService<IOptions<ProjectSettings>>().Value);

builder.Services.AddSingleton<ICartService, CartService>();

builder.Services.AddTransient<IFindProductQuery, FindProductQuery>();
builder.Services.AddTransient<IAddToCartCommand, AddToCartCommand>();

builder.Services.AddHttpClient();

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    options.SerializerSettings.Converters.Add(new StringEnumConverter());
    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
    options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
    JsonConvert.DefaultSettings = () => options.SerializerSettings;
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();

app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.Run();