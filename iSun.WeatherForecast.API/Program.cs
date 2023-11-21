using iSun.WeatherForecast.Application;
using iSun.WeatherForecast.Infrastructure;
using iSun.WeatherForecast.SharedKernel;
using iSun.WeatherForecast.SharedKernel.ApplicationContracts;
using iSun.WeatherForecast.SharedKernel.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Diagnostics;
using System.Net;

namespace iSun.WeatherForecast.API
{
    public partial class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
            {
                loggerConfiguration
                    .Enrich.FromLogContext()
                    .WriteTo.Console()
                    .WriteTo.File("logs/isun-weather-logs.txt", rollingInterval: RollingInterval.Hour); // Log to a file hourly
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                           options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationDbContext")));
            builder.Services.AddScoped<IWeatherDataApplicationService, WeatherDataApplicationService>();

            builder.Services.AddServices();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "iSun Weather Forecast Application", Version = "v1" });
            });

            var app = builder.Build();

            Serilog.ILogger logger = Log.Logger;
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        var traceId = Activity.Current?.Id ?? context.TraceIdentifier;

                        var problemDetails = new ProblemDetails();
                        problemDetails.Extensions.Add("traceId", traceId);

                        if (contextFeature.Error is BadRequestException || contextFeature.Error is NotFoundException)
                        {
                            var badRequest = context.Response.StatusCode = StatusCodes.Status400BadRequest;
                            if (badRequest == (int)HttpStatusCode.BadRequest)
                            {
                                problemDetails.Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1";
                                problemDetails.Title = contextFeature.Error.Message;
                                logger.Error(contextFeature.Error, $"Bad request {badRequest}. TraceId: {traceId}");
                            }
                            else
                            {
                                problemDetails.Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1";
                                problemDetails.Title = contextFeature.Error.Message;
                                logger.Error(contextFeature.Error, $"Not found. TraceId: {traceId}");
                            }
                        }
                        else
                        {
                            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                            problemDetails.Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1";
                            problemDetails.Title = iSunResources.GenericUnhandledExceptionMessage;

                            logger.Error(contextFeature.Error, $"Internal server error. TraceId: {traceId}");
                        }
                        problemDetails.Status = context.Response.StatusCode;

                        await context.Response.WriteAsJsonAsync(problemDetails);
                    }
                });
            });

            app.UseStatusCodePages();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}