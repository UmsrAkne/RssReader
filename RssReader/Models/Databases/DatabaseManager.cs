namespace RssReader.Models.Databases
{
    public class DatabaseManager
    {
        public DatabaseManager(IDataSource dataSource)
        {
            DataSource = dataSource;
        }

        public IDataSource DataSource { get; private set; }
    }
}