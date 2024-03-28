using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using RssReader.Models;
using RssReader.Models.Databases;
using RssReader.Views;

namespace RssReader.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class MainWindowViewModel : BindableBase
    {
        private readonly IDialogService dialogService;
        private readonly FeedListViewModel feedListViewModel;
        private WebSiteTreeViewModel webSiteTreeViewModel;

        public MainWindowViewModel(IDialogService dialogService)
        {
            this.dialogService = dialogService;

            IDataSource dataSource = new DatabaseContext();
            ((DatabaseContext)dataSource).Database.EnsureCreated();

            DatabaseManager = new DatabaseManager(dataSource);
            WebSiteTreeViewModel = new WebSiteTreeViewModel(DatabaseManager.GetWebSiteWrappers());
            FeedListViewModel = new FeedListViewModel(new List<Feed>()) { DatabaseManager = DatabaseManager, };
        }

        public string Title => "Prism Application";

        public FeedListViewModel FeedListViewModel
        {
            get => feedListViewModel;
            private init => SetProperty(ref feedListViewModel, value);
        }

        public WebSiteTreeViewModel WebSiteTreeViewModel
        {
            get => webSiteTreeViewModel;
            private set
            {
                if (!SetProperty(ref webSiteTreeViewModel, value))
                {
                    return;
                }

                RaisePropertyChanged(nameof(ReloadFeedsCommand));
                RaisePropertyChanged(nameof(LoadRssCommand));
            }
        }

        /// <summary>
        ///     TreeView.SelectedItem が変更されたときに実行するコマンド。
        ///     FeedListViewModel の更新処理を行う。
        /// </summary>
        public DelegateCommand<WebSiteWrapper> ReloadFeedsCommand => new ((webSiteWrapper) =>
        {
            if (webSiteWrapper.IsWebSite)
            {
                WebSiteTreeViewModel.SelectedId = webSiteWrapper.WebSite.Id;
                FeedListViewModel.Feeds =
                    new ObservableCollection<Feed>(DatabaseManager.GetFeeds(webSiteWrapper.WebSite.Id)
                        .OrderBy(f => f.IsRead)
                        .ThenByDescending(f => f.DateTime)
                    );

                webSiteWrapper.UnreadCount = FeedListViewModel.Feeds.Count(f => !f.IsRead);
            }
        });

        public DelegateCommand<IEnumerable<WebSiteWrapper>> LoadRssCommand => new ((wrappers) =>
        {
            var webSiteWrappers = wrappers.ToList();
            if (!webSiteWrappers.Any())
            {
                return;
            }

            foreach (var ww in webSiteWrappers)
            {
                foreach (var site in ww.Children)
                {
                    foreach (var feed in FeedReader.GetRss(site.WebSite))
                    {
                        DatabaseManager.AddFeed(feed);
                    }
                }
            }

            DatabaseManager.SaveChanges();
            FeedListViewModel.Feeds =
                new ObservableCollection<Feed>(DatabaseManager.GetFeeds(WebSiteTreeViewModel.SelectedId));
        });

        public DelegateCommand ShowWebSiteRegistrationPageCommand => new (() =>
        {
            dialogService.ShowDialog(
                nameof(WebSiteRegistrationPage),
                new DialogParameters() { { nameof(DatabaseManager), DatabaseManager }, },
                result =>
                {
                    if (result.Result == ButtonResult.Yes)
                    {
                        WebSiteTreeViewModel = new WebSiteTreeViewModel(DatabaseManager.GetWebSiteWrappers());
                    }
                });
        });

        public DelegateCommand ShowNgWordRegistrationPageCommand => new (() =>
        {
            dialogService.ShowDialog(
                nameof(NgWordRegistrationPage),
                new DialogParameters() { { nameof(DatabaseManager), DatabaseManager }, },
                _ =>
                {
                });
        });

        public DelegateCommand<WebSiteWrapper> ShowWebSiteGroupEditPageCommand => new ((ww) =>
        {
            // TreeView のコンテキストメニューから呼び出されるコマンド
            // ItemsSource の型は WebSiteWrapper となっており、グループとウェブサイトが混在している
            // そのため、WebSiteGroup に値がセットされているかを確認して処理。

            if (ww?.WebSiteGroup == null)
            {
                return;
            }

            var wg = ww.WebSiteGroup;

            dialogService.ShowDialog(
                nameof(WebSiteGroupEditPage),
                new DialogParameters()
                {
                    { nameof(DatabaseManager), DatabaseManager },
                    { nameof(WebSiteGroup), wg },
                },
                _ =>
                {
                    WebSiteTreeViewModel = new WebSiteTreeViewModel(DatabaseManager.GetWebSiteWrappers());
                });
        });

        private DatabaseManager DatabaseManager { get; init; }
    }
}