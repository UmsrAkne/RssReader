namespace RssReader.Models
{
    public class WebSite
    {
        public int Id { get; set; }

        /// <summary>
        ///     サイトの表示名
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     ウェブサイトの RSS 配信の URL
        /// </summary>
        public string Url { get; set; }
    }
}