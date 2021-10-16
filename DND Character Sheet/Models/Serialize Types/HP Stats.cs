using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using DND_Character_Sheet.Annotations;

namespace DND_Character_Sheet.Models.Serialize_Types
{
    public class HPStats : INotifyPropertyChanged
    {
        private int maxHp;
        private int currentHp;
        private int tempHp;
        public HPStats() : this(0, 0, 0) { }
        public HPStats(int MaxHP, int CurrentHP, int TempHP)
        {
            this.MaxHP = MaxHP;
            this.CurrentHP = CurrentHP;
            this.TempHP = TempHP;
        }

        public int MaxHP
        {
            get
            {
                return maxHp;
            }
            set
            {
                maxHp = value;
                OnPropertyChanged("MaxHP");
            }
        }

        public int CurrentHP
        {
            get
            {
                return currentHp;
            }
            set
            {
                currentHp = value;
                OnPropertyChanged("CurrentHP");
            }
        }

        public int TempHP
        {
            get
            {
                return tempHp;
            }
            set
            {
                tempHp = value;
                OnPropertyChanged("TempHP");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) 
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
