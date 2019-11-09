using GalaSoft.MvvmLight;

namespace savaged.MvvmAutomation.ExampleApp.ViewModels
{
    interface IViewModelLocator
    {
        ViewModelBase GetViewModel(string windowName);
    }
}