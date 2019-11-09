using System;

namespace savaged.MvvmAutomation.ExampleApp.Services
{
    interface IWindowService
    {
        bool? ShowDialog(string windowName);

        void Show(string windowName);
    }
}
