using System;
using NUnit.Framework;
using RssReader.Models;
using RssReader.Models.Databases;

namespace RssReaderTest.Models.Databases
{
    public class DatabaseManagerTest
    {
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
    }
}