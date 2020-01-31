using Prism.Commands;
using Prism.Mvvm;
using System.Windows.Input;

namespace ClipboardSaverButton.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public string Title => typeof(MainWindowViewModel).Assembly.FullName ?? "";

        private ICommand? _saveCommand;
        public ICommand SaveCommand
            => _saveCommand ??= new DelegateCommand(Save);

        public MainWindowViewModel()
        {

        }

        private void Save()
        {

        }
    }
}
