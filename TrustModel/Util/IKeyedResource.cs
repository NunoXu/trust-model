using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TrustModel.Util
{
    public interface IKeyedResource<T>
    {
        [XmlIgnore]
        T Key { get; }
        [XmlIgnore]
        bool Deleted { get; set; }
    }
}
