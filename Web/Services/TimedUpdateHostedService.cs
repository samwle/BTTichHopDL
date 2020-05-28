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
            _logger.LogInformation("Timed Background Service is starting. Loading every hours");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
               TimeSpan.FromSeconds(15)
               /*TimeSpan.FromHours(1)*/
                );

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            _logger.LogInformation("Timed Background Service is working.");

            using (IServiceScope scope = _provider.CreateScope())
            {
                var _catalogContext = scope.ServiceProvider.GetRequiredService<CatalogContext>();

                var itemList = _catalogContext.CrawledItems
                                //.GroupBy(o => new { o.Link, o.ImageUrl })
                                .ToList();

                var mergedList = itemList.GroupBy(o => new { o.Link, o.ImageUrl })
                                .ToList();

            }

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
