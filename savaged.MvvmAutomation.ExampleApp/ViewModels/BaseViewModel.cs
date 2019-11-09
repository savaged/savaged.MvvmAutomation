using GalaSoft.MvvmLight;

namespace savaged.MvvmAutomation.ExampleApp.ViewModels
{
    class BaseViewModel : ViewModelBase
    {
        // TODO wire up to busy registry
        public bool CanExecute => true;
    }
}
