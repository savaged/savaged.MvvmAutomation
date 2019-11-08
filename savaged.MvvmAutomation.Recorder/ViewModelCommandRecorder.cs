using GalaSoft.MvvmLight;
using System;
using System.Windows.Input;

namespace savaged.MvvmAutomation.Recorder
{
    class ViewModelCommandRecorder : ViewModelRecorder
    {
        private readonly ICommand _commandInvoked;

        public ViewModelCommandRecorder(
            string callerMemberName,
            ICommand commandInvoked)
            : base(callerMemberName)
        {
            _commandInvoked = commandInvoked ??
                throw new ArgumentNullException(nameof(commandInvoked));
        }

        protected override Recording NewRecording(ViewModelBase viewModel)
        {
            var value = new CommandInvokedRecording(
                _commandInvoked, ViewModelType, viewModel);
            return value;
        }

    }
}
