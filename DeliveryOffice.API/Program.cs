using DeliveryOffice.Core.Abstractions.Repositories;
using DeliveryOffice.DataAccess;
using DeliveryOffice.DataAccess.Repositories;
using DeliveryOffice.Services;
using DeliveryOffice.Services.Models.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace DeliveryOffice.API;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
        builder.Services.AddScoped<ISuppliersService, SuppliersService>();
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
