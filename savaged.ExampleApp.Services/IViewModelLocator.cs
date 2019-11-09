using GalaSoft.MvvmLight;

namespace savaged.ExampleApp.Services
{
    public interface IViewModelLocator
    {
        ViewModelBase GetViewModel(string windowName);
    }
}