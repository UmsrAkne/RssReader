using System.Collections.Generic;

namespace RssReader.Models
{
    public class FeedComparer : IEqualityComparer<Feed>
    {
        public bool Equals(Feed feedA, Feed feedB)
        {
            if (feedA == null || feedB == null)
            {
                return false;
            }

            return feedA.Url == feedB.Url && feedA.DateTime == feedB.DateTime;
        }

        public int GetHashCode(Feed obj)
        {
            return obj.GetHashCode();
        }
    }
}