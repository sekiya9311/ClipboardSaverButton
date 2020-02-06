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

            if (Properties.Settings.Default.MainWindowLeft != 0)
            {
                this.Left = Properties.Settings.Default.MainWindowLeft;
                this.Top = Properties.Settings.Default.MainWindowTop;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Properties.Settings.Default.MainWindowLeft = this.Left;
            Properties.Settings.Default.MainWindowTop = this.Top;

            Properties.Settings.Default.Save();
        }
    }
}
