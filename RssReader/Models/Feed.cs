using System;

namespace RssReader.Models
{
    public class Feed
    {
        public int Id { get; set; }

        public int ParentSiteId { get; set; }

        public DateTime DateTime { get; set; }

        public string Description { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }

        public bool IsRead { get; set; }
    }
}