using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using DND_Character_Sheet.Annotations;
using DND_Character_Sheet.Models.Serialize_Types;

namespace DND_Character_Sheet.ViewModels
{
    public class SpellLevelViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Spell> spells;
        private int spellLevel;
        private string slotsTotal;
        private string slotsExpended;

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

        public string SlotsTotal
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

        public string SlotsExpended
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

        public SpellLevelViewModel(int numOfSpells, int spellLevel) : this(numOfSpells, spellLevel, "0", "0") { }

        private SpellLevelViewModel(int numOfSpells, int spellLevel, string slotsTotal, string slotsExpended)
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

        public void RemoveExtraDeserialisedSpells() 
            => Spells = new ObservableCollection<Spell>(Spells.Where(spell => Spells.IndexOf(spell) >= Spells.Count / 2).ToList());

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
