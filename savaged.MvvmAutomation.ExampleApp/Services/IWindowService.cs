using System;

namespace savaged.MvvmAutomation.ExampleApp.Services
{
    public interface IWindowService
    {
        bool? ShowDialog(string windowName);

        void Show(string windowName);
    }
}
