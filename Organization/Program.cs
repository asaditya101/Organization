using Microsoft.EntityFrameworkCore;
using Organization.Contracts;
using Organization.Database;
using Organization.Models;
using Organization.Services;

public static class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        //Dependency Injections
        builder.Services.AddDbContext<EmployeeDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection")),
        contextLifetime: ServiceLifetime.Scoped, optionsLifetime: ServiceLifetime.Singleton);

        builder.Services.AddScoped<IEmployeeService, EmployeeService>();

        var app = builder.Build();

        //Prefill Employee table if empty.
        EmployeeData.AddEmployeeRows(app.Services);

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}