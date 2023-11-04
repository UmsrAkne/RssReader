using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using RssReader.Models;
using RssReader.Models.Databases;

namespace RssReader.ViewModels
{
    public class WebSiteRegistrationPageViewModel : BindableBase, IDialogAware
    {
        private object url;

        public WebSiteRegistrationPageViewModel(string displayName)
        {
            DisplayName = displayName;
        }

        public event Action<IDialogResult> RequestClose;

        public string Title => string.Empty;

        public DelegateCommand CloseCommand => new (() =>
        {
            RequestClose?.Invoke(new DialogResult());
        });

        private DatabaseManager DatabaseManager { get; set; }

        public List<WebSiteGroup> WebSiteGroups { get; set; }

        public string DisplayName { get; set; }

        public object Url { get => url; set => SetProperty(ref url, value); }

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
            WebSiteGroups = DatabaseManager.GetWebSiteGroups().ToList();
        }
    }
}