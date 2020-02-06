namespace ClipboardSaverButton.Models.Interfaces
{
    public interface ISettings
    {
        double MainWindowLeft { get; set; }
        double MainWindowTop { get; set; }
        void Save();
    }
}
