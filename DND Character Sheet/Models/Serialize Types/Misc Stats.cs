using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using DND_Character_Sheet.Annotations;

namespace DND_Character_Sheet.Models.Serialize_Types
{
    public class MiscStats : INotifyPropertyChanged
    {
        private string proficiency;
        private int ac;
        private string initiative;
        private string speed;
        public MiscStats() : this("0", 0, "0", "0") { }

        public MiscStats(string Proficiency, int AC, string Initiative, string Speed)
        {
            this.Proficiency = Proficiency;
            this.AC = AC;
            this.Initiative = Initiative;
            this.Speed = Speed;
        }

        public string Proficiency
        {
            get
            {
                return proficiency;
            }
            set
            {
                proficiency = value;
                OnPropertyChanged("Proficiency");
            }
        }

        public int AC
        {
            get
            {
                return ac;
            }
            set
            {
                ac = value;
                OnPropertyChanged("AC");
            }
        }

        public string Initiative
        {
            get
            {
                return initiative;
            }
            set
            {
                initiative = value;
                OnPropertyChanged("Initiative");
            }
        }

        public string Speed
        {
            get
            {
                return speed;
            }
            set
            {
                speed = value;
                OnPropertyChanged("Speed");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) 
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
