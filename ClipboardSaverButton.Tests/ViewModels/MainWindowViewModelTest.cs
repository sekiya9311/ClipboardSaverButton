using ClipboardSaverButton.Models.Interfaces;
using ClipboardSaverButton.ViewModels;
using Moq;
using Xunit;

namespace ClipboardSaverButton.Tests.ViewModels
{
    public class MainWindowViewModelTest
    {
        private readonly Mock<IClipboardDataSaver> _clipboardDataSaver;
        private readonly Mock<ISettings> _settings;

        public MainWindowViewModelTest()
        {
            _clipboardDataSaver = new Mock<IClipboardDataSaver>();
            _settings = new Mock<ISettings>();
        }

        [Fact]
        public void Initialize()
        {
            _settings
                .SetupGet(x => x.MainWindowLeft)
                .Returns(1);
            _settings
                .SetupGet(x => x.MainWindowTop)
                .Returns(2);

            var viewModel = new MainWindowViewModel(
                _clipboardDataSaver.Object,
                _settings.Object
            );

            Assert.Equal(
                typeof(MainWindowViewModel).Assembly.FullName,
                viewModel.Title);
            Assert.Equal(1, viewModel.Left);
            Assert.Equal(2, viewModel.Top);
            Assert.NotNull(viewModel.SaveCommand);
            Assert.True(viewModel.SaveCommand.CanExecute());
            Assert.NotNull(viewModel.ClosingCommand);
            Assert.True(viewModel.ClosingCommand.CanExecute());
        }

        [Fact]
        public void SetProperties()
        {
            var viewModel = new MainWindowViewModel(
                _clipboardDataSaver.Object,
                _settings.Object
            );

            viewModel.Left = 3;
            viewModel.Top = 4;

            _settings.VerifySet(x => x.MainWindowLeft = 3);
            _settings.VerifySet(x => x.MainWindowTop = 4);
        }

        [Fact]
        public void Save()
        {
            var viewModel = new MainWindowViewModel(
                _clipboardDataSaver.Object,
                _settings.Object
            );
            
            viewModel.SaveCommand.Execute();

            _clipboardDataSaver.Verify(x => x.Save());
        }

        [Fact]
        public void Closing()
        {
            var viewModel = new MainWindowViewModel(
                _clipboardDataSaver.Object,
                _settings.Object
            );

            viewModel.ClosingCommand.Execute();

            _settings.Verify(x => x.Save());
        }
    }
}
