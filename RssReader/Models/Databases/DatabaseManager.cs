using System.Collections.Generic;
using System.Linq;

namespace RssReader.Models.Databases
{
    public class DatabaseManager
    {
        public DatabaseManager(IDataSource dataSource)
        {
            DataSource = dataSource;
        }

        private IDataSource DataSource { get; }

        public void AddFeed(Feed feed)
        {
            if (DataSource.GetFeeds().Any(f => f.Url == feed.Url && f.DateTime == feed.DateTime))
            {
                return;
            }

            DataSource.Add(feed);
        }

        public void Add(WebSiteGroup webSiteGroup)
        {
            DataSource.Add(webSiteGroup);
        }

        public void Add(WebSite webSite)
        {
            DataSource.Add(webSite);
        }

        public IEnumerable<Feed> GetFeeds(int webSiteId)
        {
            return DataSource.GetFeeds().Where(f => f.ParentSiteId == webSiteId);
        }

        public IEnumerable<WebSiteWrapper> GetWebSiteWrappers()
        {
            return DataSource.GetWebSites()
                .GroupBy(g => g.GroupId)
                .Select(group =>
                {
                    var wg = DataSource.GetWebSiteGroups().FirstOrDefault(w => w.Id == group.Key);
                    return new WebSiteWrapper(wg)
                    {
                        Children = group.Select(w => new WebSiteWrapper(w)).ToList(),
                    };
                });
        }

        public IEnumerable<WebSiteGroup> GetWebSiteGroups()
        {
            return DataSource.GetWebSiteGroups();
        }

        public void SaveChanges()
        {
            DataSource.Save();
        }
    }
}