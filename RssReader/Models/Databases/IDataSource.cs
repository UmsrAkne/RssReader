using System.Collections.Generic;

namespace RssReader.Models.Databases
{
    public interface IDataSource
    {
        public IEnumerable<Feed> GetFeeds();

        public IEnumerable<WebSite> GetWebSites();

        public IEnumerable<WebSiteGroup> GetWebSiteGroups();

        public IEnumerable<NgWord> GetNgWords();

        public void Add(Feed feed);

        public void Add(WebSite webSite);

        public void Add(WebSiteGroup webSiteGroup);

        public void Add(NgWord ngWord);

        public void Save();
    }
}