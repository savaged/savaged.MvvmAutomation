using AutoMapper;
using GalaSoft.MvvmLight;
using System;

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

        public void RecordBefore<T>(
            T viewModel, 
            object[] ctorArgs = null)
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
            Record(ref _before, viewModel, ctorArgs);
        }

        public void RecordAfter(
            string callerMemberName, 
            ViewModelBase viewModel,
            object[] ctorArgs = null)
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
            Record(ref _after, viewModel, ctorArgs);
        }

        public (Recording Before, Recording After) Recordings =>
            (Before: _before, After: _after);

        protected Type ViewModelType { get; private set; }

        protected abstract Recording NewRecording(ViewModelBase viewModel);

        private void Record(
            ref Recording recording, 
            ViewModelBase viewModel,
            object[] ctorArgs = null)
        {
            var snapshot = Copy(viewModel, ctorArgs);
            recording = NewRecording(snapshot);
        }

        private ViewModelBase Copy(
            ViewModelBase viewModel, object[] ctorArgs = null)
        {
            object template = null;
            if (ctorArgs == null)
            {
                template = Activator.CreateInstance(ViewModelType);
            }
            else
            {
                template = Activator.CreateInstance(
                    ViewModelType, ctorArgs);
            }
            var mc = new MapperConfiguration(
                c => c.CreateMap(ViewModelType, ViewModelType));
            var mapper = mc.CreateMapper();
            var mapped = mapper.Map(
                viewModel, template, ViewModelType, ViewModelType);
            var value = mapped as ViewModelBase;
            return value;
        }

    }
}
