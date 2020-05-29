using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Infrastructure.Data
{
    public class CatalogContextSeed
    {
        public static async Task SeedAsync(CatalogContext catalogContext,
            ILoggerFactory loggerFactory, int? retry = 0)
        {
            int retryForAvailability = retry.Value;
            try
            {
                // TODO: Only run this if using a real database
                // context.Database.Migrate();

                if (!catalogContext.CatalogBrands.Any())
                {
                    catalogContext.CatalogBrands.AddRange(
                        GetPreconfiguredCatalogBrands());

                    await catalogContext.SaveChangesAsync();
                }

                if (!catalogContext.CatalogTypes.Any())
                {
                    catalogContext.CatalogTypes.AddRange(
                        GetPreconfiguredCatalogTypes());

                    await catalogContext.SaveChangesAsync();
                }

                if (!catalogContext.CatalogItems.Any())
                {
                    catalogContext.CatalogItems.AddRange(
                        GetPreconfiguredItems());

                    await catalogContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                if (retryForAvailability < 10)
                {
                    retryForAvailability++;
                    var log = loggerFactory.CreateLogger<CatalogContextSeed>();
                    log.LogError(ex.Message);
                    await SeedAsync(catalogContext, loggerFactory, retryForAvailability);
                }
                throw;
            }
        }

        static IEnumerable<CatalogBrand> GetPreconfiguredCatalogBrands()
        {
            return new List<CatalogBrand>()
            {
                new CatalogBrand("iPhone 11"),
                new CatalogBrand("iPhone X"),
                new CatalogBrand("iPhone SE"),
                new CatalogBrand("iPhone 8"),
                new CatalogBrand("iPhone 7"),
                new CatalogBrand("iPhone"),
            };
        }

        static IEnumerable<CatalogType> GetPreconfiguredCatalogTypes()
        {
            return new List<CatalogType>()
            {
                new CatalogType("apple.com"),
                new CatalogType("thegioididong.com"),
                new CatalogType("fpt.com.vn"),
                new CatalogType("viettelstore.vn"),
                new CatalogType("others")
            };
        }

        static IEnumerable<CatalogItem> GetPreconfiguredItems()
        {
            return new List<CatalogItem>()
            {
                new CatalogItem(1,1, "https://www.apple.com/shop/buy-iphone/iphone-11-pro", "iPhone 11 Pro", 50000000, "http://catalogbaseurltobereplaced/images/products/ip11.jpg"),
                new CatalogItem(1,1, "https://www.apple.com/shop/buy-iphone/iphone-11", "iPhone 11", 45000000,  "http://catalogbaseurltobereplaced/images/products/ip11.jpg"),
                new CatalogItem(1,2, "https://www.apple.com/shop/buy-iphone/iphone-xr","iPhone XR",  40000000, "http://catalogbaseurltobereplaced/images/products/ipxr.jpg"),
                new CatalogItem(1,3, "https://www.apple.com/shop/buy-iphone/iphone-se", "iPhone SE", 35000000 ,"http://catalogbaseurltobereplaced/images/products/ipse.jpg")
            };
        }
    }
}
