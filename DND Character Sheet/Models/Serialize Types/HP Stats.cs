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
        private string totalHitDie;
        private string hitDie;
        private bool successSave1;
        private bool successSave2;
        private bool successSave3;
        private bool failureSave1;
        private bool failureSave2;
        private bool failureSave3;

        public HPStats() : this(0, 0, 0, "", "", false, false, false, false, false, false) { }
        public HPStats(int MaxHP, int CurrentHP, int TempHP, string HitDie, string TotalHitDie, 
            bool SuccessSave1, bool SuccessSave2, bool SuccessSave3, bool FailureSave1, bool FailureSave2, bool FailureSave3)
        {
            this.MaxHP = MaxHP;
            this.CurrentHP = CurrentHP;
            this.TempHP = TempHP;
            this.HitDie = HitDie;
            this.TotalHitDie = TotalHitDie;
            this.SuccessSave1 = SuccessSave1;
            this.SuccessSave2 = SuccessSave2;
            this.SuccessSave3 = SuccessSave3;
            this.FailureSave1 = FailureSave1;
            this.FailureSave2 = FailureSave2;
            this.FailureSave3 = FailureSave3;
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

        public string HitDie
        {
            get
            {
                return hitDie;
            }
            set
            {
                hitDie = value; 
                OnPropertyChanged("HitDie");
            }
        }

        public string TotalHitDie
        {
            get
            {
                return totalHitDie;
            }
            set
            {
                totalHitDie = value;
                OnPropertyChanged("TotalHitDie");
            }
        }

        public bool SuccessSave1
        {
            get 
            { 
                return successSave1; 
            }
            set 
            {
                successSave1 = value;
                OnPropertyChanged("SuccessSave1");
            }
        }

        public bool SuccessSave2
        {
            get 
            { 
                return successSave2; 
            }
            set 
            {
                successSave2 = value;
                OnPropertyChanged("SuccessSave2");
            }
        }

        public bool SuccessSave3
        {
            get
            {
                return successSave3;
            }
            set
            {
                successSave3 = value;
                OnPropertyChanged("SuccessSave3");
            }
        }

        public bool FailureSave1
        {
            get
            {
                return failureSave1;
            }
            set
            {
                failureSave1 = value;
                OnPropertyChanged("FailureSave1");
            }
        }

        public bool FailureSave2
        {
            get
            {
                return failureSave2;
            }
            set
            {
                failureSave2 = value;
                OnPropertyChanged("FailureSave2");
            }
        }

        public bool FailureSave3
        {
            get
            {
                return failureSave3;
            }
            set
            {
                failureSave3 = value;
                OnPropertyChanged("FailureSave3");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) 
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
