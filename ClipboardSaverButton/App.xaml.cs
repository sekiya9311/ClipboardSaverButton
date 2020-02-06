using Prism.Ioc;
using ClipboardSaverButton.Views;
using System.Windows;
using ClipboardSaverButton.Models.Interfaces;
using ClipboardSaverButton.Models.Impls;

namespace ClipboardSaverButton
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
            containerRegistry.RegisterSingleton<IClipboardDataSaver, ClipboardDataSaver>();
            containerRegistry.RegisterSingleton<IDataSaver, DataSaver>();
            containerRegistry.RegisterSingleton<IClipboardManager, ClipboardManager>();
            containerRegistry.RegisterSingleton<IDialogService, DialogServiceWithMessageBox>();
            containerRegistry.RegisterSingleton<IFilePathInquirer, FilePathInquirer>();
            containerRegistry.RegisterSingleton<ISettings, SettingsWrapper>();
        }
    }
}
