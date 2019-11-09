using GalaSoft.MvvmLight;
using System.Collections.Generic;

namespace savaged.MvvmAutomation.ExampleApp.ViewModels
{
    class ViewModelLocator : IViewModelLocator
    {
        private IDictionary<string, ViewModelBase> _viewModels;

        public ViewModelLocator()
        {
            _viewModels = new Dictionary<string, ViewModelBase>
            {
                { "ExampleWindow", new ExampleWindowViewModel() },
                { "ExampleDialog", new ExampleDialogViewModel() }
            };
        }

        public ViewModelBase GetViewModel(string windowName)
        {
            var value = _viewModels[windowName];
            return value;
        }

    }
}
