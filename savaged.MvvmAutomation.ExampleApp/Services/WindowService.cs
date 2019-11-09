using savaged.MvvmAutomation.ExampleApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows;

namespace savaged.MvvmAutomation.ExampleApp.Services
{
    public class WindowService : IWindowService
    {
        private readonly IDictionary<string, Type> _windowTypes;
        private readonly IViewModelLocator _viewModelLocator;

        public WindowService(
            IDictionary<string, Type> windowTypes,
            IViewModelLocator viewModelLocator = null)
        {
            _windowTypes = windowTypes ?? new Dictionary<string, Type>();
            _viewModelLocator = viewModelLocator ??
                new ViewModelLocator();
        }

        public void Show(string windowName)
        {
            var window = GetWindowWithDataContext(windowName);
            window.Show();
        }

        public bool? ShowDialog(string windowName)
        {
            bool? result;
            var window = GetWindowWithDataContext(windowName);
            result = window.ShowDialog();
            return result;
        }

        private Window GetWindowWithDataContext(string windowName)
        {
            Window window = null;
            if (_windowTypes.ContainsKey(windowName))
            {
                var windowType = _windowTypes[windowName];
                if (windowType == null)
                {
                    throw new InvalidOperationException(
                        $"{windowName} not found!");
                }
                window = Activator.CreateInstance(windowType) as Window;
            }
            if (window == null)
            {
                throw new InvalidOperationException(
                    $"{windowName} failed to activate!");
            }
            window.DataContext = _viewModelLocator.GetViewModel(windowName);
            return window;
        }

    }
}
