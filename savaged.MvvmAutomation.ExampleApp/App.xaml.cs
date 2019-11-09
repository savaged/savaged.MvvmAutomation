using GalaSoft.MvvmLight.Ioc;
using savaged.MvvmAutomation.ExampleApp.Services;
using savaged.MvvmAutomation.ExampleApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows;

namespace savaged.MvvmAutomation.ExampleApp
{
    public partial class App : Application
    {
        private static MainWindow _app;

        private void OnStartup(object sender, StartupEventArgs e)
        {
            // TODO Use Reflection to get windows
            var windowTypes = new Dictionary<string, Type>
            {
                { nameof(ExampleWindow), typeof(ExampleWindow) },
                { nameof(ExampleDialog), typeof(ExampleDialog) }
            };

            SimpleIoc.Default.Register<IDictionary<string, Type>>(() =>
            {
                return windowTypes;
            });

            SimpleIoc.Default.Register<IViewModelLocator, ViewModelLocator>();

            SimpleIoc.Default.Register<IWindowService, WindowService>();

            SimpleIoc.Default.Register<MainViewModel>();

            _app = new MainWindow
            {
                DataContext = SimpleIoc.Default.GetInstance<MainViewModel>()
            };
            _app.Show();
        }
    }
}
