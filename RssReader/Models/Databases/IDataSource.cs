using System.Collections.Generic;

namespace RssReader.Models.Databases
{
    public interface IDataSource
    {
        public IEnumerable<Feed> GetFeeds();

        public IEnumerable<WebSite> GetWebSites();
    }
}