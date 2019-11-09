using GalaSoft.MvvmLight.Ioc;
using savaged.ExampleApp;
using savaged.ExampleApp.Services;
using savaged.ExampleApp.ViewModels;
using savaged.MvvmAutomation.Recorder;
using System;
using System.Collections.Generic;
using System.Windows;

namespace savaged.MvvmAutomation.Bridge
{
    public partial class App : Application
    {
        private static MainWindow _app;

        private async void OnStartup(object sender, StartupEventArgs e)
        {
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
            var mainViewWindow = SimpleIoc.Default.GetInstance<MainViewModel>();
            _app = new MainWindow
            {
                DataContext = mainViewWindow
            };
            _app.Show();

            object[] ctorArgs = 
            {
                SimpleIoc.Default.GetInstance<IWindowService>() 
            };
            var recordingService = new RecordingService
            {
                IsEnabled = true
            };
            recordingService.RecordBefore(
                mainViewWindow.ShowExampleDialogCmd, 
                mainViewWindow,
                ctorArgs);
            mainViewWindow.ShowExampleDialogCmd.Execute(null);
            recordingService.RecordAfter(
                mainViewWindow,
                ctorArgs);
            await recordingService.SaveAsync();
        }
    }
}
