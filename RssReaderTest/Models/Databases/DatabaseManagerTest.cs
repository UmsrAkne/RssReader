using System;
using System.Linq;
using NUnit.Framework;
using RssReader.Models;
using RssReader.Models.Databases;

namespace RssReaderTest.Models.Databases
{
    public class DatabaseManagerTest
    {
        public DatabaseManager DatabaseManager { get; set; }

        private DatabaseMock Mock { get; set; }

        [SetUp]
        public void SetUp()
        {
            Mock = new DatabaseMock();
            DatabaseManager = new DatabaseManager(Mock);

            Mock.Add(new WebSiteGroup { Id = 1, Name = "Group1", });
            Mock.Add(new WebSiteGroup { Id = 2, Name = "Group2", });

            Mock.Add(new WebSite { Id = 1, Title = "site title1", Url = "url 1", GroupId = 1, });
            Mock.Add(new WebSite { Id = 2, Title = "site title2", Url = "url 2", GroupId = 2, });
            Mock.Add(new WebSite { Id = 3, Title = "site title3", Url = "url 3", GroupId = 2, });

            Mock.Add(new Feed()
            {
                Id = 1, Title = "feed1", Url = "sample url1", Description = "sample description1",
                DateTime = new DateTime(1), ParentSiteId = 1,
            });

            Mock.Add(new Feed()
            {
                Id = 2, Title = "feed2", Url = "sample url2", Description = "sample description2",
                DateTime = new DateTime(2), ParentSiteId = 1,
            });

            Mock.Add(new Feed()
            {
                Id = 3, Title = "feed3", Url = "sample url3", Description = "sample description3",
                DateTime = new DateTime(3), ParentSiteId = 2,
            });

            Mock.Add(new Feed()
            {
                Id = 4, Title = "feed4", Url = "sample url4", Description = "sample description4",
                DateTime = new DateTime(4), ParentSiteId = 3,
            });
        }

        [Test]
        public void AddFeedTest()
        {
            var dbMock = new DatabaseMock();
            var db = new DatabaseManager(dbMock);

            Assert.That(dbMock.Feeds.Count, Is.EqualTo(0), "初期状態は要素数 0");

            var f1 = new Feed() { Url = "aaa", DateTime = new DateTime(1), Title = "test1", };
            db.AddFeed(f1);
            Assert.That(dbMock.Feeds.Count, Is.EqualTo(1), "要素が追加されて 1");

            var f2 = new Feed() { Url = "aaa", DateTime = new DateTime(1), Title = "test2", };
            db.AddFeed(f2);
            Assert.That(dbMock.Feeds.Count, Is.EqualTo(1), "Url と DateTime が重複している場合は要素は追加されない");

            var f3 = new Feed() { Url = "bbb", DateTime = new DateTime(1), Title = "test", };
            db.AddFeed(f3);
            Assert.That(dbMock.Feeds.Count, Is.EqualTo(2), "Url と DateTime のどちらかが違えば追加できる");

            var f4 = new Feed() { Url = "aaa", DateTime = new DateTime(2), Title = "test", };
            db.AddFeed(f4);
            Assert.That(dbMock.Feeds.Count, Is.EqualTo(3), "Url と DateTime のどちらかが違えば追加できる");
        }

        [Test]
        public void GetWebSiteWrapperTest()
        {
            var wrappers = DatabaseManager.GetWebSiteWrappers().ToList();

            Assert.That(wrappers.Count, Is.EqualTo(2));
            Assert.That(wrappers[0].Children.Count, Is.EqualTo(1), "子要素の数は 1 のはず");
            Assert.That(wrappers[1].Children.Count, Is.EqualTo(2), "子要素の数は 2 のはず");
        }
    }
}