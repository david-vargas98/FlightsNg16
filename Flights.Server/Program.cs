using Microsoft.OpenApi.Models;
using Flights.Server.Data;

namespace Flights.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddSwaggerGen(c =>
            {
                c.AddServer(new OpenApiServer
                {
                    Description = "Development server",
                    Url = "https://localhost:7076"
                }); // Add a server URL for Swagger UI to connect with angular app

                c.CustomOperationIds(e => $"{e.ActionDescriptor.RouteValues["action"] + e.ActionDescriptor.RouteValues["controller"]}");
            }); // Register Swagger generator

            // Adding singleton Entities to the DI container
            builder.Services.AddSingleton<Entities>();

            var app = builder.Build();

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
