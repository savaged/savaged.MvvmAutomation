using Newtonsoft.Json;

namespace savaged.MvvmAutomation.Recorder
{
    class JsonSerialiser : ISerialiser
    {
        public string Serialize(object obj)
        {
            var value = JsonConvert.SerializeObject(
                obj, Formatting.Indented);
            return value;
        }
    }
}
