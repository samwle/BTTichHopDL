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

        private static async Task VTSCralerAsync()
        {
            try
            {
                //the url of the page we want to test
                var url = "https://viettelstore.vn/iphone";
                var httpClient = new HttpClient();
                var htmlDocument = new HtmlDocument();
                var html = await httpClient.GetStringAsync(url);
                htmlDocument.LoadHtml(html);

                // a list to add all the list of cars and the various prices 
                if (crawledList == null)
                {
                    crawledList = new List<CrawlItem>();
                }
                var divs = htmlDocument.DocumentNode.SelectNodes(@"//selection[@class='item ProductList3Col_item']");

                foreach (var div in divs)
                {
                    var modelItem = div.Descendants("h2").FirstOrDefault();
                    string model = (modelItem == null) ? string.Empty : modelItem.InnerText;

                    var priceItem = div.Descendants("span").Where(o => o.GetAttributeValue("class", "").Equals("price")).FirstOrDefault();
                    string price = priceItem.InnerText;

                    string link = div.GetAttributeValue("href", "");
                    var imageItem = div.Descendants("img").Where(o => o.GetAttributeValue("id", "").Equals("imgSeo_2")).FirstOrDefault();
                    string image = imageItem.GetAttributeValue("src", "");

                    var car = new CrawlItem
                    {
                        Model = model,
                        Price = price,
                        Link = link,
                        ImageUrl = image
                    };

                    if (!string.IsNullOrEmpty(car.Model))
                    {
                        crawledList.Add(car);
                    }

                }
                Console.WriteLine("VTSCralerAsync done");
            }
            catch (Exception)
            {

            }
        }

        private static async Task DMXCralerAsync()
        {
            //the url of the page we want to test
            var url = "https://www.dienmayxanh.com/dien-thoai-apple-iphone";
            var httpClient = new HttpClient();
            var htmlDocument = new HtmlDocument();
            var html = "";
            try
            {
                html = await httpClient.GetStringAsync(url);
            }
            catch (Exception)
            {
                return;
            }
            htmlDocument.LoadHtml(html);

            // a list to add all the list of cars and the various prices 
            if (crawledList == null)
            {
                crawledList = new List<CrawlItem>();
            }
            //var cars = new List<Car>();
            //var divs = htmlDocument.DocumentNode.Descendants("ul").FirstOrDefault().Descendants("li").ToList();
            var divs = htmlDocument.DocumentNode.SelectNodes(@"//div[@id='product-list']/ul/");

            foreach (var div in divs)
            {



            }
            Console.WriteLine("DMXCralerAsync done");
        }

        private static async Task WSSCralerAsync()
        {
            try
            {
                //the url of the page we want to test
                var url = "https://websosanh.vn/dien-thoai-iphone/cat-89.htm";
                var httpClient = new HttpClient();
                var htmlDocument = new HtmlDocument();
                var html = await httpClient.GetStringAsync(url);

                htmlDocument.LoadHtml(html);


                // a list to add all the list of cars and the various prices 
                if (crawledList == null)
                {
                    crawledList = new List<CrawlItem>();
                }
                //var cars = new List<Car>();
                //var divs = htmlDocument.DocumentNode.Descendants("ul").FirstOrDefault().Descendants("li").ToList();
                var divs = htmlDocument.DocumentNode.SelectNodes(@"//li[@class='product-item row-col']");

                foreach (var div in divs)
                {
                    var modelItem = div.Descendants("h3").FirstOrDefault();
                    string model = (modelItem == null) ? string.Empty : modelItem.InnerText;

                    var priceItem = div.Descendants("span").Where(o => o.GetAttributeValue("class", "").Equals("product-price")).FirstOrDefault();
                    string price = priceItem.InnerText.Split(new string[1] { "Giá từ " }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();

                    string link = div.GetAttributeValue("href", "");
                    var imageItem = div.Descendants("span").Where(o => o.GetAttributeValue("class", "").Equals("product-img")).FirstOrDefault().Descendants("img").FirstOrDefault();
                    string image = imageItem.GetAttributeValue("src", "");

                    var car = new CrawlItem
                    {
                        Model = model,
                        Price = price,
                        Link = link,
                        ImageUrl = image
                    };

                    if (!string.IsNullOrEmpty(car.Model))
                    {
                        crawledList.Add(car);
                    }

                }
                Console.WriteLine("WSSCralerAsync done");
            }
            catch (Exception)
            {

            }
        }

        private static async Task FPTSCralerAsync()
        {
            try
            {
                //the url of the page we want to test
                var url = "https://fptshop.com.vn/dien-thoai/apple-iphone";
                var httpClient = new HttpClient();
                var htmlDocument = new HtmlDocument();
                var html = await httpClient.GetStringAsync(url);

                htmlDocument.LoadHtml(html);


                // a list to add all the list of cars and the various prices 
                if (crawledList == null)
                {
                    crawledList = new List<CrawlItem>();
                }
                //var cars = new List<Car>();
                //var divs = htmlDocument.DocumentNode.Descendants("ul").FirstOrDefault().Descendants("li").ToList();
                var divs = htmlDocument.DocumentNode.SelectNodes(@"//div[@class='fs-lpil']");

                foreach (var div in divs)
                {
                    var modelItem = div.Descendants("h3").FirstOrDefault().Descendants("a").FirstOrDefault();
                    string model = (modelItem == null) ? string.Empty : modelItem.InnerText;

                    var priceItem = div.Descendants("div").Where(o => o.GetAttributeValue("class", "").Equals("fs-lpil-price")).FirstOrDefault();
                    string price = priceItem.Descendants("p").FirstOrDefault().InnerText;

                    string link = div.GetAttributeValue("href", "");
                    var imageItem = div.Descendants("img").Where(o => o.GetAttributeValue("class", "").Equals("lazy")).FirstOrDefault();
                    string image = imageItem.GetAttributeValue("data-original", "");

                    var car = new CrawlItem
                    {
                        Model = model,
                        Price = price,
                        Link = link,
                        ImageUrl = image
                    };

                    if (!string.IsNullOrEmpty(car.Model))
                    {
                        crawledList.Add(car);
                    }

                }
                Console.WriteLine("FPTSCralerAsync done");
            }
            catch (Exception)
            {

            }
        }

        private static async Task FSSTDCralerAsync()
        {
            try
            {
                //the url of the page we want to test
                var url = "https://fstudiobyfpt.com.vn/iphone";
                var httpClient = new HttpClient();
                var htmlDocument = new HtmlDocument();
                var html = await httpClient.GetStringAsync(url);

                htmlDocument.LoadHtml(html);

                // a list to add all the list of cars and the various prices 
                if (crawledList == null)
                {
                    crawledList = new List<CrawlItem>();
                }
                //var cars = new List<Car>();
                //var divs = htmlDocument.DocumentNode.Descendants("ul").FirstOrDefault().Descendants("li").ToList();
                var divs = htmlDocument.DocumentNode.SelectNodes(@"//div[@class='fs-ctitem']");

                foreach (var div in divs)
                {
                    var modelItem = div.Descendants("h3").FirstOrDefault().Descendants("a").FirstOrDefault();
                    string model = (modelItem == null) ? string.Empty : modelItem.InnerText;

                    var priceItem = div.Descendants("p").Where(o => o.GetAttributeValue("class", "").Equals("fs-ctpri")).FirstOrDefault();
                    string price = priceItem.InnerText.Split(new string[1] { "Giá chỉ " }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();

                    string link = div.GetAttributeValue("href", "");
                    var imageItem = div.Descendants("img").FirstOrDefault();
                    string image = "https://fstudiobyfpt.com.vn" + imageItem.GetAttributeValue("src", "");

                    var car = new CrawlItem
                    {
                        Model = model,
                        Price = price,
                        Link = link,
                        ImageUrl = image
                    };

                    if (!string.IsNullOrEmpty(car.Model))
                    {
                        crawledList.Add(car);
                    }

                }
                Console.WriteLine("FSSTDCralerAsync done");
            }
            catch (Exception)
            {

            }
        }

        private static async Task TikiCralerAsync()
        {
            try
            {
                //the url of the page we want to test
                var url = "https://tiki.vn/dien-thoai-smartphone/c1795/apple";
                var httpClient = new HttpClient();
                var htmlDocument = new HtmlDocument();
                var html = await httpClient.GetStringAsync(url);

                htmlDocument.LoadHtml(html);

                // a list to add all the list of cars and the various prices 
                if (crawledList == null)
                {
                    crawledList = new List<CrawlItem>();
                }
                //var cars = new List<Car>();
                //var divs = htmlDocument.DocumentNode.Descendants("ul").FirstOrDefault().Descendants("li").ToList();
                var divs = htmlDocument.DocumentNode.SelectNodes(@"//div[@class='product-item       ']");

                foreach (var div in divs)
                {
                    var modelItem = div.Descendants("h3").FirstOrDefault().Descendants("a").FirstOrDefault();
                    string model = (modelItem == null) ? string.Empty : modelItem.InnerText;

                    var priceItem = div.Descendants("p").Where(o => o.GetAttributeValue("class", "").Equals("fs-ctpri")).FirstOrDefault();
                    string price = priceItem.InnerText.Split(new string[1] { "Giá chỉ " }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();

                    string link = div.GetAttributeValue("href", "");
                    var imageItem = div.Descendants("img").FirstOrDefault();
                    string image = "https://fstudiobyfpt.com.vn" + imageItem.GetAttributeValue("src", "");

                    var car = new CrawlItem
                    {
                        Model = model,
                        Price = price,
                        Link = link,
                        ImageUrl = image
                    };

                    if (!string.IsNullOrEmpty(car.Model))
                    {
                        crawledList.Add(car);
                    }

                }
                Console.WriteLine("TikiCralerAsync done");
            }
            catch (Exception)
            {

            }
        }

        private static async Task PicoCralerAsync()
        {
            try
            {
                //the url of the page we want to test
                var url = "https://pico.vn/dien-thoai-di-dong/apple-cid75-ma13";
                var httpClient = new HttpClient();
                var htmlDocument = new HtmlDocument();
                var html = await httpClient.GetStringAsync(url);

                htmlDocument.LoadHtml(html);

                // a list to add all the list of cars and the various prices 
                if (crawledList == null)
                {
                    crawledList = new List<CrawlItem>();
                }
                //var cars = new List<Car>();
                //var divs = htmlDocument.DocumentNode.Descendants("ul").FirstOrDefault().Descendants("li").ToList();
                var divs = htmlDocument.DocumentNode.SelectNodes(@"//div[@class=' product']");

                foreach (var div in divs)
                {
                    var modelItem = div.Descendants("h6").FirstOrDefault().Descendants("a").FirstOrDefault();
                    string model = (modelItem == null) ? string.Empty : modelItem.InnerText;

                    var priceItem = div.Descendants("span").Where(o => o.GetAttributeValue("class", "").Equals("price")).FirstOrDefault();
                    string price = priceItem.InnerText;

                    string link = div.GetAttributeValue("href", "");
                    var imageItem = div.Descendants("div").Where(o => o.GetAttributeValue("class", "").Equals("product-image")).FirstOrDefault();
                    string image = imageItem.Descendants("img").FirstOrDefault().GetAttributeValue("src", "");
                    if (image == "")
                    {
                        image = imageItem.Descendants("img").FirstOrDefault().GetAttributeValue("data-src", "");
                    }

                    var car = new CrawlItem
                    {
                        Model = model,
                        Price = price,
                        Link = link,
                        ImageUrl = image
                    };

                    if (!string.IsNullOrEmpty(car.Model))
                    {
                        crawledList.Add(car);
                    }

                }
                Console.WriteLine("PicoCralerAsync done");
            }
            catch (Exception)
            {

            }
        }

        private static async Task NgKCralerAsync()
        {
            try
            {
                //the url of the page we want to test
                var url = "https://www.nguyenkim.com/dien-thoai-di-dong-apple-iphone/";
                var httpClient = new HttpClient();
                var htmlDocument = new HtmlDocument();
                var html = await httpClient.GetStringAsync(url);

                htmlDocument.LoadHtml(html);

                // a list to add all the list of cars and the various prices 
                if (crawledList == null)
                {
                    crawledList = new List<CrawlItem>();
                }
                //var cars = new List<Car>();
                //var divs = htmlDocument.DocumentNode.Descendants("ul").FirstOrDefault().Descendants("li").ToList();
                var divs = htmlDocument.DocumentNode.SelectNodes(@"//a[@class='nk-link-product']");

                foreach (var div in divs)
                {
                    var modelItem = div.Descendants("h6").FirstOrDefault().Descendants("a").FirstOrDefault();
                    string model = (modelItem == null) ? string.Empty : modelItem.InnerText;

                    var priceItem = div.Descendants("span").Where(o => o.GetAttributeValue("class", "").Equals("price")).FirstOrDefault();
                    string price = priceItem.InnerText;

                    string link = div.GetAttributeValue("href", "");
                    var imageItem = div.Descendants("div").Where(o => o.GetAttributeValue("class", "").Equals("product-image")).FirstOrDefault();
                    string image = imageItem.Descendants("img").FirstOrDefault().GetAttributeValue("src", "");
                    if (image == "")
                    {
                        image = imageItem.Descendants("img").FirstOrDefault().GetAttributeValue("data-src", "");
                    }

                    var car = new CrawlItem
                    {
                        Model = model,
                        Price = price,
                        Link = link,
                        ImageUrl = image
                    };

                    if (!string.IsNullOrEmpty(car.Model))
                    {
                        crawledList.Add(car);
                    }

                }
                Console.WriteLine("NgKCralerAsync done");
            }
            catch (Exception)
            {

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
