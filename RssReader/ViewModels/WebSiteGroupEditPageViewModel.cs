using System;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using RssReader.Models.Databases;

namespace RssReader.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class WebSiteGroupEditPageViewModel : BindableBase, IDialogAware
    {
        private string groupName;

        public event Action<IDialogResult> RequestClose;

        public string Title => string.Empty;

        public string GroupName { get => groupName; set => SetProperty(ref groupName, value); }

        public DelegateCommand CloseCommand => new (() =>
        {
            RequestClose?.Invoke(new DialogResult());
        });

        private DatabaseManager DatabaseManager { get; set; }

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
        }
    }
}