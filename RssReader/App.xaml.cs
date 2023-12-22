using System.Windows;
using Prism.Ioc;
using RssReader.ViewModels;
using RssReader.Views;

namespace RssReader
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterDialog<WebSiteRegistrationPage, WebSiteRegistrationPageViewModel>();
            containerRegistry.RegisterDialog<NgWordRegistrationPage, NgWordRegistrationPageViewModel>();
        }
    }
}