using System.Collections.Generic;
using System.Collections.ObjectModel;
using Prism.Mvvm;
using RssReader.Models;

namespace RssReader.ViewModels
{
    public class FeedListViewModel : BindableBase
    {
        public FeedListViewModel(IEnumerable<Feed> feeds)
        {
            Feeds = new ObservableCollection<Feed>(feeds);
        }

        public ObservableCollection<Feed> Feeds { get; set; }
    }
}