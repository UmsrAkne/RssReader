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

        public void AddRange(IEnumerable<Feed> feeds)
        {
            var f = feeds.ToList();
            if (!f.Any())
            {
                return;
            }

            var dbList = GetFeeds(f.First().ParentSiteId).ToList();
            DataSource.AddRange(f.Except(dbList, new FeedComparer()));
        }

        public void Add(WebSiteGroup webSiteGroup)
        {
            DataSource.Add(webSiteGroup);
        }

        public void Add(WebSite webSite)
        {
            DataSource.Add(webSite);
        }

        public void Add(NgWord ngWord)
        {
            DataSource.Add(ngWord);
        }

        public IEnumerable<Feed> GetFeeds(int webSiteId)
        {
            var ngWords = DataSource.GetNgWords().ToList();
            return DataSource.GetFeeds()
                .Where(f => f.ParentSiteId == webSiteId)
                .Where(f => !ngWords.Any(w => w.IsMatch(f.Title) || w.IsMatch(f.Description)));
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

        public IEnumerable<NgWord> GetNgWords()
        {
            return DataSource.GetNgWords();
        }

        public void Remove(NgWord ngWord)
        {
            DataSource.Remove(ngWord);
        }

        public void SaveChanges()
        {
            DataSource.Save();
        }
    }
}