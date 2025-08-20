using Microsoft.OpenApi.Models;
using Flights.Server.Data;
using Flights.Server.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Flights.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add dbcontext to the DI container
            builder.Services.AddDbContext<Entities>(options => 
            options.UseSqlServer(builder.Configuration.GetConnectionString("Flights"))
            );

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddSwaggerGen(c =>
            {
                c.DescribeAllParametersInCamelCase(); // i.e parameters passed fron the client 
                c.AddServer(new OpenApiServer
                {
                    Description = "Development server",
                    Url = "https://localhost:7076"
                }); // Add a server URL for Swagger UI to connect with angular app

                c.CustomOperationIds(e => $"{e.ActionDescriptor.RouteValues["action"] + e.ActionDescriptor.RouteValues["controller"]}");
            }); // Register Swagger generator

            // Adding scoped services to the DI container (which created an instance per request)
            builder.Services.AddScoped<Entities>();

            var app = builder.Build();

            // Creating a scope to seed the database with initial flight data
            var entities = app.Services.CreateScope().ServiceProvider.GetService<Entities>();
            entities.Database.EnsureCreated(); // Ensure the database is created
            var random = new Random();

            // This avoids duplicate flight data on every application start
            if (!entities.Flights.Any())
            {
                // Seed initial flight data
                Flight[] flightsToSeed = new Flight[]
                {
                    new (
                    Guid.NewGuid(),
                    "American Airlines",
                    random.Next(90, 5000).ToString(),
                    new TimePlace("Los Angeles",DateTime.Now.AddHours(random.Next(1, 3))),
                    new TimePlace("Istanbul",DateTime.Now.AddHours(random.Next(4, 10))),
                    2
                    ),
                    new (   Guid.NewGuid(),
                    "Deutsche BA",
                    random.Next(90, 5000).ToString(),
                    new TimePlace("Munchen",DateTime.Now.AddHours(random.Next(1, 10))),
                    new TimePlace("Schiphol",DateTime.Now.AddHours(random.Next(4, 15))),
                    random.Next(1, 853)),
                    new (   Guid.NewGuid(),
                    "British Airways",
                    random.Next(90, 5000).ToString(),
                    new TimePlace("London, England",DateTime.Now.AddHours(random.Next(1, 15))),
                    new TimePlace("Vizzola-Ticino",DateTime.Now.AddHours(random.Next(4, 18))),
                        random.Next(1, 853)),
                    new (   Guid.NewGuid(),
                    "Basiq Air",
                    random.Next(90, 5000).ToString(),
                    new TimePlace("Amsterdam",DateTime.Now.AddHours(random.Next(1, 21))),
                    new TimePlace("Glasgow, Scotland",DateTime.Now.AddHours(random.Next(4, 21))),
                        random.Next(1, 853)),
                    new (   Guid.NewGuid(),
                    "BB Heliag",
                    random.Next(90, 5000).ToString(),
                    new TimePlace("Zurich",DateTime.Now.AddHours(random.Next(1, 23))),
                    new TimePlace("Baku",DateTime.Now.AddHours(random.Next(4, 25))),
                        random.Next(1, 853)),
                    new (   Guid.NewGuid(),
                    "Adria Airways",
                    random.Next(90, 5000).ToString(),
                    new TimePlace("Ljubljana",DateTime.Now.AddHours(random.Next(1, 15))),
                    new TimePlace("Warsaw",DateTime.Now.AddHours(random.Next(4, 19))),
                        random.Next(1, 853)),
                    new (   Guid.NewGuid(),
                    "ABA Air",
                    random.Next(90, 5000).ToString(),
                    new TimePlace("Praha Ruzyne",DateTime.Now.AddHours(random.Next(1, 55))),
                    new TimePlace("Paris",DateTime.Now.AddHours(random.Next(4, 58))),
                        random.Next(1, 853)),
                    new (   Guid.NewGuid(),
                    "AB Corporate Aviation",
                    random.Next(90, 5000).ToString(),
                    new TimePlace("Le Bourget",DateTime.Now.AddHours(random.Next(1, 58))),
                    new TimePlace("Zagreb",DateTime.Now.AddHours(random.Next(4, 60))),
                        random.Next(1, 853))
                };

                entities.Flights.AddRange(flightsToSeed); // Seeding the db

                entities.SaveChanges(); // Save the changes to the in-memory db
            }

            app.UseCors(builder => builder
            .WithOrigins("*")
            .AllowAnyMethod()
            .AllowAnyHeader()
            ); // Allow requests from any origin, any method and any header (for development purposes only) 

            app.UseSwagger().UseSwaggerUI(); // Enable the Swagger middleware and UI

            app.UseDefaultFiles();
            app.UseStaticFiles();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.MapFallbackToFile("/index.html");

            app.Run();
        }
    }
}
