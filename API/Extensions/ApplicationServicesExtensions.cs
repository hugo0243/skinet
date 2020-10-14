using System.Linq;
using API.Errors;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
             services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped(typeof(IGenericRepository<>),
                               typeof(GenericRepository<>));
                               
            // Como el atributo [ApiController] realiza validaciones como tipo de parámetro
            // acá se configura la forma de su respuesta en caso de error, incluimos un
            // un arreglo de errores para hacerlo más manejable al cliente
            services.Configure<ApiBehaviorOptions>(options => 
            { 
                // con actionConttext podemos acceder our model state errors
                // lo que nuestro api controller usa para handle valid errors
                // como vamos a configurar un atributo de nuestro controlador esta configuración
                // se hace despues de agregar el servicio de controladores.
                options.InvalidModelStateResponseFactory = actionContext => 
                {
                    // como el model state es un dictionary usamos selectMany para flatten out
                    // los errores que estan en los values del diccionario.
                    var errors = actionContext.ModelState
                        .Where(e => e.Value.Errors.Count > 0)
                        .SelectMany(x => x.Value.Errors)
                        .Select(x => x.ErrorMessage).ToArray();

                    // populate nuestro apiValidation enumerable de errores y lo retorna como 
                    // bad request object, ya que el request es invalido por la data enviada.
                    var errorResponse = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });

            return services;
        }
    }
}