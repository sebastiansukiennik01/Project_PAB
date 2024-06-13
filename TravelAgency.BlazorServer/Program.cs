
using NLog;
using NLog.Web;

using FluentValidation;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using TravelAgency.BlazorServer.Data;
using TravelAgency.Domain;
using TravelAgency.Application.Mappings;
using TravelAgency.Infrastructure;
using TravelAgency.Application.Validators;
using TravelAgency.SharedKernel.Dto.City;
using TravelAgency.Application.Validators.City;
using TravelAgency.Domain.Contracts;
using TravelAgency.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using TravelAgency.SharedKernel.Dto.Country;
using TravelAgency.SharedKernel.Dto.Hotel;
using TravelAgency.Application.Validators.Hotel;

var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddRazorPages();
    builder.Services.AddServerSideBlazor();
    builder.Services.AddSingleton<WeatherForecastService>();

    // rejestracja automappera w kontenerze IoC
    builder.Services.AddAutoMapper(typeof(TravelAgencyMappingProfile));

    // rejestracja kontekstu bazy w kontenerze IoC
    var sqliteConnectionString = "Data Source=Kiosk.WebAPI.Logger.db";
    builder.Services.AddDbContext<DatabaseContext>(options =>
        options.UseSqlite(sqliteConnectionString));

    // rejestracja walidatora
    //builder.Services.AddScoped<IValidator<CreateCityDto>, RegisterCreateCityDtoValidator>();
    //builder.Services.AddScoped<IValidator<UpdateCityDto>, RegisterUpdateCityDtoValidator>();
    //builder.Services.AddScoped<IValidator<CreateHotelDto>, RegisterCreateHotelDtoValidator>();
    //builder.Services.AddScoped<IValidator<UpdateHotelDto>, RegisterUpdateHotelDtoValidator>();

    builder.Services.AddScoped<ITravelAgencyUnitOfWork, TravelAgencyUnitOfWork>();
    builder.Services.AddScoped<ICityRepository, CityRepository>();
    builder.Services.AddScoped<DataSeeder>();
    builder.Services.AddScoped<ICountryRepository, CountryRepository>();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();

    app.UseStaticFiles();

    app.UseRouting();

    app.MapBlazorHub();
    app.MapFallbackToPage("/_Host");

    // seeding data
    using (var scope = app.Services.CreateScope())
    {
        var dataSeeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
        dataSeeder.Seed();
    }

    app.Run();
}
catch (Exception exception)
{
    // NLog: catch setup errors
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    NLog.LogManager.Shutdown();
}

