using System.Collections.Generic;
using System.Collections.ObjectModel;
using Prism.Mvvm;
using RssReader.Models;

namespace RssReader.ViewModels
{
    public class WebSiteTreeViewModel : BindableBase
    {
        public WebSiteTreeViewModel(IEnumerable<WebSiteWrapper> webSiteWrappers)
        {
            WebSiteWrappers = new ObservableCollection<WebSiteWrapper>(webSiteWrappers);
        }

        public ObservableCollection<WebSiteWrapper> WebSiteWrappers { get; private set; }

        public int SelectedId { get; set; }
    }
}