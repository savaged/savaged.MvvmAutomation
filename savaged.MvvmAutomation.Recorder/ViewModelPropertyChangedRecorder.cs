using GalaSoft.MvvmLight;
using System;
using System.ComponentModel;

namespace savaged.MvvmAutomation.Recorder
{
    class ViewModelPropertyChangedRecorder : ViewModelRecorder
    {
        private readonly PropertyChangedEventArgs _propertyChangedEventArgs;

        public ViewModelPropertyChangedRecorder(
            string callerMemberName, 
            PropertyChangedEventArgs e)
            : base(callerMemberName)
        {
            _propertyChangedEventArgs = e ??
                throw new ArgumentNullException(nameof(e));
        }

        protected override Recording NewRecording(ViewModelBase viewModel)
        {
            var value = new PropertyChangedRecording(
                _propertyChangedEventArgs, ViewModelType, viewModel);
            return value;
        }

    }
}
