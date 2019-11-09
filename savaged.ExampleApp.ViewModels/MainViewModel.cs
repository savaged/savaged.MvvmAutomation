using GalaSoft.MvvmLight.CommandWpf;
using savaged.ExampleApp.Services;
using System;
using System.Windows.Input;

namespace savaged.ExampleApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IWindowService _viewService;
        private bool _isBusy;

        private string _feedback;

        public MainViewModel(
            IWindowService windowService)
        {
            _viewService = windowService ??
                throw new ArgumentNullException(nameof(windowService));

            ShowExampleWindowCmd = new RelayCommand(
                OnShowExampleWindow, () => CanShowExampleWindow);

            ShowExampleDialogCmd = new RelayCommand(
                OnShowExampleDialog, () => CanShowExampleDialog);
        }

        // TODO wire up to busy registry
        public bool IsBusy
        {
            get => _isBusy;
            set => Set(ref _isBusy, value);
        }

        public bool CanShowExampleWindow => !IsBusy;

        public bool CanShowExampleDialog => !IsBusy;

        public ICommand ShowExampleWindowCmd { get; }

        public ICommand ShowExampleDialogCmd { get; }

        public string Feedback
        {
            get => _feedback;
            set => Set(ref _feedback, value);
        }

        private void OnShowExampleWindow()
        {
            _viewService.Show("ExampleWindow");
        }

        private void OnShowExampleDialog()
        {
            var result = _viewService.ShowDialog("ExampleDialog");
            
            if (result == true)
            {
                Feedback = "True";
            }
            else if (result == false)
            {
                Feedback = "False";
            }
            else
            {
                Feedback = "";
            }
        }

    }
}
