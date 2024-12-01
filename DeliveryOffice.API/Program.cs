using System.Reflection;
using DeliveryOffice.Core.Abstractions.Repositories;
using DeliveryOffice.DataAccess;
using DeliveryOffice.DataAccess.Repositories;
using DeliveryOffice.Services;
using DeliveryOffice.Services.Contracts.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DeliveryOffice.API;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);

            c.DocInclusionPredicate((docName, apiDesc) =>
            {
                if (!apiDesc.TryGetMethodInfo(out var methodInfo))
                {
                    return false;
                }
                var groupName = apiDesc.GroupName ?? string.Empty;
                return groupName.Equals(docName, StringComparison.OrdinalIgnoreCase);
            });
            c. SwaggerDoc("Управление поставщиками", new OpenApiInfo {Title = "Suppliers.API", Version = "v1"});
        });

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
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/Управление поставщиками/swagger.json", "Suppliers.API");
            });
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}
