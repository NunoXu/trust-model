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
        public class CategoriesHolder : XmlCollectionHolder<CategoriesHolder, Category>
        {
            [XmlElement("Category")]
            public override ObservableCollection<Category> List { get; set; } = new ObservableCollection<Category>();
        }


        public CategoriesHolder Categories = new CategoriesHolder();


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

        protected override void InObjectLoad()
        {
            Categories.InObjectLoad(FilePath);
        }
    }
}
