using System.ComponentModel;
using System.Runtime.CompilerServices;
using DND_Character_Sheet.Annotations;
using DND_Character_Sheet.Models.Serialize_Types;

namespace DND_Character_Sheet.ViewModels
{
    public class SpellsDialogViewModel : INotifyPropertyChanged
    {
        private AllSpells allSpells;

        public AllSpells AllSpells
        {
            get
            {
                return allSpells;
            }
            set
            {
                allSpells = value;
                OnPropertyChanged("AllSpells");
            }
        }


        public SpellsDialogViewModel(AllSpells allSpells)
        {
            AllSpells = allSpells;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
