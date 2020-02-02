using ClipboardSaverButton.Models.Interfaces;
using Prism.Commands;
using Prism.Mvvm;

namespace ClipboardSaverButton.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public string Title => typeof(MainWindowViewModel).Assembly.FullName ?? "";

        private DelegateCommand? _saveCommand;
        public DelegateCommand SaveCommand
            => _saveCommand ??= new DelegateCommand(Save);

        private readonly IClipboardDataSaver _clipboardDataSaver;

        public MainWindowViewModel(IClipboardDataSaver clipboardDataSaver)
        {
            _clipboardDataSaver = clipboardDataSaver;
        }

        private void Save()
        {
            _clipboardDataSaver.Save();
        }
    }
}
