using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using Fullstack.Core;
using Fullstack.Model.Persistent;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ConfigurationBuilder configurationBuilder = new();
var configuration = configurationBuilder.AddJsonFile("appsettings.json").Build();
RegisterDefaultHttpClient(builder.Services);
RegisterDbContextFactory(configuration, builder.Services);
RegisterServices(builder.Services);

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();
return;

void RegisterDbContextFactory(IConfiguration cfg, IServiceCollection services)
{
	var connectionString = cfg.GetConnectionString("DefaultConnection");
	var dataSource = MyDbContext.BuildDataSource(connectionString);

	void OptionsAction(IServiceProvider sp, DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseNpgsql(dataSource);

		var loggerFactory = sp.GetRequiredService<ILoggerFactory>();
		optionsBuilder.UseLoggerFactory(loggerFactory);
	}

	services.AddDbContextFactory<MyDbContext>(OptionsAction);
	services.AddDbContext<MyDbContext>(OptionsAction);
}

static void RegisterDefaultHttpClient(IServiceCollection services)
{
	services.AddHttpClient(string.Empty, ConfigureHttpClient)
				.ConfigurePrimaryHttpMessageHandler(CreateDefaultHttpMessageHandler);
}

static void ConfigureHttpClient(HttpClient client)
{
	client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
	client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));
	client.Timeout = Timeout.InfiniteTimeSpan;
}

static HttpMessageHandler CreateDefaultHttpMessageHandler()
{
	return new HttpClientHandler {
		AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
		UseCookies = false,
		AllowAutoRedirect = false,
	};
}

void RegisterServices(IServiceCollection services)
{
	services.AddSingleton<IFrisbeeConditionsChecker, FrisbeeConditionsChecker>();
	services.AddSingleton<IOpenWeatherApiClient, OpenWeatherApiClient>();
}
