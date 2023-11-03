using System.Collections.Generic;

namespace RssReader.Models
{
    public class WebSiteWrapper
    {
        public WebSiteWrapper(WebSiteGroup webSiteGroup)
        {
            WebSiteGroup = webSiteGroup;
        }

        public WebSiteWrapper(WebSite website)
        {
            WebSite = website;
        }

        public WebSite WebSite { get; }

        public WebSiteGroup WebSiteGroup { get; }

        public string Name => WebSiteGroup != null ? WebSiteGroup.Name : WebSite.Title;

        public List<WebSiteWrapper> Children { get; set; } = new ();

        public bool IsWebSite => WebSite != null;
    }
}