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

            Assert.That(dbMock.Feeds.Count, Is.EqualTo(0));

            db.AddFeed(new Feed());
            Assert.That(dbMock.Feeds.Count, Is.EqualTo(1));

            db.AddFeed(new Feed());
            Assert.That(dbMock.Feeds.Count, Is.EqualTo(2));
        }
    }
}