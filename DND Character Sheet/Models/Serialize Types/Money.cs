using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using DND_Character_Sheet.Annotations;

namespace DND_Character_Sheet.Models.Serialize_Types
{
    public class Money : INotifyPropertyChanged
    {
        private string platinum = "";

        public string Platinum
        {
            get
            {
                return platinum;
            }
            set
            {
                platinum = value;
                OnPropertyChanged("Platinum");
            }
        }

        private string gold = "";

        public string Gold
        {
            get
            {
                return gold;
            }
            set
            {
                gold = value;
                OnPropertyChanged("Gold");
            }
        }

        private string silver = "";

        public string Silver
        {
            get
            {
                return silver;
            }
            set
            {
                silver = value;
                OnPropertyChanged("Silver");
            }
        }

        private string bronze = "";

        public string Bronze
        {
            get
            {
                return bronze;
            }
            set
            {
                bronze = value;
                OnPropertyChanged("Bronze");
            }
        }

        private string electrum = "";

        public string Electrum
        {
            get
            {
                return electrum;
            }
            set
            {
                electrum = value;
                OnPropertyChanged("Electrum");
            }
        }

        public Money() : this("", "", "", "", "") { }

        public Money(string platinum, string gold, string silver, string bronze, string electrum)
        {
            Platinum = platinum;
            Gold = gold;
            Silver = silver;
            Bronze = bronze;
            Electrum = electrum;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
