using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.eShopWeb.Infrastructure.Data;
using Microsoft.eShopWeb.Web.Pages.Admin;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Microsoft.eShopWeb.Web.Services
{
    public class TimedCrawlHostedService : IHostedService, IDisposable
    {
        private readonly ILogger _logger;
        private Timer _timer;
        private readonly IServiceProvider _provider;
        
        private List<ApplicationCore.Entities.CrawledItem> crawledList;

        public TimedCrawlHostedService(ILogger<TimedCrawlHostedService> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _provider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Crawl Products Background Service is starting. Loading every hours");

             _timer = new Timer(DoWork, null, TimeSpan.Zero,
                //TimeSpan.FromSeconds(15)
                TimeSpan.FromHours(1)
                //TimeSpan.FromMinutes(5)
                 );

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            _logger.LogInformation("Crawl Products Background Service is working.");

            TGDDCrawlerasync().Wait();
                        
            if (crawledList != null && crawledList.Count > 0)
            {
                using (IServiceScope scope = _provider.CreateScope())
                {
                    var _catalogContext = scope.ServiceProvider.GetRequiredService<CatalogContext>();
                    _catalogContext.CrawledItems.AddRange(crawledList);
                    _catalogContext.SaveChanges();
                    crawledList = null;
                }                  
            }
        }

        private async Task TGDDCrawlerasync()
        {
            //the url of the page we want to test
            var url = "https://www.thegioididong.com/dtdd-apple-iphone";
            var httpClient = new HttpClient();
            var htmlDocument = new HtmlDocument();
            var html = await httpClient.GetStringAsync(url);
            htmlDocument.LoadHtml(html);

            // a list to add all the list of cars and the various prices 
            if (crawledList == null)
            {
                crawledList = new List<ApplicationCore.Entities.CrawledItem>();
            }
            
            var divs = htmlDocument.DocumentNode.SelectNodes(@"//section[@class='cate cate42 filtered']/ul/li/a");

            foreach (var div in divs)
            {
                var modelItem = div.Descendants("h3").FirstOrDefault();
                string model = (modelItem == null) ? string.Empty : modelItem.InnerText;

                var priceItem = div.Descendants("div").Where(o => o.GetAttributeValue("class", "").Equals("price")).FirstOrDefault();

                string price = (priceItem == null || priceItem.Descendants("strong").FirstOrDefault() == null)
                             ? string.Empty
                             : priceItem.Descendants("strong").FirstOrDefault().InnerText;
                if (string.IsNullOrEmpty(price))
                {
                    price = "Đang cập nhật";
                }

                string link = "https://www.thegioididong.com" + div.GetAttributeValue("href", "");

                var imageItem = div.Descendants("img").FirstOrDefault();
                string image = string.Empty;
                if (imageItem != null)
                {
                    string dataorigin = imageItem.GetAttributeValue("data-original", "");
                    image = string.IsNullOrWhiteSpace(dataorigin)
                          ? imageItem.GetAttributeValue("src", "")
                          : dataorigin;
                }

                var item = new ApplicationCore.Entities.CrawledItem(model, price, link, image);

                if (!string.IsNullOrEmpty(item.Model))
                {
                    crawledList.Add(item);
                }

            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Crawl Products Background Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
