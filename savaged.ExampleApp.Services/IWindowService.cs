using System;

namespace savaged.ExampleApp.Services
{
    public interface IWindowService
    {
        bool? ShowDialog(string windowName);

        void Show(string windowName);
    }
}
