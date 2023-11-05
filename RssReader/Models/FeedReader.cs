using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Xml;

namespace RssReader.Models
{
    public static class FeedReader
    {
        public static IEnumerable<Feed> GetRss(string url)
        {
            using var rdr = XmlReader.Create(url);
            var feed = SyndicationFeed.Load(rdr);

            return feed.Items.Select(f => new Feed
            {
                // ParentId = 0,
                DateTime = f.PublishDate.DateTime,
                Description = f.Summary != null ? f.Summary.Text : string.Empty,
                Title = f.Title.Text,
                Url = f.Links.Count > 0 ? f.Links[0].Uri.AbsolutePath : "",
            });
        }
    }
}