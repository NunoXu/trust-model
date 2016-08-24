using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace TrustModel.Util
{
    public abstract class ManagerSingleton<T> : Singleton<T>
        where T: ManagerSingleton<T>, new()
    {
        private string _filePath;
        public string FilePath
        {
            get
            {
                return _filePath;
            }
            internal set
            {
                _filePath = value;
                LoadOrCreate();
            }
        }


        public abstract void LoadOrCreate();
        public abstract void Save();
        public abstract void Load();
    }
}
