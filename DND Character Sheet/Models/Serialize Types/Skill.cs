using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using DND_Character_Sheet.Annotations;
using DND_Character_Sheet.Enums;

namespace DND_Character_Sheet.Models.Serialize_Types
{
    public class Skill : INotifyPropertyChanged
    {
        private string name;
        private string skillScore;
        private bool isProficient;
        private StatType statType;

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public string SkillScore
        {
            get
            {
                return skillScore;
            }
            set
            {
                skillScore = value;
                OnPropertyChanged("SkillScore");
            }
        }

        public bool IsProficient
        {
            get
            {
                return isProficient;
            }
            set
            {
                isProficient = value;
                OnPropertyChanged("IsProficient");
            }
        }

        public StatType StatType
        {
            get
            {
                return statType;
            }
            set
            {
                statType = value;
                OnPropertyChanged("StatType");
            }
        }

        public Skill(string name, string skillScore, bool isProficient = false)
        {
            Name = name;
            SkillScore = skillScore;
            IsProficient = isProficient;
            Enum.TryParse(name.Substring(name.IndexOf('(') + 1, 3), out StatType statType);
            StatType = statType;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
