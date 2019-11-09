using GalaSoft.MvvmLight;

namespace savaged.ExampleApp.ViewModels
{
    public class BaseViewModel : ViewModelBase
    {
        // TODO wire up to busy registry
        public bool CanExecute => true;
    }
}
