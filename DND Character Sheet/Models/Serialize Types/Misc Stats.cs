using System.ComponentModel;
using System.Runtime.CompilerServices;
using DND_Character_Sheet.Annotations;

namespace DND_Character_Sheet.Models.Serialize_Types
{
    public class MiscStats : INotifyPropertyChanged
    {
        private string proficiency;
        private int ac;
        private string initiative;
        private string speed;

        public MiscStats() : this("0", 0, "0", "0", "0", "0") { }

        public MiscStats(string proficiency, int ac, string initiative, string speed, string inspiration, string passivePerception)
        {
            Proficiency = proficiency;
            AC = ac;
            Initiative = initiative;
            Speed = speed;
            Inspiration = inspiration;
            PassivePerception = passivePerception;
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

        private string inspiration;

        public string Inspiration
        {
            get
            {
                return inspiration;
            }
            set
            {
                inspiration = value; 
                OnPropertyChanged("Inspiration");
            }
        }

        private string passivePerception;

        public string PassivePerception
        {
            get
            {
                return passivePerception;
            }
            set
            {
                passivePerception = value; 
                OnPropertyChanged("PassivePerception");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) 
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
