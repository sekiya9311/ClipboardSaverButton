using ClipboardSaverButton.Models.Interfaces;
using Prism.Commands;
using Prism.Mvvm;

namespace ClipboardSaverButton.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public string Title => typeof(MainWindowViewModel).Assembly.FullName ?? "";
        public double Left
        {
            get => _settings.MainWindowLeft;
            set
            {
                _settings.MainWindowLeft = value;
                RaisePropertyChanged();
            }
        }
        public double Top
        {
            get => _settings.MainWindowTop;
            set
            {
                _settings.MainWindowTop = value;
                RaisePropertyChanged();
            }
        }

        private DelegateCommand? _saveCommand;
        public DelegateCommand SaveCommand
            => _saveCommand ??= new DelegateCommand(Save);

        private DelegateCommand? _closingCommand;
        public DelegateCommand ClosingCommand
            => _closingCommand ??= new DelegateCommand(Closing);

        private readonly IClipboardDataSaver _clipboardDataSaver;
        private readonly ISettings _settings;

        public MainWindowViewModel(IClipboardDataSaver clipboardDataSaver, ISettings settings)
        {
            _clipboardDataSaver = clipboardDataSaver;
            _settings = settings;
        }

        private void Save()
        {
            _clipboardDataSaver.Save();
        }

        private void Closing()
        {
            _settings.Save();
        }
    }
}
