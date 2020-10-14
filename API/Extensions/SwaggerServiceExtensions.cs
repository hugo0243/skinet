using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace API.Extensions
{
    public static class SwaggerServiceExtensions
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
               services.AddSwaggerGen(swaggerOpts => 
            {
                // V1 representa la versión de la API
                swaggerOpts.SwaggerDoc("v1", new OpenApiInfo 
                { 
                    Title = "SkiNet API",
                    Version = "v1"
                });
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
        {
                 app.UseSwagger();
            // swagger ui nos permite hacer una página web con nuestra api documentada
            app.UseSwaggerUI(swaggerUIOpts => 
            {
                // El endpoint usado para nuestra api endpoints en formato json
                swaggerUIOpts.SwaggerEndpoint("/swagger/v1/swagger.json", "Skinet API v1");
            });

            return app;
        }
    }
}