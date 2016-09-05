using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TrustModel.Util;
using Utils;

namespace TrustModel.Actions
{
    public class ActionsManager : ManagerSingleton<ActionsManager, string, TrustAction>
    {
        [Serializable, XmlRoot("Actions"), XmlType("Actions")]
        public class ActionsHolder : ResourceHolder<ActionsHolder>
        {
            public override SerializableDictionary<string, TrustAction> Map { get; set; } = new SerializableDictionary<string, TrustAction>();
        }

        public ActionsHolder Actions = new ActionsHolder();

        public override SerializableDictionary<string, TrustAction> ResourceMap
        {
            get
            {
                return Actions.Map;
            }
        }
        

        public override void LoadOrCreate()
        {
            Actions = ActionsHolder.LoadOrCreate(FilePath);
        }

        public override void Load()
        {
            Actions = ActionsHolder.Load(FilePath);
        }

        public override void Save()
        {
            Actions.Save(FilePath);
        }

        protected override void InObjectLoad()
        {
            Actions.InObjectLoad(FilePath);
        }
    }
}
