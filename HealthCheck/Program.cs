using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;

namespace HealthCheck
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddHealthChecks()
                .AddSqlServer("Server=10.0.5.22,11433\\basql;Database=BakG7;User Id=g7;Password=g7123!@#;Integrated Security=False;", 
                    healthQuery: "select 1", 
                    name: "SQL Server g7bak", 
                    failureStatus: HealthStatus.Unhealthy, 
                    tags: new[] { "Feedback", "Database" });
            
            builder.Services.AddHealthChecksUI().AddInMemoryStorage();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/healthcheck");
                endpoints.MapHealthChecksUI(options =>
                {
                    options.UIPath = "/healthchecks-ui"; // Đường dẫn cho giao diện người dùng Health Checks UI
                });
            });

            //app.MapHealthChecks("/hc", new HealthCheckOptions()
            //{
            //    //Predicate = _ => true,
            //    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
            //});

            //app.MapHealthChecksUI(options => options.UIPath = "/hc-ui");

            //app.UseHealthChecks("/hc", new HealthCheckOptions
            //{
            //    ResponseWriter = async (c, r) =>
            //    {
            //        c.Response.ContentType = "application/json";

            //        var result = JsonConvert.SerializeObject(new
            //        {
            //            status = r.Status.ToString(),
            //            components = r.Entries.Select(e => new { key = e.Key, value = e.Value.Status.ToString() })
            //        });
            //        await c.Response.WriteAsync(result);
            //    }
            //});


            //app.UseHealthChecksUI(options =>
            //{
            //    options.UIPath = "/hc-ui"; // Đường dẫn cho giao diện người dùng Health Checks UI
            //});

            app.MapControllers();

            app.Run();
        }
    }
}
