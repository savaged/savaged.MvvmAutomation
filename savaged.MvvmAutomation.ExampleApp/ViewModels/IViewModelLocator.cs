using GalaSoft.MvvmLight;

namespace savaged.MvvmAutomation.ExampleApp.ViewModels
{
    public interface IViewModelLocator
    {
        ViewModelBase GetViewModel(string windowName);
    }
}