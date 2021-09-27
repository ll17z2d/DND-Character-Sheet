using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using DND_Character_Sheet.Annotations;
using DND_Character_Sheet.Models.Serialize_Types;

namespace DND_Character_Sheet.ViewModels
{
    public class SkillsDialogViewModel : INotifyPropertyChanged
    {
        private AllSkills allSkills;

        public AllSkills AllSkills
        {
            get
            {
                return allSkills;
            }
            set
            {
                allSkills = value;
                OnPropertyChanged("AllSkills");
            }
        }

        private bool isReadOnly;

        public bool IsReadOnly
        {
            get
            {
                return isReadOnly;

            }
            set
            {
                isReadOnly = value;
                OnPropertyChanged("IsReadOnly");
            }
        }

        public bool InverseIsReadOnly => !isReadOnly;

        public SkillsDialogViewModel(AllSkills allSkills, bool isReadOnly)
        {
            AllSkills = allSkills;
            IsReadOnly = isReadOnly;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
