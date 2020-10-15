using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using Core.Entities;
using System;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext storeContext, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!storeContext.productBrands.Any())
                {
                    //Since this runs from the program class the route to the json is specified
                    //from there
                    string brandsData = File.ReadAllText(@"../Infrastructure/Data/SeedData/brands.json");
                    
                    List<ProductBrand> brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                    Action<ProductBrand> insertBrands = brand => storeContext.productBrands.Add(brand); 

                    brands.ForEach(insertBrands);

                    await storeContext.SaveChangesAsync();
                }

                if (!storeContext.productTypes.Any())
                {
                    string productTypes = File.ReadAllText(@"../Infrastructure/Data/SeedData/types.json");

                    var types = JsonSerializer.Deserialize<List<ProductType>>(productTypes);

                    void insertTypes(ProductType prodType) => storeContext.productTypes.Add(prodType);

                    types.ForEach(insertTypes);

                    await storeContext.SaveChangesAsync();
                }


                if (!storeContext.Products.Any())
                {
                    var productsData = File.ReadAllText(@"../Infrastructure/Data/SeedData/products.json");

                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                    products.ForEach(product => storeContext.Add(product));

                    await storeContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                ILogger<StoreContextSeed> logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}