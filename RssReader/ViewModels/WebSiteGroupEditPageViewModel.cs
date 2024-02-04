using System;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using RssReader.Models;
using RssReader.Models.Databases;

namespace RssReader.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class WebSiteGroupEditPageViewModel : BindableBase, IDialogAware
    {
        private string groupName;

        public event Action<IDialogResult> RequestClose;

        public string Title => "WebSiteGroup Edit Page";

        public string GroupName { get => groupName; set => SetProperty(ref groupName, value); }

        public DelegateCommand CloseCommand => new (() =>
        {
            WebSiteGroup.Name = GroupName;
            DatabaseManager.SaveChanges();
            RequestClose?.Invoke(new DialogResult());
        });

        private DatabaseManager DatabaseManager { get; set; }

        private WebSiteGroup WebSiteGroup { get; set; }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            DatabaseManager = parameters.GetValue<DatabaseManager>(nameof(DatabaseManager));
            WebSiteGroup = parameters.GetValue<WebSiteGroup>(nameof(WebSiteGroup));
            GroupName = WebSiteGroup.Name;
        }
    }
}