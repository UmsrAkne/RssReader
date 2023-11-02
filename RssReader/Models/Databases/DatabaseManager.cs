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
            DataSource.Add(feed);
        }
    }
}