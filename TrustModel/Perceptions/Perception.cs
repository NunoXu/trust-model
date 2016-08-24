using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TrustModel.Perceptions
{
    [Serializable]
    public class Perception : INotifyPropertyChanged
    {
        private string _name = "";

        [XmlElement]
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                NotifyPropertyChanged();
            }
        } 

        public Perception()
        {
            _name = Guid.NewGuid().ToString();
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
/*
        public override bool Equals(object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to Point return false.
            Perception p = obj as Perception;
            if ((System.Object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (Name == p.Name);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }*/
    }
}
