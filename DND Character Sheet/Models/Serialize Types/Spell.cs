using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using DND_Character_Sheet.Annotations;
using DND_Character_Sheet.Commands;
using DND_Character_Sheet.Wrappers;

namespace DND_Character_Sheet.Models.Serialize_Types
{
    public class Spell : INotifyPropertyChanged
    {
        private string name;
        private string description;
        private bool isPrepared;

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

        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }

        public bool IsPrepared
        {
            get
            {
                return isPrepared;
            }
            set
            {
                isPrepared = value;
                OnPropertyChanged("IsPrepared");
            }
        }

        public IOpenNewViewWrapper OpenNewViewWrapper { get; set; }

        public ICommand OpenSpellDetailsCommand { get; set; }

        public Spell() : this("Enter Spell Here", "Press '...' to edit spell details", false, new OpenNewViewWrapper()) { }

        private Spell(string name, string description, bool isPrepared, IOpenNewViewWrapper openNewViewWrapper)
        {
            Name = name;
            Description = description;
            IsPrepared = isPrepared;
            OpenNewViewWrapper = openNewViewWrapper;
            OpenSpellDetailsCommand = new MethodCommands(OpenSpellDetails);
        }

        public bool OpenSpellDetails() 
            => OpenNewViewWrapper.OpenSpellDetailsWindow(this);

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
