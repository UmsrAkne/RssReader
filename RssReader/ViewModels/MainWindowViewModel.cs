﻿using Prism.Mvvm;

namespace RssReader.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class MainWindowViewModel : BindableBase
    {
        public string Title => "Prism Application";

        public FeedListViewModel FeedListViewModel { get; private set; }

        public WebSiteTreeViewModel WebSiteTreeViewModel { get; private set; }
    }
}