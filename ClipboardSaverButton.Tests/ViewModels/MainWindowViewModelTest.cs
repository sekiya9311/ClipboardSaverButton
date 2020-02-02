using ClipboardSaverButton.Models.Interfaces;
using ClipboardSaverButton.ViewModels;
using Moq;
using Xunit;

namespace ClipboardSaverButton.Tests.ViewModels
{
    public class MainWindowViewModelTest
    {
        private readonly Mock<IClipboardDataSaver> _clipboardDataSaver;

        public MainWindowViewModelTest()
        {
            _clipboardDataSaver = new Mock<IClipboardDataSaver>();
        }

        [Fact]
        public void Initialize()
        {
            var viewModel = new MainWindowViewModel(_clipboardDataSaver.Object);

            Assert.Equal(
                typeof(MainWindowViewModel).Assembly.FullName,
                viewModel.Title);
            Assert.NotNull(viewModel.SaveCommand);
            Assert.True(viewModel.SaveCommand.CanExecute());
        }

        [Fact]
        public void Save()
        {
            var viewModel = new MainWindowViewModel(_clipboardDataSaver.Object);
            
            viewModel.SaveCommand.Execute();

            _clipboardDataSaver.Verify(x => x.Save());
        }
    }
}
