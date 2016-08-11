using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TrustModel.Util
{
    public abstract class XmlHolder<T> where T : new()
    {

        public static T LoadOrCreate(string filePath)
        {
            if (Exists(filePath))
            {
                return Load(filePath);
            }
            else
                return new T();
        }

        public static T Load(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (FileStream file = new FileStream(filePath, FileMode.Open))
                return (T)serializer.Deserialize(file);
        }

        public void Save(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (FileStream file = new FileStream(filePath, FileMode.Create))
                serializer.Serialize(file, this);
        }

        public static bool Exists(string filePath)
        {
            return File.Exists(filePath);
        }
    }
}
