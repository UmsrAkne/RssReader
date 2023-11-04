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
        private string url;

        public event Action<IDialogResult> RequestClose;

        public string Title => string.Empty;

        public DelegateCommand CloseCommand => new (() =>
        {
            RequestClose?.Invoke(new DialogResult());
        });

        public DelegateCommand<WebSiteGroup> ConfirmCommand => new ((webSiteGroup) =>
        {
            if (string.IsNullOrEmpty(Url))
            {
                return;
            }

            if (webSiteGroup == null)
            {
                var wg = new WebSiteGroup { Name = "default Name Group", };
                DatabaseManager.Add(wg);
                webSiteGroup = wg;
            }

            var site = new WebSite
            {
                Title = DisplayName,
                Url = Url,
                GroupId = webSiteGroup.Id,
            };

            DatabaseManager.Add(site);
            DatabaseManager.SaveChanges();
            RequestClose?.Invoke(new DialogResult());
        });

        private DatabaseManager DatabaseManager { get; set; }

        public List<WebSiteGroup> WebSiteGroups { get; private set; }

        public string DisplayName { get; set; }

        public string Url { get => url; set => SetProperty(ref url, value); }

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