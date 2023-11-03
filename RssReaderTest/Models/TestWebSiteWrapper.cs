using NUnit.Framework;
using RssReader.Models;

namespace RssReaderTest.Models
{
    [TestFixture]
    public class TestWebSiteWrapper
    {
        [Test]
        public void NameTest()
        {
            var wrapper1 = new WebSiteWrapper(new WebSite { Title = "testTitle", });
            Assert.That(wrapper1.Name, Is.EqualTo("testTitle"));

            var wrapper2 = new WebSiteWrapper(new WebSiteGroup { Name = "testName", });
            Assert.That(wrapper2.Name, Is.EqualTo("testName"));
        }

        [Test]
        public void IsWebSiteTest()
        {
            var wrapper1 = new WebSiteWrapper(new WebSite());
            Assert.IsTrue(wrapper1.IsWebSite);

            var wrapper2 = new WebSiteWrapper(new WebSiteGroup());
            Assert.IsFalse(wrapper2.IsWebSite);
        }
    }
}