using ClipboardSaverButton.Models.Interfaces;
using System.Windows;

namespace ClipboardSaverButton.Models.Impls
{
    class DialogServiceWithMessageBox : IDialogService
    {
        public void ShowMessage(string message, string caption = "")
        {
            MessageBox.Show(message, caption);
        }
    }
}
