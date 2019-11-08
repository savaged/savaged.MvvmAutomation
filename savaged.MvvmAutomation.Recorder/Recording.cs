using GalaSoft.MvvmLight;
using System;

namespace savaged.MvvmAutomation.Recorder
{
    [Serializable]
    abstract class Recording
    {
        public Recording(
            Type type,
            ViewModelBase viewModel)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            ViewModel = viewModel
                 ?? throw new ArgumentNullException(nameof(viewModel));
        }

        public Type Type { get; }

        public ViewModelBase ViewModel { get; }

    }
}
