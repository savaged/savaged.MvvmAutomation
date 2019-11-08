using GalaSoft.MvvmLight;
using System;
using System.Windows.Input;

namespace savaged.MvvmAutomation.Recorder
{
    [Serializable]
    class CommandInvokedRecording : Recording
    {
        public CommandInvokedRecording(
            ICommand commandInvoked,
            Type viewModelType,
            ViewModelBase viewModel)
            : base(viewModelType, viewModel)
        {
            CommandInvoked = commandInvoked ?? 
                throw new ArgumentNullException(nameof(commandInvoked));
        }

        public ICommand CommandInvoked { get; }

    }
}
