using ChangeDB.Contexts;
using ChangeDB.Contexts.MongoDb;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text.Json.Serialization;

namespace ChangeDB
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.


            builder.Services.AddControllers().AddJsonOptions(_ =>
            {
                _.JsonSerializerOptions.WriteIndented = true;
                _.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddHttpClient();
            builder.Services.AddDbContext<FloorG783DbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString1"), op =>
                {
                    op.EnableRetryOnFailure();
                })
               .EnableSensitiveDataLogging() // Bật log dữ liệu nhạy cảm
               .LogTo(Console.WriteLine, LogLevel.Information)
               );
            builder.Services.AddSingleton<IMongoDbContext, MongoDbContext>();

            builder.Services.AddSingleton(FirebaseApp.Create(new AppOptions
            {
                Credential = GoogleCredential.FromFile($"{Path.Combine(Directory.GetCurrentDirectory())}\\firebaseDriver.json")
            }));

            builder.Services.AddDbContext<FloorG726DbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString2"), options =>
            {
                options.EnableRetryOnFailure();
            }));
            var app = builder.Build();

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
