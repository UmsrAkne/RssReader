using System.Collections.Generic;
using System.Collections.ObjectModel;
using Prism.Mvvm;
using RssReader.Models;
using RssReader.Models.Databases;

namespace RssReader.ViewModels
{
    public class FeedListViewModel : BindableBase
    {
        private Feed selectedItem;
        private ObservableCollection<Feed> feeds;

        public FeedListViewModel(IEnumerable<Feed> feeds)
        {
            Feeds = new ObservableCollection<Feed>(feeds);
        }

        public ObservableCollection<Feed> Feeds { get => feeds; set => SetProperty(ref feeds, value); }

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

        public DatabaseManager DatabaseManager { get; set; }
    }
}