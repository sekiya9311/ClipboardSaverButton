using ClipboardSaverButton.Models.Interfaces;
using ClipboardSaverButton.Properties;

namespace ClipboardSaverButton.Models.Impls
{
    public class SettingsWrapper : ISettings
    {
        public double MainWindowLeft { get; set; }
        public double MainWindowTop { get; set; }

        public SettingsWrapper()
        {
            MainWindowLeft = Settings.Default.MainWindowLeft;
            MainWindowTop = Settings.Default.MainWindowTop;
        }

        public void Save()
        {
            Settings.Default.MainWindowLeft = MainWindowLeft;
            Settings.Default.MainWindowTop = MainWindowTop;

            Settings.Default.Save();
        }
    }
}
