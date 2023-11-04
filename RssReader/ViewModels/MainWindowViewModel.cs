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

        public MainWindowViewModel(IDialogService dialogService)
        {
            this.dialogService = dialogService;

            IDataSource dataSource = new DatabaseContext();
            ((DatabaseContext)dataSource).Database.EnsureCreated();

            DatabaseManager = new DatabaseManager(dataSource);
            WebSiteTreeViewModel = new WebSiteTreeViewModel(DatabaseManager.GetWebSiteWrappers());
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

        public DelegateCommand ShowWebSiteRegistrationPageCommand => new (() =>
        {
            dialogService.ShowDialog(
                nameof(WebSiteRegistrationPage),
                new DialogParameters() { { nameof(DatabaseManager), DatabaseManager }, },
                (IDialogResult result) =>
                {
                });
        });

        private DatabaseManager DatabaseManager { get; set; }
    }
}