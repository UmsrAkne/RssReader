using System.ServiceModel.Syndication;
using System.Xml;

namespace RssReader.Models
{
    public static class FeedReader
    {
        public static void GetRss(string url)
        {
            using var rdr = XmlReader.Create(url);
            var feed = SyndicationFeed.Load(rdr);
        }
    }
}