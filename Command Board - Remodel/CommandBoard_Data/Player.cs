using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace CommandBoard_Data
{
    [DataContract]
    public class Player : INotifyPropertyChanged
    {
        private int _cash = 0;
        private int _totalValue = 0;
        public event PropertyChangedEventHandler PropertyChanged;

        [DataMember]
        public string cashS { set; get; }

        [DataMember]
        public string totalValueS { set; get; }

        [DataMember]
        public int cash {
            get {
                return _cash;
            }
            set {
                if (value != this._cash) {
                    this._cash = value;
                    cashS = value.ToString();
                    OnPropertyChanged("cashS");
                    //OnPropertyChanged("totalValueS");
                }
            }
        }

        [DataMember]
        public int totalValue {
            get {
                return _totalValue;
            }
            set {
                if (value != this.totalValue) {
                    this._totalValue = value;
                    totalValueS = value.ToString();
                    OnPropertyChanged("totalValueS");
                }
            }
        }

        [DataMember]
        public int row { set; get; }

        [DataMember]
        public int column { set; get; }

        [DataMember]
        public List<Location> checkPoints { set; get; }

        private void OnPropertyChanged(string property) {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

    }
}
