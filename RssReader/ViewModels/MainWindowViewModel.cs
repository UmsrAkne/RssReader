using Prism.Commands;
using Prism.Mvvm;
using RssReader.Models;
using RssReader.Models.Databases;

namespace RssReader.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class MainWindowViewModel : BindableBase
    {
        public MainWindowViewModel()
        {
            IDataSource dataSource = new DatabaseContext();
            DatabaseManager = new DatabaseManager(dataSource);
        }

        public string Title => "Prism Application";

        public FeedListViewModel FeedListViewModel { get; private set; }

        public WebSiteTreeViewModel WebSiteTreeViewModel { get; private set; }

        /// <summary>
        ///     TreeView.SelectedItem が変更されたときに実行するコマンド。
        ///     FeedListViewModel の更新処理を行う。
        /// </summary>
        public DelegateCommand<WebSiteWrapper> ReloadFeedsCommand => new ((webSiteWrapper) =>
        {
            if (webSiteWrapper.IsWebSite)
            {
            }
        });

        private DatabaseManager DatabaseManager { get; set; }
    }
}