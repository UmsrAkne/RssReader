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
    }
}