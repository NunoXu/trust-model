using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;
using TrustModel.Util;

namespace TrustModel.Features
{
    public class CategoriesManager : ManagerSingleton<CategoriesManager>
    {
        [Serializable, XmlRoot("Categories"), XmlType("Categories")]
        public class CategoriesHolder : XmlCollectionHolder<CategoriesHolder, string>
        {
            [XmlElement("Category")]
            public override ObservableCollection<string> List { get; set; } = new ObservableCollection<string>();
        }


        public CategoriesHolder Categories;


        public override void LoadOrCreate()
        {
            Categories = CategoriesHolder.LoadOrCreate(FilePath);
        }

        public override void Load()
        {
            Categories = CategoriesHolder.Load(FilePath);
        }

        public override void Save()
        {
            Categories.Save(FilePath);
        }
    }
}
