﻿using System.Reflection;
using DeliveryOffice.Core.Models;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DeliveryOffice.API.Infrastructure;

static internal class SwaggerDocExtension
{
    public static void AddSwaggerGen(this IServiceCollection services) =>
        services.AddSwaggerGen(options =>
        {
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);

            options.DocInclusionPredicate((docName, apiDesc) =>
            {
                if (!apiDesc.TryGetMethodInfo(out var methodInfo))
                {
                    return false;
                }

                var groupName = apiDesc.GroupName ?? string.Empty;
                return groupName.Equals(docName, StringComparison.OrdinalIgnoreCase);
            });
            options.SwaggerDoc($"{nameof(Supplier)}", new OpenApiInfo { Title = "Поставщики", Version = "v1" });
            options.SwaggerDoc($"{nameof(Bill)}", new OpenApiInfo { Title = "Чеки", Version = "v1" });
            options.SwaggerDoc($"{nameof(Product)}", new OpenApiInfo { Title = "Продукты", Version = "v1" });
            options.SwaggerDoc($"{nameof(Buyer)}", new OpenApiInfo { Title = "Покупатели", Version = "v1" });
        });

    public static void UseSwaggerUI(this WebApplication web) =>
        web.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint($"{nameof(Supplier)}/swagger.json", "Поставщики");
            options.SwaggerEndpoint($"{nameof(Bill)}/swagger.json", "Чеки");
            options.SwaggerEndpoint($"{nameof(Product)}/swagger.json", "Продукты");
            options.SwaggerEndpoint($"{nameof(Buyer)}/swagger.json", "Покупатели");
        });
}
