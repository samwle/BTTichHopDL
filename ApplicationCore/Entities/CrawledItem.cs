using Ardalis.GuardClauses;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using System;
using System.Runtime.CompilerServices;

namespace Microsoft.eShopWeb.ApplicationCore.Entities
{
    public class CrawledItem
    {
        public string ID { get; private set; }
        public string Model { get; private set; }
        public string Price { get; private set; }
        public string Link { get; private set; }
        public string ImageUrl { get; private set; }   
        public DateTime CrawledTime { get; private set; }

        public CrawledItem(string model, string price, string link, string imageUrl)
        {
            ID = System.Guid.NewGuid().ToString();
            Model = model;
            Price = price;
            Link = link;
            ImageUrl = imageUrl;
            CrawledTime = DateTime.Now;
        }
    }
}
