using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;

namespace savaged.MvvmAutomation.Recorder
{
    public class RecordingService
    {
        private readonly LinkedList<(Recording Before, Recording After)> 
            _recordings;
        private readonly ISerialiser _serialiser;
        private readonly IWriter _writer;

        private ViewModelCommandRecorder _commandInvokedRecorder;
        private ViewModelPropertyChangedRecorder _propertyChangedRecorder;

        public RecordingService(
            string saveLocation = null,
            ISerialiser serialiser = null,
            IWriter writer = null)
        {
            if (string.IsNullOrEmpty(saveLocation))
            {
                saveLocation =
                    $"{Path.GetTempPath()}{GetType().Namespace}\\";
            }

            _serialiser = serialiser ?? new JsonSerialiser();

            _writer = writer ?? new FileWriter(saveLocation);

            _recordings = new LinkedList<(Recording Before, Recording After)>();
        }

        public bool IsEnabled { get; set; }

        public void RecordBefore<T>(
            ICommand commandInvoked, 
            T viewModel,
            [CallerMemberName] string callerMemberName = "")
            where T : ViewModelBase
        {
            if (!IsEnabled) return;
            _commandInvokedRecorder = new ViewModelCommandRecorder(
                callerMemberName, commandInvoked);
            _commandInvokedRecorder.RecordBefore(viewModel);
        }

        public void RecordAfter<T>(
            T viewModel,
            [CallerMemberName] string callerMemberName = "")
            where T : ViewModelBase
        {
            if (!IsEnabled) return;
            _commandInvokedRecorder.RecordAfter(callerMemberName, viewModel);
            _recordings.AddLast(_commandInvokedRecorder.Recordings);
        }

        public void RecordBefore<T>(
            PropertyChangedEventArgs e, 
            T viewModel,
            [CallerMemberName] string callerMemberName = "")
            where T : ViewModelBase
        {
            if (!IsEnabled) return;
            _propertyChangedRecorder = new ViewModelPropertyChangedRecorder(
                callerMemberName, e);
            _propertyChangedRecorder.RecordBefore(viewModel);
        }

        public async Task SaveAsync()
        {
            if (!IsEnabled) return;

            var firstRecording = _recordings.FirstOrDefault();
            if (firstRecording.Equals(
                default(ValueTuple<Recording, Recording>)))
            {
                return;
            }
            var serialised = await SerialiseRecordingsAsync();
            await WriteRecordingsAsync(serialised);
        }

        private async Task<string> SerialiseRecordingsAsync()
        {
            var value = await Task.Run(
                () => _serialiser.Serialize(_recordings));
            return value;
        }

        private async Task WriteRecordingsAsync(string value)
        {
            await _writer.WriteAsync(value);
        }

    }
}
