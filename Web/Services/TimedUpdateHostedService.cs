using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.Infrastructure.Data;
using Microsoft.eShopWeb.Web.Pages.Admin;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Microsoft.eShopWeb.Web.Services
{
    public class TimedUpdateHostedService : IHostedService, IDisposable
    {
        private readonly ILogger _logger;
        private Timer _timer;
        private readonly IServiceProvider _provider;

        public TimedUpdateHostedService(ILogger<TimedUpdateHostedService> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _provider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Update Products Background Service is starting. Loading every hours");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                //TimeSpan.FromSeconds(15)
                TimeSpan.FromHours(5)
                //TimeSpan.FromMinutes(30)
                );

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            _logger.LogInformation("Update Products Background Service is working.");

            using (IServiceScope scope = _provider.CreateScope())
            {
                var _catalogContext = scope.ServiceProvider.GetRequiredService<CatalogContext>();

                var itemList = _catalogContext.CrawledItems
                                //.GroupBy(o => new { o.Link, o.ImageUrl })
                                .ToList();
                if (itemList !=null)
                {
                    var mergedList = itemList
                                    .OrderByDescending(a => a.CrawledTime)
                                    .GroupBy(a => a.Link)
                                    .SelectMany(b => b);                   

                    var catalogList = _catalogContext.CatalogItems.Select(o => o).ToList<CatalogItem>();

                    foreach (var item in mergedList)
                    {
                        string name = item.Model;
                        string description = item.Link;
                        decimal pricee = 0;
                        string priceText = FormatPrice(item.Price, out pricee);
                        var catalogItem = catalogList.Where(o => o.Name.Equals(name) && o.Description.Equals(description)).FirstOrDefault();

                        if (catalogItem != null)
                        {
                            if (catalogItem.Price != pricee)
                            {
                                catalogItem.Update(catalogItem.Name, pricee);
                            }
                        }
                        else
                        {
                            int catalogType = GetCatalogType(description);
                            int catalogBrand = GetCatalogBrand(name);
                            catalogItem = new CatalogItem(catalogType, catalogBrand, description, name, pricee, item.ImageUrl);
                            catalogList.Add(catalogItem);
                        }
                    }

                    _catalogContext.CatalogItems.UpdateRange(catalogList);
                    _catalogContext.SaveChanges();
                }
                
            }

        }
        
        private int GetCatalogType(string link)
        {
            int catalogType = 0;
            /*
                new CatalogType("apple.com"),
                new CatalogType("thegioididong.com"),
                new CatalogType("fpt.com.vn"),
                new CatalogType("viettelstore.vn"),
                new CatalogType("others")
             */
            if (!string.IsNullOrWhiteSpace(link))
            {
                if (link.Contains("apple.com", StringComparison.OrdinalIgnoreCase))
                {
                    catalogType = 1;
                }
                else if (link.Contains("thegioididong.com", StringComparison.OrdinalIgnoreCase))
                {
                    catalogType = 2;
                }
                else if (link.Contains("fpt.com.vn", StringComparison.OrdinalIgnoreCase))
                {
                    catalogType = 3;
                }
                else if (link.Contains("viettelstore.vn", StringComparison.OrdinalIgnoreCase))
                {
                    catalogType = 4;
                }
                else
                {
                    catalogType = 5;
                }
            }
            return catalogType;
        }
        private int GetCatalogBrand(string name)
        {
            int catalogBrand = 0;
            /*
                new CatalogBrand("iPhone 11"),
                new CatalogBrand("iPhone X"),
                new CatalogBrand("iPhone SE"),
                new CatalogBrand("iPhone 8"),
                new CatalogBrand("iPhone 7"),
                new CatalogBrand("iPhone")
             */
            if (!string.IsNullOrWhiteSpace(name) && name.Contains("iPhone", StringComparison.OrdinalIgnoreCase))
            {
                if (name.Contains("11", StringComparison.OrdinalIgnoreCase))
                {
                    catalogBrand = 1;
                }
                else if (name.Contains("X", StringComparison.OrdinalIgnoreCase))
                {
                    catalogBrand = 2;
                }
                else if (name.Contains("SE", StringComparison.OrdinalIgnoreCase))
                {
                    catalogBrand = 3;
                }
                else if (name.Contains("8", StringComparison.OrdinalIgnoreCase))
                {
                    catalogBrand = 4;
                }
                else if (name.Contains("7", StringComparison.OrdinalIgnoreCase))
                {
                    catalogBrand = 5;
                }
                else
                {
                    catalogBrand = 6;
                }
            }

            return catalogBrand;
        }
        private string FormatPrice(string priceText, out decimal priceNumber)
        {
            string priceReturn = priceText
                                .Substring(0, priceText.Length-1)
                                .Replace(".", "").Replace(",", "").Trim();
            Decimal.TryParse(priceReturn, out priceNumber);

            return priceReturn;
        }
        private bool FormatName(string inputName, out string outputName)
        {
            string nameFormat = "iPhone {0} {1} {2}";
            outputName = inputName;
            return true;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Update Products Background Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
