using System.Reflection;
using DeliveryOffice.Core.Models;
using Microsoft.OpenApi.Models;

namespace DeliveryOffice.API.Infrastructure;

/// <summary>
///     Предоставляет фукнционал для регистрации сервисов авто-документирования Swagger
/// </summary>
static internal class SwaggerDocExtensions
{
    /// <summary>
    ///     Регистриует сервис xml документирования
    /// </summary>
    public static void AddSwaggerGen(this IServiceCollection services)
    {
        services.AddSwaggerGen(
            options =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);

                options.SwaggerDoc($"{nameof(Supplier)}", new OpenApiInfo { Title = "Поставщики", Version = "v1", });
                options.SwaggerDoc($"{nameof(Bill)}", new OpenApiInfo { Title = "Чеки", Version = "v1", });
                options.SwaggerDoc($"{nameof(Product)}", new OpenApiInfo { Title = "Продукты", Version = "v1", });
                options.SwaggerDoc($"{nameof(Buyer)}", new OpenApiInfo { Title = "Покупатели", Version = "v1", });
            });
    }

    /// <summary>
    ///     Вносит параметры для UI элементов Swagger
    /// </summary>
    public static void UseSwaggerUI(this WebApplication web)
    {
        web.UseSwaggerUI(
            options =>
            {
                options.SwaggerEndpoint($"{nameof(Supplier)}/swagger.json", "Поставщики");
                options.SwaggerEndpoint($"{nameof(Bill)}/swagger.json", "Чеки");
                options.SwaggerEndpoint($"{nameof(Product)}/swagger.json", "Продукты");
                options.SwaggerEndpoint($"{nameof(Buyer)}/swagger.json", "Покупатели");
            });
    }
}
