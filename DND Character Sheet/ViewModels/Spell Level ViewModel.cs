using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using DND_Character_Sheet.Annotations;
using DND_Character_Sheet.Models.Serialize_Types;
using DND_Character_Sheet.Wrappers;

namespace DND_Character_Sheet.ViewModels
{
    public class SpellLevelViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Spell> spells;
        private int spellLevel;
        private int slotsTotal;
        private int slotsExpended;

        public ObservableCollection<Spell> Spells
        {
            get
            {
                return spells;
            }
            set
            {
                spells = value;
                OnPropertyChanged("Spells");
            }
        }

        public int SpellLevel
        {
            get
            {
                return spellLevel;
            }
            set
            {
                spellLevel = value;
                OnPropertyChanged("SpellLevel");
            }
        }

        public int SlotsTotal
        {
            get
            {
                return slotsTotal;
            }
            set
            {
                slotsTotal = value;
                OnPropertyChanged("SlotsTotal");
            }
        }

        public int SlotsExpended
        {
            get
            {
                return slotsExpended;
            }
            set
            {
                slotsExpended = value;
                OnPropertyChanged("SlotsExpended");
            }
        }

        public IOpenNewViewWrapper OpenNewViewWrapper { get; set; }

        public SpellLevelViewModel(int numOfSpells, int spellLevel) : this(numOfSpells, spellLevel, 0, 0) { }

        public SpellLevelViewModel(int numOfSpells, int spellLevel, int slotsTotal, int slotsExpended)
        {
            SpellLevel = spellLevel;
            SlotsTotal = slotsTotal;
            SlotsExpended = slotsExpended;
            Spells = new ObservableCollection<Spell>();

            for (int i = 0; i < numOfSpells; i++)
            {
                Spells.Add(new Spell());
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
