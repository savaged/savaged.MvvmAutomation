using AutoMapper;
using GalaSoft.MvvmLight;
using System;
using System.Diagnostics.CodeAnalysis;

namespace savaged.MvvmAutomation.Recorder
{
    abstract class ViewModelRecorder
    {
        private readonly string _callerMemberName;        

        private Recording _before = null;
        private Recording _after;

        public ViewModelRecorder(string callerMemberName)
        {
            _callerMemberName = !string.IsNullOrEmpty(callerMemberName) ?
                callerMemberName : throw new ArgumentNullException(
                    nameof(callerMemberName));
        }

        public void RecordBefore<T>(T viewModel)
            where T : ViewModelBase
        {
            ViewModelType = typeof(T);

            if (_after != null)
            {
                throw new InvalidOperationException(
                    "This method must be called before calling " +
                    nameof(RecordAfter));
            }
            _after = null;
            Record(_before, viewModel);
        }

        public void RecordAfter(
            string callerMemberName, ViewModelBase viewModel)
        {
            if (callerMemberName != _callerMemberName)
            {
                throw new InvalidOperationException(
                    $"The {nameof(RecordAfter)} should be called in the " +
                    $"same 'Caller Member' as {nameof(RecordBefore)}.");
            }
            if (_before == null)
            {
                throw new InvalidOperationException(
                    "This method must be called after calling " +
                    nameof(RecordBefore));
            }
            if (ViewModelType == null)
            {
                throw new InvalidOperationException(
                    $"The {nameof(ViewModelType)} should be set by " +
                    $"{nameof(RecordBefore)} prior to calling " +
                    $"{nameof(RecordAfter)}!");
            }
            if (viewModel?.GetType() != ViewModelType ||
                ViewModelType != _before.ViewModel.GetType())
            {
                throw new InvalidOperationException(
                    $"Always use the same {nameof(viewModel)}!");
            }
            Record(_after, viewModel);
        }

        public (Recording Before, Recording After) Recordings =>
            (Before: _before, After: _after);

        protected Type ViewModelType { get; private set; }

        protected abstract Recording NewRecording(ViewModelBase viewModel);

        private void Record(Recording recording, ViewModelBase viewModel)
        {
            var snapshot = Copy(viewModel);
            recording = NewRecording(snapshot);
        }

        private ViewModelBase Copy(ViewModelBase viewModel)
        {
            object empty = Activator.CreateInstance(ViewModelType);
            var mc = new MapperConfiguration(
                c => c.CreateMap(ViewModelType, ViewModelType));
            var mapper = mc.CreateMapper();
            var mapped = mapper.Map(
                viewModel, empty, ViewModelType, ViewModelType);
            var value = mapped as ViewModelBase;
            return value;
        }

    }
}
