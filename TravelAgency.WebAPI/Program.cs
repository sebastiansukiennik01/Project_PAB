using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;
using TravelAgency.Application.Mappings;
using TravelAgency.Application.Services.Generic;
using TravelAgency.Application.Services.Interfaces;
using TravelAgency.Application.Validators.City;
using TravelAgency.Application.Validators.Country;
using TravelAgency.Application.Validators.Hotel;
using TravelAgency.Application.Validators.Offer;
using TravelAgency.Domain.Contracts;
using TravelAgency.Infrastructure;
using TravelAgency.Infrastructure.Repositories;
using TravelAgency.SharedKernel.Dto.City;
using TravelAgency.SharedKernel.Dto.Country;
using TravelAgency.SharedKernel.Dto.Hotel;
using TravelAgency.SharedKernel.Dto.Offer;
using TravelAgency.WebAPI.Middleware;

// Early init of NLog to allow startup and exception logging, before host is built
var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // NLog: Setup NLog for Dependency injection
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddAutoMapper(typeof(TravelAgencyMappingProfile));

    builder.Services.AddFluentValidationAutoValidation();

    var sqliteConnectionString = "Data Source=TravelAgency.WebAPI.database.db";
    builder.Services.AddDbContext<DatabaseContext>(options =>
        options.UseSqlite(sqliteConnectionString));

    builder.Services.AddScoped<IValidator<CreateCityDto>, RegisterCreateCityDtoValidator>();
    builder.Services.AddScoped<IValidator<UpdateCityDto>, RegisterUpdateCityDtoValidator>();

    builder.Services.AddScoped<IValidator<CreateCountryDto>, RegisterCreateCountryDtoValidator>();
    builder.Services.AddScoped<IValidator<UpdateCountryDto>, RegisterUpdateCountryDtoValidator>();

    builder.Services.AddScoped<IValidator<CreateHotelDto>, RegisterCreateHotelDtoValidator>();
    builder.Services.AddScoped<IValidator<UpdateHotelDto>, RegisterUpdateHotelDtoValidator> ();

    builder.Services.AddScoped<IValidator<CreateOfferDto>, RegisterCreateOfferDtoValidator>();
    builder.Services.AddScoped<IValidator<UpdateOfferDto>, RegisterUpdateOfferDtoValidator>();
    
    builder.Services.AddScoped<DataSeeder>();

    builder.Services.AddScoped<ITravelAgencyUnitOfWork, TravelAgencyUnitOfWork>();

    builder.Services.AddScoped<ICountryService, CountryService>();
    builder.Services.AddScoped<ICityService, CityService>();
    builder.Services.AddScoped<IHotelService, HotelService>();
    builder.Services.AddScoped<IOfferService, OfferService>();

    builder.Services.AddScoped<ICountryRepository, CountryRepository>();
    builder.Services.AddScoped<ICityRepository, CityRepository>();
    builder.Services.AddScoped<IHotelRepository, HotelRepository>();
    builder.Services.AddScoped<IOfferRepository, OfferRepository>();


    builder.Services.AddScoped<ExceptionMiddleware>();

    var app = builder.Build();

    app.UseStaticFiles();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseMiddleware<ExceptionMiddleware>();

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

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
