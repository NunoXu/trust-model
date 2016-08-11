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


        public string CategoriesFilePath;
        public CategoriesHolder Categories;


        public CategoriesManager(string filePath)
        {
            CategoriesFilePath = filePath;
            LoadOrCreate();
        }

        public void LoadOrCreate()
        {
            Categories = CategoriesHolder.LoadOrCreate(CategoriesFilePath);
        }

        public void Load()
        {
            Categories = CategoriesHolder.Load(CategoriesFilePath);
        }

        public void Save()
        {
            Categories.Save(CategoriesFilePath);
        }
    }
}
