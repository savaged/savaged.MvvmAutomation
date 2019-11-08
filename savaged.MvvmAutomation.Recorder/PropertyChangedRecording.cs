using GalaSoft.MvvmLight;
using System;
using System.ComponentModel;

namespace savaged.MvvmAutomation.Recorder
{
    [Serializable]
    class PropertyChangedRecording : Recording
    {
        public PropertyChangedRecording(
            PropertyChangedEventArgs e,
            Type viewModelType,
            ViewModelBase viewModel)
            : base(viewModelType, viewModel)
        {
            PropertyChangedEventArgs = e ??
                throw new ArgumentNullException(nameof(e));
        }

        public PropertyChangedEventArgs PropertyChangedEventArgs
        { get; }

    }
}
