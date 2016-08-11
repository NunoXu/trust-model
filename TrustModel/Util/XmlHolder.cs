using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TrustModel.Util
{
    public abstract class XmlHolder<T>
    {
        public static T Load(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            FileStream file = new FileStream(filePath, FileMode.Open);
            return (T) serializer.Deserialize(file);

        }

        public void Save(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            FileStream file = new FileStream(filePath, FileMode.Create);
            serializer.Serialize(file, this);
        }
    }
}
