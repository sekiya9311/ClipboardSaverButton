using System.Configuration;
using System.Windows;

namespace ClipboardSaverButton.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            if (Settings.Default.MainWindowLeft != 0)
            {
                this.Left = Settings.Default.MainWindowLeft;
                this.Top = Settings.Default.MainWindowTop;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Settings.Default.MainWindowLeft = this.Left;
            Settings.Default.MainWindowTop = this.Top;

            Settings.Default.Save();
        }
    }
}
