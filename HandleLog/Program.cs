using HandleLog.Contexts;
using HandleLog.Models;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace HandleLog;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var configuration = builder.Configuration;
        var appSetting = AppSetting.MapValue(configuration);

        builder.Services.AddSingleton(appSetting);
        builder.Services.AddDbContext<LandingPageManagementDbContext>(x => x.UseSqlServer(appSetting.ConnectionString, options =>
        {
            options.EnableRetryOnFailure();
        }));

        // Add services to the container.
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        //if (app.Environment.IsDevelopment())
        //{
        app.UseSwagger();
        app.UseSwaggerUI();
        //}

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}