using System.Threading.Tasks;

namespace savaged.MvvmAutomation.Recorder
{
    public interface IWriter
    {
        Task WriteAsync(string value);
    }
}