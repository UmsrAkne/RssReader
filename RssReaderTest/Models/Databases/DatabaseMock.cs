using System.Collections.Generic;
using RssReader.Models;
using RssReader.Models.Databases;

namespace RssReaderTest.Models.Databases
{
    public class DatabaseMock : IDataSource
    {
        public List<Feed> Feeds { get; } = new ();

        public List<WebSite> WebSites { get; } = new ();

        public IEnumerable<Feed> GetFeeds()
        {
            return Feeds;
        }

        public IEnumerable<WebSite> GetWebSites()
        {
            return WebSites;
        }

        public void Add(Feed feed)
        {
            Feeds.Add(feed);
        }

        public void Add(WebSite webSite)
        {
            WebSites.Add(webSite);
        }
    }
}