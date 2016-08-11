using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TrustModel.Util
{
    public abstract class XmlHolder<T> where T : XmlHolder<T>, new()
    {

        public static T LoadOrCreate(string filePath)
        {
            if (Exists(filePath))
            {
                return Load(filePath);
            }
            else
            {
                var instance =  new T();
                instance.Save(filePath);
                return instance;
            }
        }

        public static T Load(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (FileStream file = new FileStream(filePath, FileMode.Open))
                return (T)serializer.Deserialize(file);
        }

        public void Save(string filePath)
        {
            CheckDirectory(filePath);
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (FileStream file = new FileStream(filePath, FileMode.Create))
                serializer.Serialize(file, this);
        }

        public static bool Exists(string filePath)
        {
            return File.Exists(filePath);
        }


        private static void CheckDirectory(string filePath)
        {
            var dirPath = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);
        }
    }
}
