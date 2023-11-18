using System.Xml.Serialization;

namespace Organizations.Helpers
{
    /// <summary>
    /// Класс-помощник для сериализации и десериализации в XML
    /// </summary>
    internal class XmlHelper
    {
        readonly XmlSerializer _serializer;
        readonly string _filename;
        public XmlHelper(Type type, string filename)
        {
            _serializer = new XmlSerializer(type);
            _filename = filename;
        }
        public void Serialize(object obj)
        {
            using (var stream = new FileStream(_filename, FileMode.OpenOrCreate))
            {
                _serializer.Serialize(stream, obj);
            }
        }
        public object? Deserialize()
        {
            object? obj = null;

            try
            {
                using (var stream = new FileStream(_filename, FileMode.OpenOrCreate))
                {

                    obj = _serializer.Deserialize(stream);
                }
            }
            catch
            {
            }
            return obj;
        }
    }
}
