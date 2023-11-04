using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace RssReader.Models.Databases
{
    public class DatabaseContext : DbContext, IDataSource
    {
        private string DatabaseFileName => "database.sqlite";

        // ReSharper disable once UnusedAutoPropertyAccessor.Global : Database 利用のため、get, set の両方が必要
        public DbSet<Feed> Feeds { get; set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Global : Database 利用のため、get, set の両方が必要
        public DbSet<WebSite> WebSites { get; set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Global : Database 利用のため、get, set の両方が必要
        public DbSet<WebSiteGroup> WebSiteGroups { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!File.Exists(DatabaseFileName))
            {
                SQLiteConnection.CreateFile(DatabaseFileName); // ファイルが存在している場合は上書き。
            }

            var connectionString = new SqliteConnectionStringBuilder { DataSource = DatabaseFileName, }.ToString();
            optionsBuilder.UseSqlite(new SQLiteConnection(connectionString));
        }

        public IEnumerable<Feed> GetFeeds()
        {
            return Feeds;
        }

        public IEnumerable<WebSite> GetWebSites()
        {
            return WebSites;
        }

        public IEnumerable<WebSiteGroup> GetWebSiteGroups()
        {
            return WebSiteGroups;
        }

        public void Add(Feed feed)
        {
            Feeds.Add(feed);
        }

        public void Add(WebSite webSite)
        {
            WebSites.Add(webSite);
        }

        public void Add(WebSiteGroup webSiteGroup)
        {
            WebSiteGroups.Add(webSiteGroup);
        }

        public void Save()
        {
            SaveChanges();
        }
    }
}