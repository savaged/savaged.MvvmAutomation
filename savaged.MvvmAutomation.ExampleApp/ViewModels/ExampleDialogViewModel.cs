using GalaSoft.MvvmLight.CommandWpf;
using System.Windows.Input;

namespace savaged.MvvmAutomation.ExampleApp.ViewModels
{
    class ExampleDialogViewModel : DialogViewModel
    {
        public ExampleDialogViewModel()
        {
            OkCmd = new RelayCommand(OnOk, CanExecute);
        }

        public ICommand OkCmd { get; }

        private void OnOk()
        {
            DialogResult = true;
        }

    }
}
