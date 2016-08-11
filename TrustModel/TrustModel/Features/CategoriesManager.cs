using NetObjectToFileWritter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using TrustModel.Util;

namespace TrustModel.Features
{
    public class CategoriesManager
    {

        [Serializable, XmlRoot("Categories"), XmlType("Categories")]
        public class CategoriesHolder : XmlHolder<CategoriesHolder>
        {
            [XmlElement("Category")]
            public List<string> List = new List<string>();

        }

        public CategoriesHolder Categories = new CategoriesHolder();

        public void Load(string filePath)
        {
            Categories = CategoriesHolder.Load(filePath);
        }

        public void Save(string filePath)
        {
            Categories.Save(filePath);
        }
    }
}
