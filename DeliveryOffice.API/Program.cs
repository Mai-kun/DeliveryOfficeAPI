using DeliveryOffice.API.Infrastructure;
using DeliveryOffice.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace DeliveryOffice.API;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers(options => options.Filters.Add<ApiExceptionFilter>());
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDependence();

        builder.Services.AddDbContext<DeliveryOfficeDbContext>(
            options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString(nameof(DeliveryOfficeDbContext)));
            });

        var app = builder.Build();

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
