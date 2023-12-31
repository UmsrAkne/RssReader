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
    // ReSharper disable once ClassNeverInstantiated.Global
    public class WebSiteRegistrationPageViewModel : BindableBase, IDialogAware
    {
        private string url;
        private const string DefaultGroupName = "default Name Group";

        public event Action<IDialogResult> RequestClose;

        public string Title => string.Empty;

        public bool WebSiteAdded { get; set; }

        public DelegateCommand CloseCommand => new (() =>
        {
            RequestClose?.Invoke(new DialogResult(ButtonResult.Cancel));
        });

        public DelegateCommand<WebSiteGroup> ConfirmCommand => new ((webSiteGroup) =>
        {
            if (string.IsNullOrEmpty(Url))
            {
                return;
            }

            if (webSiteGroup == null)
            {
                var wg = DatabaseManager.GetWebSiteGroups().FirstOrDefault(g => g.Name == DefaultGroupName);
                if (wg == null)
                {
                    var defaultNameGroup = new WebSiteGroup() { Name = DefaultGroupName, };
                    DatabaseManager.Add(defaultNameGroup);
                    DatabaseManager.SaveChanges(); // 変更を確定して WebSiteGroup の Id を確定させる。
                    webSiteGroup = defaultNameGroup;
                }
                else
                {
                    webSiteGroup = wg;
                }
            }

            var site = new WebSite
            {
                Title = DisplayName,
                Url = Url,
                GroupId = webSiteGroup.Id,
            };

            DatabaseManager.Add(site);
            DatabaseManager.SaveChanges();
            RequestClose?.Invoke(new DialogResult(ButtonResult.Yes));
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