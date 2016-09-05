using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using TrustModel.Util;
using Utils;

namespace TrustModel.Features
{
    public class CategoriesManager : ManagerSingleton<CategoriesManager, string, Category>
    {
        

        [Serializable, XmlRoot("Categories"), XmlType("Categories")]
        public class CategoriesHolder : ResourceHolder<CategoriesHolder>
        {
            public override SerializableDictionary<string, Category> Map { get; set; } = new SerializableDictionary<string, Category>();
        }


        public CategoriesHolder Categories = new CategoriesHolder();

        public override SerializableDictionary<string, Category> ResourceMap
        {
            get
            {
                return Categories.Map;
            }
        }

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
