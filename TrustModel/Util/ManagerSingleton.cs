using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace TrustModel.Util
{
    public abstract class ManagerSingleton<T> : Singleton<T>, INotifyPropertyChanged
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
                InObjectLoad();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }



        public abstract void LoadOrCreate();
        public abstract void Save();
        public abstract void Load();
        protected abstract void InObjectLoad();
    }
}
