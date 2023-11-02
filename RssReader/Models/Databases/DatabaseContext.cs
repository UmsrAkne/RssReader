using System.Data.SQLite;
using System.IO;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace RssReader.Models.Databases
{
    public class DatabaseContext : DbContext
    {
        private string DatabaseFileName => "database.sqlite";

        public DbSet<Feed> Feeds { get; set; }

        public DbSet<WebSite> WebSites { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!File.Exists(DatabaseFileName))
            {
                SQLiteConnection.CreateFile(DatabaseFileName); // ファイルが存在している場合は上書き。
            }

            var connectionString = new SqliteConnectionStringBuilder { DataSource = DatabaseFileName, }.ToString();
            optionsBuilder.UseSqlite(new SQLiteConnection(connectionString));
        }
    }
}