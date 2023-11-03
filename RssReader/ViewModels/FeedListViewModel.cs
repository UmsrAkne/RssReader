using System.Collections.Generic;
using System.Collections.ObjectModel;
using Prism.Mvvm;
using RssReader.Models;

namespace RssReader.ViewModels
{
    public class FeedListViewModel : BindableBase
    {
        private Feed selectedItem;

        public FeedListViewModel(IEnumerable<Feed> feeds)
        {
            Feeds = new ObservableCollection<Feed>(feeds);
        }

        public ObservableCollection<Feed> Feeds { get; set; }

        public Feed SelectedItem
        {
            get => selectedItem;
            set
            {
                if (SetProperty(ref selectedItem, value))
                {
                    value.IsRead = true;
                }
            }
        }
    }
}