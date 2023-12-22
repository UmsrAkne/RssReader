using System;
using System.Collections.ObjectModel;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using RssReader.Models;
using RssReader.Models.Databases;

namespace RssReader.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class NgWordRegistrationPageViewModel : BindableBase, IDialogAware
    {
        private ObservableCollection<NgWord> ngWords;
        private string inputText;

        public event Action<IDialogResult> RequestClose;

        public string Title => string.Empty;

        public DelegateCommand CloseCommand => new (() =>
        {
            RequestClose?.Invoke(new DialogResult());
        });

        public DatabaseManager DatabaseManager { get; set; }

        public ObservableCollection<NgWord> NgWords { get => ngWords; set => SetProperty(ref ngWords, value); }

        public string InputText { get => inputText; set => SetProperty(ref inputText, value); }

        public DelegateCommand<string> AddNgWordCommand => new ((word) =>
        {
            var n = new NgWord() { Word = word, };
            DatabaseManager.Add(n);
            NgWords.Add(n);
            DatabaseManager.SaveChanges();
            InputText = string.Empty;
        });

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
            NgWords = new ObservableCollection<NgWord>(DatabaseManager.GetNgWords());
        }
    }
}