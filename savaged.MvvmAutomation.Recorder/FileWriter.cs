using System;
using System.IO;
using System.Threading.Tasks;

namespace savaged.MvvmAutomation.Recorder
{
    class FileWriter : IWriter
    {
        private FileInfo _file;

        public FileWriter(string location)
        {
            _file = new FileInfo(location);
        }

        public async Task WriteAsync(string value)
        {
            if (_file.Exists)
            {
                _file = new FileInfo($"{_file.Name}-{DateTime.Now.ToFileTime()}");
            }
            using (var sw = _file.CreateText())
            {
                await sw.WriteAsync(value);
            }
        }
    }
}
